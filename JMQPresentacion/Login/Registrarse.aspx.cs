using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using JMQPresentacion.JMQWS;

namespace JMQPresentacion.Login
{
    public partial class Registrarse : Page
    {
        private UsuarioWSClient usuarioWSCLClient;

        protected void Page_Init(object sender, EventArgs e)
        {
            usuarioWSCLClient = new UsuarioWSClient("UsuarioWSPort");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
            }
        }

        protected void rblTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlEmpresa.Visible = rbEmpresa.Checked;
            pnlCliente.Visible = rbCliente.Checked;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            // Validaciones del lado servidor (refuerzo)
            if (CamposObligatoriosVacios())
            {
                MostrarError("Por favor, complete todos los campos requeridos.");
                return;
            }

            if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MostrarError("Formato de correo inválido.");
                return;
            }

            if (txtContr.Text != txtContrConf.Text)
            {
                MostrarError("Las contraseñas no coinciden.");
                return;
            }

            if (txtContr.Text.Trim().Length < 8)
            {
                MostrarError("La contraseña debe tener al menos 8 caracteres.");
                return;
            }

            // Validaciones específicas por tipo
            if (pnlEmpresa.Visible && !Regex.IsMatch(txtRUC.Text.Trim(), @"^\d{11}$"))
            {
                MostrarError("El RUC debe tener 11 dígitos numéricos.");
                return;
            }

            if (pnlCliente.Visible && !Regex.IsMatch(txtDNI.Text.Trim(), @"^\d{8}$"))
            {
                MostrarError("El DNI debe tener 8 dígitos numéricos.");
                return;
            }

            // Comprobar si el correo ya existe
            var existente = usuarioWSCLClient.BuscarUsuarioPorCorreo(txtEmail.Text.Trim());
            if (existente != null)
            {
                MostrarError("El correo ingresado ya está registrado.");
                return;
            }

            // Crear el usuario
            var user = new usuario
            {
                nombreUsuario = $"{txtNombre.Text.Trim()} {txtApellido.Text.Trim()}",
                direccion = txtDireccion.Text.Trim(),
                correo = txtEmail.Text.Trim(),
                contrasena = txtContr.Text.Trim(),
                tipoUsuario = rbEmpresa.Checked ? tipoUsuario.EMPRESA : tipoUsuario.CLIENTE,
                tipoUsuarioSpecified = true,
                saldo = 0,
                activo = true
            };

            if (rbEmpresa.Checked)
            {
                user.razonsocial = txtRazonSocial.Text.Trim();
                user.RUC = txtRUC.Text.Trim();
            }
            else
            {
                user.dni = txtDNI.Text.Trim();
            }

           
            usuario nuevoUsuario = usuarioWSCLClient.registrarUsuario(user);
            Session["Usuario"] = nuevoUsuario;
            Session["RedirectAfterLogin"] = "/Pedidos/Carrito.aspx";
            
        }

        private bool CamposObligatoriosVacios()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtContr.Text) ||
                string.IsNullOrWhiteSpace(txtContrConf.Text))
                return true;

            if (pnlEmpresa.Visible && (string.IsNullOrWhiteSpace(txtRazonSocial.Text) || string.IsNullOrWhiteSpace(txtRUC.Text)))
                return true;

            if (pnlCliente.Visible && string.IsNullOrWhiteSpace(txtDNI.Text))
                return true;

            return false;
        }

        private void MostrarError(string mensaje)
        {
            // Muestra un alert JS clásico como refuerzo
            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorAlerta", $"alert('{mensaje}');", true);
        }

    }
}
