using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;

namespace JMQPresentacion.Login
{
    public partial class OlvidarContraseña : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Ocultar mensaje en primera carga
            if (!IsPostBack)
            {
                divError.Style["display"] = "none";
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MostrarMensaje("Por favor, ingresa tu correo.", false);
                btnRegistrarse.Visible = false;
                return;
            }
            //Validacion básica de formato de correo

            try
            {
                // Llamar al servicio SOAP real
                UsuarioWSClient client = new UsuarioWSClient();
                var usuario = client.BuscarUsuarioPorCorreo(email);

                if (usuario == null)
                {
                    // Mostrar mensaje personalizado con enlace si deseas
                    MostrarMensaje("El correo ingresado no está registrado. <a href='Registrarse.aspx'>Regístrate aquí</a>", false);
                    btnRegistrarse.Visible = true;
                    return;
                }

                client.iniciarRecuperacionPassword(email);
                MostrarMensaje("Hemos enviado un enlace de recuperación a tu correo.", true);
                btnRegistrarse.Visible = false;
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no está registrado") || ex.Message.Contains("registrado"))
                {
                    MostrarMensaje("El correo no está registrado. ¿Deseas registrarte?", false);
                    btnRegistrarse.Visible = true;
                }
                else
                {
                    MostrarMensaje("Ocurrió un error: " + ex.Message, false);
                    btnRegistrarse.Visible = false;
                }
            }

        }

        private void MostrarMensaje(string mensaje, bool exito)
        {
            lblError.Text = mensaje;
            divError.Style["display"] = "block";

            if (exito)
                divError.Attributes["class"] = "alert alert-success";
            else
                divError.Attributes["class"] = "alert alert-danger";
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login/Registrarse.aspx");
        }

    }
}