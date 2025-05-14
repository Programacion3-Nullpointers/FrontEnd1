using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQDominio;

namespace Front_Admin.Usuarios
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Si no hay lista guardada en sesión, inicialízala con algunos datos de ejemplo
                if (Session["Usuarios"] == null)
                {
                        List<Usuario> listaInicial = new List<Usuario>
                {
                    new Usuario(1, "admin01", "123456", true, "admin@jmq.com", new TipoUsuario(), "JMQ SAC", "Av. Principal 123", "20604010123"),
                    new Usuario(2, "user02", "abc123", true, "user@jmq.com", new TipoUsuario(), "JMQ Ferretería", "Jr. Comercio 456", "20101020304")
                };

                    Session["Usuarios"] = listaInicial;
                }

                // Mostrar la tabla
                List<Usuario> lista = Session["Usuarios"] as List<Usuario>;
                gvUsuarios.DataSource = lista.OrderBy(u => u.id).ToList();
                gvUsuarios.DataBind();
            }

        }

            protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            List<Usuario> lista = Session["Usuarios"] as List<Usuario>;
            if (lista == null) lista = new List<Usuario>();

            int idEditar = ViewState["EditarId"] != null ? Convert.ToInt32(ViewState["EditarId"]) : 0;

            if (idEditar > 0)
            {
                // Editar usuario existente
                Usuario usuario = lista.FirstOrDefault(x => x.id == idEditar);
                if (usuario != null)
                {
                    usuario.nombreUsuario = txtNombreUsuario.Text;
                    usuario.correo = txtCorreo.Text;
                    usuario.razonsocial = txtRazonSocial.Text;
                    usuario.direccion = txtDireccion.Text;
                    usuario.RUC = txtRUC.Text;
                }

                ViewState["EditarId"] = null;
            }
            else
            {
                // ➕ Buscar el menor ID disponible
                int nuevoId = Enumerable.Range(1, lista.Count + 1)
                                        .Except(lista.Select(u => u.id))
                                        .First();

                Usuario nuevo = new Usuario(
                    nuevoId,
                    txtNombreUsuario.Text,
                    "", // contraseña vacía
                    true,
                    txtCorreo.Text,
                    new TipoUsuario(),
                    txtRazonSocial.Text,
                    txtDireccion.Text,
                    txtRUC.Text
                );

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
                List<Usuario> lista = (List<Usuario>)Session["Usuarios"];

                if (e.CommandName == "Eliminar")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    Usuario usuarioAEliminar = lista.FirstOrDefault(u => u.id == id);

                    if (usuarioAEliminar != null)
                    {
                        lista.Remove(usuarioAEliminar); // Eliminar de la lista
                        Session["Usuarios"] = lista;     // Guardar la lista actualizada en la sesión

                        gvUsuarios.DataSource = lista.OrderBy(u => u.id).ToList();
                        gvUsuarios.DataBind();
                        // Refrescar la tabla
                    }
                }

                if (e.CommandName == "Editar")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    Usuario usuarioAEditar = lista.FirstOrDefault(u => u.id == id);

                    if (usuarioAEditar != null)
                    {
                        txtNombreUsuario.Text = usuarioAEditar.nombreUsuario;
                        txtCorreo.Text = usuarioAEditar.correo;
                        txtRazonSocial.Text = usuarioAEditar.razonsocial;
                        txtDireccion.Text = usuarioAEditar.direccion;
                        txtRUC.Text = usuarioAEditar.RUC;

                        ViewState["EditarId"] = id;

                        ScriptManager.RegisterStartupScript(this, GetType(), "MostrarModal", "mostrarModal();", true);
                    }
                }
            }
        }

    }


}
