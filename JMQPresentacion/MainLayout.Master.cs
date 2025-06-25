using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;

namespace JMQPresentacion
{
    public partial class MainLayout : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                phLogin.Visible = Session["Usuario"] == null;
                phLogout.Visible = Session["Usuario"] != null;

                MostrarCantidadCarrito();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Limpiar sesión de usuario y carrito
            Session["Usuario"] = null;
            Session["Cart"] = null; // 🟡 Aquí eliminamos el carrito también

            Response.Redirect("/Principal/Principal.aspx?logout=1");
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
