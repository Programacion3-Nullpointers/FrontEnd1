using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQDominio;

namespace JMQPresentacion.Login
{
    public partial class Registrarse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void rblTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlEmpresa.Visible = rbEmpresa.Checked;
            pnlCliente.Visible = rbCliente.Checked;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            divError.Style["display"] = "none";
            lblError.Text = "";

            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtContr.Text) ||
                string.IsNullOrWhiteSpace(txtContrConf.Text) ||
                (pnlEmpresa.Visible && (string.IsNullOrWhiteSpace(txtRazonSocial.Text) || string.IsNullOrWhiteSpace(txtRUC.Text))) ||
                (pnlCliente.Visible && string.IsNullOrWhiteSpace(txtDNI.Text)))
            {
                lblError.Text = "Complete todos los campos.";
                divError.Style["display"] = "block";
                return;
            }

            if (txtContr.Text != txtContrConf.Text)
            {
                lblError.Text = "La contraseña no coincide.";
                divError.Style["display"] = "block";
                return;
            }

            if (pnlEmpresa.Visible && txtRUC.Text.Length != 11)
            {
                lblError.Text = "RUC inválido.";
                divError.Style["display"] = "block";
                return;
            }

            if (pnlCliente.Visible && txtDNI.Text.Length != 8)
            {
                lblError.Text = "DNI inválido.";
                divError.Style["display"] = "block";
                return;
            }
            // Código para procesar el formulario
            // Verifica correo
            /*
            bool correoEnUso = ...
            if (correoEnUso)
            {
                lblError.Text = "El correo ingresado ya se encuentra registrado.";
                divError.Style["display"] = "block";
                return;
            }
            */

            // insertar usuario en la base de datos
            // ...
            Usuario user = null; // cambiar por el usuario recién ingresado
            Session["Usuario"] = user;
            Response.Redirect("/Pedidos/DatosEntrega.aspx");
        }

    }
}