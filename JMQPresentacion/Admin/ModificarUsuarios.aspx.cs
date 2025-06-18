using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;
using System.Linq;


namespace JMQPresentacion.Usuarios
{
    public partial class ModificarUsuarios : System.Web.UI.Page
    {
        private UsuarioWSClient usuarioWSCLClient;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login/Login.aspx");
            }
            usuarioWSCLClient = new JMQWS.UsuarioWSClient();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Si no hay lista guardada en sesión, inicialízala con algunos datos de ejemplo
                if (Session["Usuarios"] == null)
                {
                    List<usuario> listaInicial = new List<usuario>();
                    listaInicial = usuarioWSCLClient.listarUsuarios().ToList();


                    Session["Usuarios"] = listaInicial;
                }

                // Mostrar la tabla
                List<usuario> lista = Session["Usuarios"] as List<usuario>;
                gvUsuarios.DataSource = lista.OrderBy(u => u.id).ToList();
                gvUsuarios.DataBind();
            }

        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            List<usuario> lista = Session["Usuarios"] as List<usuario>;
            if (lista == null) lista = new List<usuario>();

            int idEditar = ViewState["EditarId"] != null ? Convert.ToInt32(ViewState["EditarId"]) : 0;

            if (idEditar > 0)
            {
                // Editar usuario existente
                usuario user = lista.FirstOrDefault(x => x.id == idEditar);
                if (user != null)
                {
                    user.nombreUsuario = txtNombreUsuario.Text;
                    user.correo = txtCorreo.Text;
                    user.razonsocial = txtRazonSocial.Text;
                    user.direccion = txtDireccion.Text;
                    user.RUC = txtRUC.Text;
                }

                ViewState["EditarId"] = null;
            }
            else
            {
                // ➕ Buscar el menor ID disponible
                int nuevoId = Enumerable.Range(1, lista.Count + 1)
                        .Except(lista.Select(u => u.id))
                        .First();

                usuario nuevo = new usuario();
                nuevo.id = nuevoId;
                nuevo.nombreUsuario = txtNombreUsuario.Text;
                nuevo.contrasena = ""; // contraseña vacía, según lo que indicaste
                nuevo.correo = txtCorreo.Text;
                nuevo.tipoUsuario = new tipoUsuario(); // o asigná un valor real si lo tenés
                nuevo.razonsocial = txtRazonSocial.Text;
                nuevo.direccion = txtDireccion.Text;
                nuevo.RUC = txtRUC.Text;
                usuarioWSCLClient.registrarUsuario(nuevo);
                lista.Add(nuevo);
            }

            // Guardar y actualizar
            Session["Usuarios"] = lista;
            gvUsuarios.DataSource = lista.OrderBy(u => u.id).ToList();
            gvUsuarios.DataBind();

            // Limpiar
            txtNombreUsuario.Text = "";
            txtCorreo.Text = "";
            txtRazonSocial.Text = "";
            txtDireccion.Text = "";
            txtRUC.Text = "";

            // Cerrar modal
            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal", "cerrarModal();", true);
        }


        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (Session["Usuarios"] != null)
            {
                List<usuario> lista = (List<usuario>)Session["Usuarios"];

                if (e.CommandName == "Eliminar")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    usuario usuarioAEliminar = lista.FirstOrDefault(u => u.id == id);

                    if (usuarioAEliminar != null)
                    {
                        lista.Remove(usuarioAEliminar); // Eliminar de la lista
                        Session["Usuarios"] = lista;     // Guardar la lista actualizada en la sesión
                        usuarioWSCLClient.eliminarUsuario(id);

                        gvUsuarios.DataSource = lista.OrderBy(u => u.id).ToList();
                        gvUsuarios.DataBind();
                        // Refrescar la tabla
                    }
                }

                if (e.CommandName == "Editar")
                {
                    int id = Convert.ToInt32(e.CommandArgument);

                    // Obtener los datos del usuario desde la base de datos
                    var usuario = usuarioWSCLClient.buscarUsuario(id); // Este método lo defines tú

                    if (usuario != null)
                    {
                        hfIdUsuario.Value = usuario.id.ToString();
                        txtNombreUsuarioMod.Text = usuario.nombreUsuario;
                        txtCorreoMod.Text = usuario.correo;
                        txtRazonSocialMod.Text = usuario.razonsocial;
                        txtDireccionMod.Text = usuario.direccion;
                        txtRUCMod.Text = usuario.RUC;

                        // Mostrar modal de modificación
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModalModificar", "mostrarModalModificar();", true);

                    }
                }
            }
        }

        protected void btnActualizarUsuario_Click(object sender, EventArgs e)
        {
            List<usuario> lista = Session["Usuarios"] as List<usuario>;
            if (lista == null) return;

            int id = int.Parse(hfIdUsuario.Value);
            usuario user = lista.FirstOrDefault(u => u.id == id);

            if (user != null)
            {
                user.nombreUsuario = txtNombreUsuarioMod.Text;
                user.correo = txtCorreoMod.Text;
                user.razonsocial = txtRazonSocialMod.Text;
                user.direccion = txtDireccionMod.Text;
                user.RUC = txtRUCMod.Text;

                 usuarioWSCLClient.actualizarUsuario(user);
            }

            Session["Usuarios"] = lista;
            gvUsuarios.DataSource = lista.OrderBy(u => u.id).ToList();
            gvUsuarios.DataBind();

            // Cerrar modal
            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModalModificar", "cerrarModalModificar();", true);
        }
    }
}