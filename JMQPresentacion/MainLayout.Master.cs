using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;
using JMQPresentacion.JMQWS;  // Asegúrate que 'detalle' esté en este namespace


namespace JMQPresentacion
{
    public partial class MainLayout : System.Web.UI.MasterPage
    {
        protected HtmlButton btnUserDropdown;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    usuario user = (usuario)Session["Usuario"];
                    lblNombreUsuario.Text = $"{user.nombreUsuario}"; // Asume propiedades 'nombre' y 'apellido'

                    phLogin.Visible = false;           // Oculta el PlaceHolder de "Iniciar Sesión"
                    phUsuarioLogueado.Visible = true;  // Muestra el PlaceHolder del dropdown de usuario
                }
                else
                {
                    phLogin.Visible = true;            // Muestra el PlaceHolder de "Iniciar Sesión"
                    phUsuarioLogueado.Visible = false; // Oculta el PlaceHolder del dropdown de usuario
                }
                phLogin.Visible = Session["Usuario"] == null;
                phLogout.Visible = Session["Usuario"] != null;

                MostrarCantidadCarrito();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Lógica para cerrar la sesión del usuario
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Principal/Principal.aspx"); // Redirige a la página principal
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
