using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;

namespace JMQPresentacion.Usuarios
{
    public partial class ModificarUsuarios : System.Web.UI.Page
    {
        private UsuarioWSClient usuarioWSCLClient;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || ((usuario)Session["Usuario"]).tipoUsuario != tipoUsuario.ADMIN)
            {
                // Redirigir al login u otra acción
                Response.Redirect("~/Login/Login.aspx");
                return;
            }
            usuarioWSCLClient = new JMQWS.UsuarioWSClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Acceso/NoAutorizado.aspx");
                return;
            }
            if (!IsPostBack)
            {
                if (Session["Usuarios"] == null)
                {
                    List<usuario> listaInicial = usuarioWSCLClient.listarUsuarios().ToList();
                    Session["Usuarios"] = listaInicial;
                }

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
                int nuevoId = Enumerable.Range(1, lista.Count + 1)
                        .Except(lista.Select(u => u.id))
                        .First();

                usuario nuevo = new usuario();
                nuevo.id = nuevoId;
                nuevo.nombreUsuario = txtNombreUsuario.Text;
                nuevo.contrasena = "";
                nuevo.correo = txtCorreo.Text;
                nuevo.tipoUsuario = new tipoUsuario();
                nuevo.razonsocial = txtRazonSocial.Text;
                nuevo.direccion = txtDireccion.Text;
                nuevo.RUC = txtRUC.Text;
                usuarioWSCLClient.registrarUsuario(nuevo);
                lista.Add(nuevo);
            }

            Session["Usuarios"] = lista;
            gvUsuarios.DataSource = lista.OrderBy(u => u.id).ToList();
            gvUsuarios.DataBind();

            txtNombreUsuario.Text = "";
            txtCorreo.Text = "";
            txtRazonSocial.Text = "";
            txtDireccion.Text = "";
            txtRUC.Text = "";

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
                        lista.Remove(usuarioAEliminar);
                        Session["Usuarios"] = lista;
                        usuarioWSCLClient.eliminarUsuario(id);

                        gvUsuarios.DataSource = lista.OrderBy(u => u.id).ToList();
                        gvUsuarios.DataBind();
                    }
                }

                if (e.CommandName == "Editar")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    var usuario = usuarioWSCLClient.buscarUsuario(id);

                    if (usuario != null)
                    {
                        hfIdUsuario.Value = usuario.id.ToString();
                        txtNombreUsuarioMod.Text = usuario.nombreUsuario;
                        txtCorreoMod.Text = usuario.correo;
                        txtRazonSocialMod.Text = usuario.razonsocial;
                        txtDireccionMod.Text = usuario.direccion;
                        txtRUCMod.Text = usuario.RUC;

                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModalModificar", "mostrarModalModificar();", true);
                    }
                }
                if (e.CommandName == "VerPedidos")
                {
                    int idUsuario = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect($"VerPedidosAdmin.aspx?idUsuario={idUsuario}");
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

            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModalModificar", "cerrarModalModificar();", true);
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string textoBusqueda = txtBuscar.Text.Trim().ToLower();

            if (Session["Usuarios"] != null)
            {
                List<usuario> listaOriginal = Session["Usuarios"] as List<usuario>;

                var listaFiltrada = listaOriginal.Where(u =>
                    (!string.IsNullOrEmpty(u.nombreUsuario) && u.nombreUsuario.ToLower().Contains(textoBusqueda)) ||
                    (!string.IsNullOrEmpty(u.dni) && u.dni.ToLower().Contains(textoBusqueda)) ||
                    (!string.IsNullOrEmpty(u.RUC) && u.RUC.ToLower().Contains(textoBusqueda))
                ).ToList();

                if (listaFiltrada.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "sinResultados", "alert('No se encontraron coincidencias.');", true);
                }

                gvUsuarios.DataSource = listaFiltrada.OrderBy(u => u.id).ToList();
                gvUsuarios.DataBind();
            }
        }


        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string tipoEntidad = ddlTipoEntidad.SelectedValue;

            // Valor por defecto: true
            bool activo = true;
            if (ddlActivo.SelectedValue == "false") activo = false;

            var resultado = usuarioWSCLClient.filtrarUsuarios(tipoEntidad, activo);
            List<usuario> listaFiltrada = resultado != null ? resultado.ToList() : new List<usuario>();

            if (listaFiltrada.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "sinResultados", "alert('No se encontraron usuarios con esos filtros.');", true);
            }

            Session["Usuarios"] = listaFiltrada;
            gvUsuarios.DataSource = listaFiltrada.OrderBy(u => u.id).ToList();
            gvUsuarios.DataBind();
        }



        protected void btnResetFiltros_Click(object sender, EventArgs e)
        {
            ddlTipoEntidad.SelectedIndex = 0;
            ddlActivo.SelectedIndex = 0;
            txtBuscar.Text = ""; // Limpiar campo de búsqueda

            List<usuario> lista = usuarioWSCLClient.listarUsuarios().ToList();
            Session["Usuarios"] = lista;

            gvUsuarios.DataSource = lista.OrderBy(u => u.id).ToList();
            gvUsuarios.DataBind();
        }

    }
}
