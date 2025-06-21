using System;
using System.Web.UI;

namespace JMQPresentacion.Login
{
    public partial class Restablecer : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && string.IsNullOrEmpty(Request.QueryString["token"]))
            {
                lblMensaje.Text = "Token inválido o ausente.";
                lblMensaje.Visible = true;
                btnRestablecer.Enabled = false;
            }
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            string nuevaPassword = txtNuevaPassword.Text.Trim();
            string confirmarPassword = txtConfirmarPassword.Text.Trim();
            string token = Request.QueryString["token"];

            if (nuevaPassword != confirmarPassword)
            {
                lblMensaje.Text = "Las contraseñas no coinciden.";
                lblMensaje.Visible = true;
                return;
            }

            // Aquí llamas al servicio web para cambiar la contraseña
            try
            {
                var cliente = new JMQWS.UsuarioWSClient();
                cliente.cambiarPasswordConToken(token, nuevaPassword);

                lblMensaje.CssClass = "text-success";
                lblMensaje.Text = "Contraseña restablecida correctamente.";
                lblMensaje.Visible = true;
                btnRestablecer.Enabled = false;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al restablecer contraseña: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }
    }
}
