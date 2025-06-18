using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;


namespace JMQPresentacion.Login
{
    public partial class Registrarse : System.Web.UI.Page
    {
        private UsuarioWSClient usuarioWSCLClient;

        protected void Page_Init(object sender, EventArgs e)
        {
            usuarioWSCLClient = new JMQWS.UsuarioWSClient();
        }
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

            // Crear usuario
            usuario user = new usuario ();
            user.nombreUsuario = txtNombre.Text.Trim() + " " + txtApellido.Text.Trim();
            user.direccion = txtDireccion.Text.Trim();
            user.correo = txtEmail.Text.Trim();
            user.contrasena = txtContr.Text.Trim();
            user.tipoUsuario = pnlEmpresa.Visible ? tipoUsuario.EMPRESA : tipoUsuario.CLIENTE;
            user.tipoUsuarioSpecified = true;
            user.activo = true;

            if (user.tipoUsuario == tipoUsuario.EMPRESA)
            {
                user.razonsocial = txtRazonSocial.Text.Trim();
                user.RUC = txtRUC.Text.Trim();
            }
            else
            {
                user.dni = txtDNI.Text.Trim();
            }

            // Verificar si el correo ya existe
            usuario uss = usuarioWSCLClient.BuscarUsuarioPorCorreo(user.correo.ToString()); // Asume que este método existe en el WebService
            if (uss != null)
            {
                lblError.Text = "El correo ingresado ya se encuentra registrado.";
                divError.Style["display"] = "block";
                return;
            }

            // Insertar usuario
            usuarioWSCLClient.registrarUsuario(user); // Asume que este método inserta y devuelve el usuario creado

            // Guardar en sesión y redirigir
            Session["Usuario"] = user;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModal", "mostrarModalRegistro();", true);

<<<<<<< Updated upstream
            Response.Redirect("/Principal/Principal.aspx");
=======
            Response.Redirect("/Pedidos/DatosEntrega.aspx");
>>>>>>> Stashed changes

        }
    }
}