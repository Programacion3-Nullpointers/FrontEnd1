using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;

namespace JMQPresentacion.Admin
{
    public partial class ModificarOrdenes : System.Web.UI.Page
    {
        private OrdenVentaWSClient ordenVentaWS = new OrdenVentaWSClient();
        private DetalleWSClient detalleWS = new DetalleWSClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarOrdenes();
            }
        }

        private void CargarOrdenes()
        {
            var resultado = ordenVentaWS.listarOrdenVenta();

            if (resultado == null || resultado.Length == 0)
            {
                gvOrdenes.DataSource = null;
                gvOrdenes.DataBind();

                lblMensaje.Text = "No hay órdenes de venta registradas.";
                lblMensaje.Visible = true;
                return;
            }

            List<ordenVenta> lista = resultado.ToList();
            gvOrdenes.DataSource = lista;
            gvOrdenes.DataBind();

            lblMensaje.Visible = false; // Oculta el mensaje si hay datos
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string estado = ddlEstado.SelectedValue;
            string idUsuarioStr = txtBuscarUsuario.Text.Trim();
            string activoStr = ddlActivo.SelectedValue;
            string fechaDesdeStr = txtFechaDesde.Text.Trim();
            string fechaHastaStr = txtFechaHasta.Text.Trim();

            // ID Usuario: si no se ingresa, se considera 0
            int idUsuario = 0;
            if (int.TryParse(idUsuarioStr, out int idTemp))
            {
                idUsuario = idTemp;
            }

            // Activo: solo filtrar inactivos si se selecciona "0", si no se selecciona nada se asume activos
            bool activo = true;
            if (activoStr == "0") activo = false;

            // Fecha Desde: si no hay input, usar fecha antigua como string
            string fechaDesde = "1900-01-01";
            if (DateTime.TryParse(fechaDesdeStr, out DateTime fechaTempDesde))
            {
                fechaDesde = fechaTempDesde.ToString("yyyy-MM-dd");
            }

            // Fecha Hasta: si no hay input, usar fecha actual como string
            string fechaHasta = DateTime.Now.ToString("yyyy-MM-dd");
            if (DateTime.TryParse(fechaHastaStr, out DateTime fechaTempHasta))
            {
                fechaHasta = fechaTempHasta.ToString("yyyy-MM-dd");
            }

            // Llamada al Web Service
            var resultado = ordenVentaWS.filtrarOrdenesVenta(
                string.IsNullOrEmpty(estado) ? null : estado,
                activo,
                idUsuario,
                fechaDesde,
                fechaHasta
            );

            if (resultado == null || resultado.Length == 0)
            {
                gvOrdenes.DataSource = null;
                gvOrdenes.DataBind();
                lblMensaje.Text = "No se encontraron órdenes con los filtros aplicados.";
                lblMensaje.Visible = true;
            }
            else
            {
                gvOrdenes.DataSource = resultado.ToList();
                gvOrdenes.DataBind();
                lblMensaje.Visible = false;
            }
        }


        protected void btnResetFiltros_Click(object sender, EventArgs e)
        {
            ddlEstado.SelectedIndex = 0;
            txtBuscarUsuario.Text = "";
            CargarOrdenes();
        }

        protected void gvOrdenes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                ordenVentaWS.eliminarOrdenVentaService(id);
                CargarOrdenes();
            }
            else if (e.CommandName == "Ver")
            {
                var detalles = detalleWS.ListarPorOrden(id);
                gvDetallesOrden.DataSource = detalles;
                gvDetallesOrden.DataBind();

                // Usar Bootstrap 5: modal Bootstrap sin jQuery
                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModalDetalles",
                    "var myModal = new bootstrap.Modal(document.getElementById('modalVerDetalles')); myModal.show();", true);
            }
            else if (e.CommandName == "CambiarEstado")
            {
                hfIdOrdenEstado.Value = id.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModalEstado", @"
                        var modal = new bootstrap.Modal(document.getElementById('modalCambiarEstado'));
                        modal.show();", true);
            }
        }
        protected void btnGuardarEstado_Click(object sender, EventArgs e)
        {
            int id = int.Parse(hfIdOrdenEstado.Value);
            string nuevoEstado = ddlNuevoEstado.SelectedValue;

            ordenVentaWS.actualizarEstadoOrdenVentaService(id, nuevoEstado);
            CargarOrdenes();
        }


    }
}