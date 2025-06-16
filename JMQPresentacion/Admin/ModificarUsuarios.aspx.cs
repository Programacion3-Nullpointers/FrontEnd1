using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;

namespace JMQPresentacion.Usuarios
{
    public partial class ModificarUsuarios : System.Web.UI.Page
    {
        private UsuarioWSClient UsuarioService => new UsuarioWSClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            var lista = UsuarioService.listarUsuarios().ToList();
            gvUsuarios.DataSource = lista.OrderBy(u => u.id).ToList();
            gvUsuarios.DataBind();
        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            var nuevo = new usuario
            {
                nombreUsuario = txtNombreUsuario.Text,
                contrasena = "", // Asignar si aplica
                correo = txtCorreo.Text,
                tipoUsuario = new tipoUsuario(),
                razonsocial = txtRazonSocial.Text,
                direccion = txtDireccion.Text,
                RUC = txtRUC.Text
            };

            UsuarioService.registrarUsuario(nuevo);
            CargarUsuarios();

            // Limpiar campos
            txtNombreUsuario.Text = "";
            txtCorreo.Text = "";
            txtRazonSocial.Text = "";
            txtDireccion.Text = "";
            txtRUC.Text = "";

            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal", "cerrarModal();", true);
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString()); // ✅ ← AQUÍ estaba el problema
            Response.Write($"<script>console.log('ID recibido: {id}');</script>");

            if (e.CommandName == "Eliminar")
            {
                UsuarioService.eliminarUsuario(id);
                CargarUsuarios();
            }

            if (e.CommandName == "Editar")
            {
                var usuario = UsuarioService.buscarUsuario(id);

                if (usuario == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alerta", $"alert('No se encontró el usuario con ID {id}.');", true);
                    return;
                }

                // ✅ Guardar el ID en el hidden field para cuando presionen “Actualizar”
                IdUsuario.Value = usuario.id.ToString();
                txtNombreUsuarioMod.Text = usuario.nombreUsuario ?? "";
                txtCorreoMod.Text = usuario.correo ?? "";
                txtRazonSocialMod.Text = usuario.razonsocial ?? "";
                txtDireccionMod.Text = usuario.direccion ?? "";
                txtRUCMod.Text = usuario.RUC ?? "";

                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModalModificar", "mostrarModalModificar();", true);
            }
        }

        protected void btnActualizarUsuario_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IdUsuario.Value)) return;

            int id = int.Parse(IdUsuario.Value);
            var usuario = UsuarioService.buscarUsuario(id);

            if (usuario != null)
            {
                usuario.nombreUsuario = txtNombreUsuarioMod.Text;
                usuario.correo = txtCorreoMod.Text;
                usuario.razonsocial = txtRazonSocialMod.Text;
                usuario.direccion = txtDireccionMod.Text;
                usuario.RUC = txtRUCMod.Text;

                UsuarioService.actualizarUsuario(usuario);
            }

            CargarUsuarios();
            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModalModificar", "cerrarModalModificar();", true);
        }
    }
}
