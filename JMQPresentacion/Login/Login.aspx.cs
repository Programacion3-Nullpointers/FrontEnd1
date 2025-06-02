using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQDominio;
namespace JMQPresentacion.Login
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            divError.Style["display"] = "none";
            lblError.Text = "";

            if (string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtContr.Text))
            {
                lblError.Text = "Complete todos los campos.";
                divError.Style["display"] = "block";
                return;
            }
            Usuario user = null; // = buscarUsuario(txtEmail.Text);
            if (user != null)
            {
                Session["Usuario"] = user;
                // descrifrar primero user.contrasena
                if (user.contrasena == txtContr.Text)
                {
                    Response.Redirect("/Pedidos/DatosEntrega.aspx");
                }
                else
                {
                    lblError.Text = "Contraseña incorrecta.";
                    divError.Style["display"] = "block";
                    return;
                }
            }
            else
            {
                lblError.Text = "Usuario no encontrado.";
                divError.Style["display"] = "block";
                return;
            }
        }
    }
}