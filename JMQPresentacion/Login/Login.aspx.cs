using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;
namespace JMQPresentacion.Login
{
    public partial class Login : System.Web.UI.Page
    {
        private UsuarioWSClient usuarioWSCLClient;

        protected void Page_Init(object sender, EventArgs e)
        {
            usuarioWSCLClient = new JMQWS.UsuarioWSClient();
        }

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
            usuario user = usuarioWSCLClient.BuscarUsuarioPorCorreo(txtEmail.Text); 
            if (user != null)
            {
                Session["Usuario"] = user;
                // descrifrar primero user.contrasena
                if (user.contrasena == txtContr.Text)
                {
                    if (user.tipoUsuario == tipoUsuario.ADMIN)
                    {
                        Response.Redirect("/Admin/PrincipalAdmin.aspx");
                    }
                    else
                        Response.Redirect("/Principal/Principal.aspx");
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