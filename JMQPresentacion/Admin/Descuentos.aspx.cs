using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;

namespace JMQPresentacion.Admin
{
    public partial class Descuentos : System.Web.UI.Page
    {
        private DescuentoWSClient descuentoWSClient;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            descuentoWSClient = new DescuentoWSClient();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrid();
            }
        }

        private void CargarGrid()
        {
            var descuentos = descuentoWSClient.ListarDescuentos();
            List<descuento> lista = descuentos != null ? descuentos.ToList() : new List<descuento>();

            Session["Descuentos"] = lista;

            gvDescuentos.DataSource = lista;
            gvDescuentos.DataBind();
        }
        protected void btnResetFiltros_Click(object sender, EventArgs e)
        {
            // Limpia campos de filtro
            ddlActivoFiltro.ClearSelection();
            txtPorcentajeMin.Text = "";
            txtPorcentajeMax.Text = "";

            // Recarga la lista original
            var descuentos = descuentoWSClient.ListarDescuentos();
            Session["Descuentos"] = descuentos;
            gvDescuentos.DataSource = descuentos;
            gvDescuentos.DataBind();

            // Oculta mensaje de "no encontrado"
            lblMensaje.Visible = false;
        }

        protected void btnAplicarFiltros_Click(object sender, EventArgs e)
        {
            // Filtro por estado activo/inactivo
            bool? activo = null;
            if (ddlActivoFiltro.SelectedValue == "true") activo = true;
            else if (ddlActivoFiltro.SelectedValue == "false") activo = false;

            // Filtros por porcentaje de descuento
            int? porcentajeMin = null;
            int? porcentajeMax = null;

            if (int.TryParse(txtPorcentajeMin.Text, out int min)) porcentajeMin = min;
            if (int.TryParse(txtPorcentajeMax.Text, out int max)) porcentajeMax = max;

            // Llamada al servicio con valores por defecto si los campos están vacíos
            var descuentosFiltrados = descuentoWSClient.filtrarDescuentos(
                activo ?? true,                   // Por defecto: mostrar activos
                porcentajeMin ?? 0,              // Por defecto: mínimo 0%
                porcentajeMax ?? 100             // Por defecto: máximo 100%
            );
          
            // Mensaje si no hay resultados
            lblMensaje.Visible = descuentosFiltrados == null || !descuentosFiltrados.Any();
            lblMensaje.Text = "⚠️ No se encontraron descuentos.";

            // Mostrar resultados
            Session["Descuentos"] = descuentosFiltrados;
            gvDescuentos.DataSource = descuentosFiltrados;
            gvDescuentos.DataBind();
        }




        protected void gvDescuentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                // Llama al método del WebService para eliminar
                descuentoWSClient.EliminarDescuento(id); // Asegúrate de tener esto en tu servicio

                // Recarga los datos actualizados
                CargarGrid();
            }
            else if (e.CommandName == "Editar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                List<descuento> lista = (List<descuento>)Session["Descuentos"];
                descuento seleccionado = lista.FirstOrDefault(x => x.id == id);

                if (seleccionado != null)
                {
                    ViewState["EditarId"] = seleccionado.id;
                    txtNumDescuento.Text = seleccionado.numDescuento.ToString();
                    chkActivo.Checked = seleccionado.activo;
                    ScriptManager.RegisterStartupScript(this, GetType(), "MostrarModalEditar", "mostrarModal();", true);
                }
            }
        }

        protected void btnGuardarDescuento_Click(object sender, EventArgs e)
        {
            List<descuento> lista = Session["Descuentos"] as List<descuento> ?? new List<descuento>();
            int? idEditando = ViewState["EditarId"] as int?;

            descuento nuevoDescuento = new descuento
            {
                numDescuento = int.Parse(txtNumDescuento.Text),
                activo = chkActivo.Checked
            };

            if (idEditando.HasValue)
            {
                // MODO EDICIÓN
                nuevoDescuento.id = idEditando.Value;

                // Actualizas datos
                descuentoWSClient.ActualizarDescuento(nuevoDescuento);

                // Lógica para activar o desactivar
                if (chkActivo.Checked)
                {
                    descuentoWSClient.activarDescuento(nuevoDescuento.id);
                }
                else
                {
                    descuentoWSClient.desactivarDescuento(nuevoDescuento.id); // Desactiva
                }

                ViewState["EditarId"] = null;
            }
            else
            {
                // MODO NUEVO
                descuentoWSClient.RegistrarDescuento(nuevoDescuento);
            }

            // Recarga
            Session["Descuentos"] = descuentoWSClient.ListarDescuentos();
            CargarGrid();

            // Limpieza
            txtNumDescuento.Text = "";
            chkActivo.Checked = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal", "cerrarModal();", true);
        }
    }

}
