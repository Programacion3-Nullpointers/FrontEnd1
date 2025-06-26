using JMQPresentacion.JMQWS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMQPresentacion.Cotizaciones
{

    public partial class DetalleCotizacion : System.Web.UI.Page
    {

        private ProductoCotizacionWSClient productoCotizacionWSClient;
        private CotizacionWSClient cotizacionWSClient;
        private UsuarioWSClient usuarioWSClient;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login/Login.aspx");
            }

            productoCotizacionWSClient = new JMQWS.ProductoCotizacionWSClient();
            cotizacionWSClient = new JMQWS.CotizacionWSClient();
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
                    int idCotizacion = Convert.ToInt32(Request.QueryString["id"]);
                    //usuario usu = Session["usuario"] as usuario;
                    //cotizacion coti = cotizacionWSClient.buscarCotizacion(usu.id);
                    CargarCotizacion(idCotizacion);
                }
            }
        }
            private void CargarCotizacion(int id)
            {
                try
                {

                    // Llama a tu servicio web CotizacionWS
                    cotizacion cotiza = cotizacionWSClient.buscarCotizacion(id); // O el método correcto según tu WS

                    // Muestra los datos generales (por ejemplo, en labels)
                    lblEstado.Text = cotiza.estadoCotizacion.ToString();


                    // Carga productos asociados
                    var productos = productoCotizacionWSClient.listarProductosPorCotizacion(id);

                    gvProductos.DataSource = productos;
                    gvProductos.DataBind();
                }
                catch (System.Exception ex)
                {
                    lblError.Text = "Error al cargar la cotización: " + ex.Message;
                }
            }
    }
}