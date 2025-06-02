using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMQPresentacion.Login
{
    public partial class OlvidarContraseña : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (IsEmailRegistered(email)) // Verifica si el correo está registrado
            {
                string resetLink = GenerateResetLink(email); // Genera el enlace de recuperación
                SendResetEmail(email, resetLink); // Envía el correo
                lblError.Text = "Hemos enviado un enlace de recuperación a tu correo.";
                divError.Attributes["class"] = "alert alert-success"; // Muestra mensaje de éxito
            }
            else
            {
                lblError.Text = "Este correo no está registrado.";
                divError.Style["display"] = "block"; // Muestra error si el correo no existe
            }
        }

        // Simulación de validación de email en base de datos
        private bool IsEmailRegistered(string email)
        {
            // Aquí consultarías la base de datos
            return email == "usuario@example.com"; // Simulación de un correo registrado
        }

        // Generación del enlace de recuperación
        private string GenerateResetLink(string email)
        {
            return null; // Simulación de enlace único
        }

        // Simulación de envío de correo
        private void SendResetEmail(string email, string resetLink)
        {
            // Aquí enviarías el correo con SMTP, SendGrid, etc.
        }


    }
}