using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;

namespace JMQPresentacion.Login
{
    public partial class VerPedidosaspx : System.Web.UI.Page
    {
        private OrdenVentaWSClient ordenVentaWSClient;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
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
                // Obtener el ID del usuario desde la sesión
                usuario user = Session["Usuario"] as usuario;
                if (user == null)
                {
                    Response.Redirect("~/Login/Login.aspx");
                    return;
                }
                // Cargar las órdenes de venta del usuario
                CargarOrdenesVenta(user.id);
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
                gvOrdenesVenta.Visible = true;
                lblNoPedidos.Visible = false;
            }
        }

        protected void VerDetalle_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int idOrden = Convert.ToInt32(btn.CommandArgument);
            Response.Redirect($"DetallePedido.aspx?id={idOrden}");

        }
    }
}