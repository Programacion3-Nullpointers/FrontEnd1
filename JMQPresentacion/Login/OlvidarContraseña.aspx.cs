using System;
using System.Web.UI;
using JMQPresentacion.JMQWS;

namespace JMQPresentacion.Login
{
    public partial class OlvidarContraseña : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divError.Style["display"] = "none";
                btnRegistrarse.Visible = false;
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MostrarMensaje("Por favor, ingresa tu correo electrónico.", false);
                return;
            }

            if (!EsCorreoValido(email))
            {
                MostrarMensaje("El formato del correo no es válido.", false);
                return;
            }

            try
            {
                UsuarioWSClient client = new UsuarioWSClient();
                var usuario = client.BuscarUsuarioPorCorreo(email);

                if (usuario == null)
                {
                    MostrarMensaje("El correo no está registrado. <a href='Registrarse.aspx'>Regístrate aquí</a>", false);
                    btnRegistrarse.Visible = true;
                    return;
                }

                client.iniciarRecuperacionPassword(email);
                MostrarMensaje("Hemos enviado un enlace de recuperación a tu correo.", true);
            }
            catch (System.Exception ex)
            {
                MostrarMensaje("Ocurrió un error inesperado: " + ex.Message, false);
            }
        }

        private void MostrarMensaje(string mensaje, bool exito)
        {
            lblError.Text = mensaje;
            divError.Style["display"] = "block";
            divError.Attributes["class"] = exito ? "alert alert-success small" : "alert alert-danger small";
            btnRegistrarse.Visible = !exito;
        }

        private bool EsCorreoValido(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login/Registrarse.aspx");
        }
    }
}
