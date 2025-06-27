using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;

namespace JMQPresentacion.Admin
{
    public partial class VerPedidosAdmin : System.Web.UI.Page
    {
        private OrdenVentaWSClient ordenVentaWSClient;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || ((usuario)Session["Usuario"]).tipoUsuario != tipoUsuario.ADMIN)
            {
                // Redirigir al login u otra acción
                Response.Redirect("~/Login/Login.aspx");
                return;
            }
            ordenVentaWSClient = new JMQWS.OrdenVentaWSClient();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get idUsuario from the query string (passed from modificarUsuarios.aspx)
                if (Request.QueryString["idUsuario"] != null)
                {
                    int idUsuario;
                    if (int.TryParse(Request.QueryString["idUsuario"], out idUsuario))
                    {
                        hfIdUsuario.Value = idUsuario.ToString(); // Store idUsuario in HiddenField
                        CargarOrdenesVenta(idUsuario);
                        CargarEstadosCompraEnDropdown(); // Cargar los estados de compra en el dropdown
                    }
                    else
                    {
                        Response.Write("<script>alert('ID de usuario inválido.');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('ID de usuario no proporcionado.');</script>");
                }
            }
        }

        private void CargarOrdenesVenta(int idUsuario)
        {
            var ordenes = ordenVentaWSClient.obtenerOrdenesVentasPorUsuario(idUsuario);

            if (ordenes != null && ordenes.Length > 0) 
            {
                gvOrdenesVenta.DataSource = ordenes;
                gvOrdenesVenta.DataBind(); 
                gvOrdenesVenta.Visible = true; 
                lblNoPedidos.Visible = false; 
            }
            else
            {
                gvOrdenesVenta.DataSource = new List<ordenVenta>(); 
                gvOrdenesVenta.DataBind();
                gvOrdenesVenta.Visible = false; 
                lblNoPedidos.Visible = true; 
            }

        }
        private void CargarEstadosCompraEnDropdown()
        {
            ddlEstadoOrden.Items.Clear();

            // Obtener todos los valores del enum EstadoCompra y agregarlos al DropDownList
            foreach (estadoCompra estado in Enum.GetValues(typeof(estadoCompra)))
            {
                ddlEstadoOrden.Items.Add(new ListItem(estado.ToString(), estado.ToString()));
            }
        }

        protected void gvOrdenesVenta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarEstado")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                ordenVenta ordenActual = ordenVentaWSClient.buscarOrdenVentaServicesById(id);

                if (ordenActual != null)
                {
                    //Seleccionar el estado actual en el DropDownList
                    hfIdOrdenVentaEditar.Value = id.ToString();
                    estadoCompra estadoCompraUsuario = ordenActual.estado_compra;
                    ListItem item = ddlEstadoOrden.Items.FindByValue(estadoCompraUsuario.ToString());
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "MostrarModalEditar", "mostrarModalEditarEstado();", true);
            }
        }

        protected void btnGuardarEstado_Click(object sender, EventArgs e)
        {
            int idOrdenVenta;
            // Obtener el ID de la orden del HiddenField del modal
            if (int.TryParse(hfIdOrdenVentaEditar.Value, out idOrdenVenta))
            {
                string nuevoEstado = ddlEstadoOrden.SelectedValue;

                // Llamar al servicio para actualizar el estado
                ordenVentaWSClient.actualizarEstadoOrdenVentaService(idOrdenVenta, nuevoEstado);

                int idUsuarioActual = int.Parse(hfIdUsuario.Value); 
                CargarOrdenesVenta(idUsuarioActual);

                // Ocultar el modal
                ScriptManager.RegisterStartupScript(this, GetType(), "OcultarModalEditar", "cerrarModalEditarEstado();", true);
                Response.Write("<script>alert('Estado de orden actualizado correctamente.');</script>");
              

            }
            else
            {
                Response.Write("<script>alert('No se pudo obtener el ID de la orden para actualizar.');</script>");
            }
        }

        protected void VerDetalle_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int idOrden = Convert.ToInt32(btn.CommandArgument);
            Response.Redirect($"DetallePedidoAAdmin.aspx?id={idOrden}");

        }
    }
}