using System;
using System.Web.UI;
using JMQPresentacion.JMQWS;

namespace JMQPresentacion.Login
{
    public partial class Restablecer : Page
    {
        private string token;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    token = Request.QueryString["token"];
            //    if (string.IsNullOrEmpty(token))
            //    {
            //        MostrarMensaje("Token no válido o ausente.");
            //        btnRestablecer.Enabled = false;
            //        return;
            //    }

            //    try
            //    {
            //        UsuarioWSClient client = new UsuarioWSClient();
            //        bool esValido = client.validarTokenPassword(token);

            //        if (!esValido)
            //        {
            //            MostrarMensaje("El enlace ha expirado o no es válido.");
            //            btnRestablecer.Enabled = false;
            //        }
            //    }
            //    catch (System.Exception ex)
            //    {
            //        MostrarMensaje("Error al validar el token: " + ex.Message);
            //        btnRestablecer.Enabled = false;
            //    }
            //}
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            string nuevaPassword = txtNuevaPassword.Text.Trim();
            string confirmarPassword = txtConfirmarPassword.Text.Trim();
            token = Request.QueryString["token"];

            if (string.IsNullOrEmpty(nuevaPassword) || string.IsNullOrEmpty(confirmarPassword))
            {
                MostrarMensaje("Ambos campos son obligatorios.");
                return;
            }

            if (nuevaPassword != confirmarPassword)
            {
                MostrarMensaje("Las contraseñas no coinciden.");
                return;
            }

            try
            {
                UsuarioWSClient client = new UsuarioWSClient();
                bool resultado = client.cambiarPasswordConToken(token, nuevaPassword);

                if (resultado)
                {
                    lblMensaje.CssClass = "alert alert-success mt-2";
                    lblMensaje.Text = "Tu contraseña ha sido restablecida correctamente.";
                    lblMensaje.Visible = true;
                    btnRestablecer.Enabled = false;
                }
                else
                {
                    MostrarMensaje("No fue posible cambiar la contraseña.");
                }
            }
            catch (System.Exception ex)
            {
                MostrarMensaje("Ocurrió un error: " + ex.Message);
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = "alert alert-danger mt-2";
            lblMensaje.Visible = true;
        }
    }
}
