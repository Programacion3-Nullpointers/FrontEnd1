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
                return;
            }

            try
            {
                // Llamar al servicio SOAP real
                UsuarioWSClient client = new UsuarioWSClient();
                client.iniciarRecuperacionPassword(email);

                MostrarMensaje("Hemos enviado un enlace de recuperación a tu correo.", true);
            }
            catch (Exception ex)
            {
                // Posible error: usuario no encontrado en backend
                MostrarMensaje("Error: " + ex.Message, false);
            }
        }

        // Simulación de validación de email en base de datos
        private bool IsEmailRegistered(string email)
        {
            // Aquí consultarías la base de datos
            return email == "usuario@example.com"; // Simulación de un correo registrado
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


    }
}