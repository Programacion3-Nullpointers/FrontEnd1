using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;  // Asegúrate que 'detalle' esté en este namespace

namespace JMQPresentacion
{
    public partial class MainLayout : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verifica si estos controles están declarados en el .master correctamente
                phLogin.Visible = Session["Usuario"] == null;
                phLogout.Visible = Session["Usuario"] != null;

                // Asegúrate que btnLogout.Top y btnLogout (sidebar) no choquen en nombres o acciones
                MostrarCantidadCarrito();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["Usuario"] = null;
            Response.Redirect("/Principal/Principal.aspx");
        }

        private void MostrarCantidadCarrito()
        {
            if (Session["Cart"] != null)
            {
                List<detalle> carrito = Session["Cart"] as List<detalle>;
                int cantidadTotal = carrito.Sum(d => d.cantidad);

                litCantidadCarrito.Text = $@"
                    <span class='position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger'>
                        {cantidadTotal}
                    </span>";
            }
            else
            {
                litCantidadCarrito.Text = "";
            }
        }
    }
}
