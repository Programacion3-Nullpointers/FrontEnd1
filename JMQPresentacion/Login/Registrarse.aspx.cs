using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;
using System.Text.RegularExpressions;

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

            if (!Regex.IsMatch(txtNombre.Text, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$") ||
                !Regex.IsMatch(txtApellido.Text, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
            {
                lblError.Text = "El nombre y apellido solo deben contener letras.";
                divError.Style["display"] = "block";
                return;
            }

            if (txtNombre.Text.Trim().Length < 2 || txtApellido.Text.Trim().Length < 2)
            {
                lblError.Text = "El nombre y apellido deben tener al menos 2 caracteres.";
                divError.Style["display"] = "block";
                return;
            }

            if (txtDireccion.Text.Trim().Length < 4)
            {
                lblError.Text = "La dirección debe tener al menos 4 caracteres.";
                divError.Style["display"] = "block";
                return;
            }

            if (pnlEmpresa.Visible && !Regex.IsMatch(txtRazonSocial.Text, @"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s\.\-&]{2,}$"))
            {
                lblError.Text = "La razón social solo puede contener letras, números, espacios y los símbolos '.', '-' y '&'.";
                divError.Style["display"] = "block";
                return;
            }

            if (pnlEmpresa.Visible)
            {
                if (!Regex.IsMatch(txtRUC.Text, @"^\d+$"))
                {
                    lblError.Text = "El RUC solo debe contener números.";
                    divError.Style["display"] = "block";
                    return;
                }

                if (txtRUC.Text.Length != 11)
                {
                    lblError.Text = "RUC inválido.";
                    divError.Style["display"] = "block";
                    return;
                }
            }

            if (pnlCliente.Visible)
            {
                if (!Regex.IsMatch(txtDNI.Text, @"^\d+$"))
                {
                    lblError.Text = "El DNI solo debe contener números.";
                    divError.Style["display"] = "block";
                    return;
                }

                if (txtDNI.Text.Length != 8)
                {
                    lblError.Text = "DNI inválido.";
                    divError.Style["display"] = "block";
                    return;
                }
            }

            if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                lblError.Text = "El formato del correo electrónico no es válido.";
                divError.Style["display"] = "block";
                return;
            }

            // Crear usuario
            usuario user = new usuario();
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

            usuario uss = usuarioWSCLClient.BuscarUsuarioPorCorreo(user.correo.ToString());
            if (uss != null)
            {
                lblError.Text = "El correo ingresado ya se encuentra registrado.";
                divError.Style["display"] = "block";
                return;
            }

            // Insertar usuario
            usuarioWSCLClient.registrarUsuario(user);

            // Guardar en sesión y redirigir
            Session["Usuario"] = user;

            // Nombre a mostrar en mensaje
            string nombreMostrar = user.nombreUsuario.Split(' ')[0];
            string destino = "/Principal/Principal.aspx";

            if (Session["RedirectAfterLogin"] != null)
            {
                destino = Session["RedirectAfterLogin"].ToString();
                Session.Remove("RedirectAfterLogin");
            }

            // Mostrar mensaje de bienvenida y redirigir
            string script = $@"
                Swal.fire({{
                    icon: 'success',
                    title: '¡Bienvenido, {nombreMostrar}!',
                    text: 'Tu registro fue exitoso.',
                    timer: 1800,
                    showConfirmButton: false
                }});
                setTimeout(function() {{
                    window.location.href = '{destino}';
                }}, 1800);";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "bienvenidaRegistro", script, true);
        }
    }
}
