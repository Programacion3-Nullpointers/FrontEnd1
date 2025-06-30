using JMQPresentacion.JMQWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMQPresentacion.Admin
{
    public partial class ModificarCategorias : System.Web.UI.Page
    {
        private CategoriaWSClient categoriaWSClient;
        private DescuentoWSClient descuentoWSClient;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || ((usuario)Session["Usuario"]).tipoUsuario != tipoUsuario.ADMIN)
            {
                // Redirigir al login u otra acción
                Response.Redirect("~/Login/Login.aspx");
                return;
            }
            categoriaWSClient = new JMQWS.CategoriaWSClient();
            descuentoWSClient = new DescuentoWSClient();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Si no hay lista guardada en sesión, inicialízala con algunos datos de ejemplo
                if (Session["Categorias"] == null)
                {
                    List<categoria> listaInicial = new List<categoria>();
                    listaInicial = categoriaWSClient.ListarCategorias().ToList();


                    Session["Categorias"] = listaInicial;
                }

                // Mostrar la tabla
                List<categoria> lista = Session["Categorias"] as List<categoria>;
                gvCategorias.DataSource = lista.OrderBy(u => u.id).ToList();
                gvCategorias.DataBind();


                // Cargar descuentos al DropDownList
                List<descuento> descuentos = descuentoWSClient.ListarDescuentos().ToList();
                ddlDescuento.DataSource = descuentos;
                ddlDescuento.DataTextField = "numDescuento";
                ddlDescuento.DataValueField = "id";
                ddlDescuento.DataBind();

                ddlDescuentoMod.DataSource = descuentos;
                ddlDescuentoMod.DataTextField = "numDescuento";
                ddlDescuentoMod.DataValueField = "id";
                ddlDescuentoMod.DataBind();
            }
        }

        protected void btnGuardarCategoria_Click(object sender, EventArgs e)
        {
            List<categoria> lista = Session["Categorias"] as List<categoria>;
            if (lista == null) lista = new List<categoria>();

            int idEditar = ViewState["EditarId"] != null ? Convert.ToInt32(ViewState["EditarId"]) : 0;

            if (idEditar > 0)
            {
                // Editar usuario existente
                categoria user = lista.FirstOrDefault(x => x.id == idEditar);
                if (user != null)
                {
                    user.nombre = txtnombre.Text;
                    user.descripcion = txtdescripcion.Text;
                    int idDescuento = Convert.ToInt32(ddlDescuento.SelectedValue);

                    user.descuento = descuentoWSClient.BuscarDescuento(idDescuento);


                }

                ViewState["EditarId"] = null;
            }
            else
            {
                // ➕ Buscar el menor ID disponible
                int nuevoId = Enumerable.Range(1, lista.Count + 1)
                        .Except(lista.Select(u => u.id))
                        .First();

                categoria nuevo = new categoria();
                nuevo.id = nuevoId;
                nuevo.descripcion = txtdescripcion.Text;
                nuevo.nombre = txtnombre.Text;

                int idDescuento = Convert.ToInt32(ddlDescuento.SelectedValue);
                nuevo.descuento = descuentoWSClient.BuscarDescuento(idDescuento);


                categoriaWSClient.RegistrarCategoria(nuevo);
                lista.Add(nuevo);
            }

            // Guardar y actualizar
            Session["Categorias"] = lista;
            gvCategorias.DataSource = lista.OrderBy(u => u.id).ToList();
            gvCategorias.DataBind();

            // Limpiar
            txtdescripcion.Text = "";
            txtnombre.Text = "";
            ddlDescuento.Text = "";
            //DescuentoWSClient descuentoWSClient = new DescuentoWSClient();
            
            // Cerrar modal
            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal", "cerrarModal();", true);
        }

        protected void gvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (Session["Categorias"] != null)
            {
                List<categoria> lista = (List<categoria>)Session["Categorias"];

                if (e.CommandName == "Eliminar")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    categoria usuarioAEliminar = lista.FirstOrDefault(u => u.id == id);

                    if (usuarioAEliminar != null)
                    {
                        lista.Remove(usuarioAEliminar); // Eliminar de la lista
                        Session["Descuentos"] = lista;     // Guardar la lista actualizada en la sesión
                        categoriaWSClient.EliminarCategoria(id);

                        gvCategorias.DataSource = lista.OrderBy(u => u.id).ToList();
                        gvCategorias.DataBind();
                        // Refrescar la tabla
                    }
                }

                if (e.CommandName == "Editar")
                {
                    int id = Convert.ToInt32(e.CommandArgument);

                    // Obtener los datos del usuario desde la base de datos
                    var usuario = categoriaWSClient.ObtenerCategoria(id); // Este método lo defines tú

                    if (usuario != null)
                    {
                        hfIdCategoria.Value = usuario.id.ToString();
                        txtdescripcionMod.Text = usuario.descripcion.ToString();
                        txtnombreMod.Text = usuario.nombre.ToString();

                        ddlDescuentoMod.SelectedValue = usuario.descuento.id.ToString();

                        // Mostrar modal de modificación
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModalModificar", "mostrarModalModificar();", true);

                    }
                }
            }
        }
        protected void btnActualizarCategoria_Click(object sender, EventArgs e)
        {
            List<categoria> lista = Session["Categorias"] as List<categoria>;
            if (lista == null) return;

            int id = int.Parse(hfIdCategoria.Value);
            categoria user = lista.FirstOrDefault(u => u.id == id);

            if (user != null)
            {
                user.descripcion = txtdescripcionMod.Text;
                user.nombre = txtnombreMod.Text;
                int idDescuento = Convert.ToInt32(ddlDescuentoMod.SelectedValue);
                user.descuento = descuentoWSClient.BuscarDescuento(idDescuento);


                categoriaWSClient.ActualizarCategoria(user);
            }

            Session["Descuentos"] = lista;
            gvCategorias.DataSource = lista.OrderBy(u => u.id).ToList();
            gvCategorias.DataBind();
            ddlDescuento.ClearSelection(); // si usas AutoPostBack

            // Cerrar modal
            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModalModificar", "cerrarModalModificar();", true);
        }

    }
}