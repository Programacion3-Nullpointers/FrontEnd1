using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;

namespace JMQPresentacion.Admin
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private OrdenVentaWSClient ordenVentaWSClient;
        private DetalleWSClient detalleWSClient;

        protected void Page_Init(object sender, EventArgs e)
        {

            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login/Login.aspx");
            }
            ordenVentaWSClient = new JMQWS.OrdenVentaWSClient();
            detalleWSClient = new JMQWS.DetalleWSClient();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Validar si el usuario ha iniciado sesión
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Acceso/NoAutorizado.aspx");
                return;
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int idOrden = Convert.ToInt32(Request.QueryString["id"]);
                    //usuario usu = Session["usuario"] as usuario;
                    //cotizacion coti = cotizacionWSClient.buscarCotizacion(usu.id);
                    CargarDetalle(idOrden);
                }
            }
        }
        private void CargarDetalle(int id)
        {
            try
            {
                // Carga productos asociados

                //var detallePediddo = detalleWSClient.ListarDetalles();
                var detallePediddo = detalleWSClient.ListarPorOrden(id);


                gvDetalles.DataSource = detallePediddo;
                gvDetalles.DataBind();
            }
            catch (System.Exception ex)
            {
                lblError.Text = "Error al cargar el detalle pedido: " + ex.Message;
            }
        }
    }
}