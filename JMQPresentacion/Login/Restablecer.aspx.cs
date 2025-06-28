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
                    MostrarMensaje("Tu contraseña ha sido restablecida correctamente.", true);
                    btnRestablecer.Enabled = false;
                    Response.AddHeader("REFRESH", "2;URL=Login.aspx"); // redirige en 2 segundos
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

        private void MostrarMensaje(string mensaje, bool esExito = false)
        {
            mensajeFlotante.InnerText = mensaje;
            mensajeFlotante.Attributes["class"] = "mensaje-flotante alert " + (esExito ? "alert-success" : "alert-danger");
            mensajeFlotante.Style["display"] = "block";
        }

    }
}
