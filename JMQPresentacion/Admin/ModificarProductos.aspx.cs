using JMQPresentacion.JMQWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMQPresentacion.Admin
{
    public partial class ModificarProductos : System.Web.UI.Page
    {
        private ProductoWSClient productoService;
        private CategoriaWSClient categoriaService;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
            Response.Redirect("~/Login.aspx");
            }

            productoService = new JMQWS.ProductoWSClient();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Si no hay lista guardada en sesión, inicialízala con algunos datos de ejemplo
                if (Session["Productos"] == null)
                {
                    List<producto> listaInicial = new List<producto>();
                    listaInicial = productoService.listaProducto().ToList();
                    Session["Productos"] = listaInicial;
                }

                // Mostrar la tabla
                List<producto> lista = Session["Productos"] as List<producto>;
                gvProductos.DataSource = lista.OrderBy(u => u.id).ToList();
                gvProductos.DataBind();
            }

        }

        protected void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            List<producto> lista = Session["Productos"] as List<producto>;
            if (lista == null) lista = new List<producto>();

            int idEditar = ViewState["EditarId"] != null ? Convert.ToInt32(ViewState["EditarId"]) : 0;

            if (idEditar > 0)
            {
                producto prod = lista.FirstOrDefault(x => x.id == idEditar);
                if (prod != null)
                {
                    prod.nombre = txtNombre.Text;
                    //cambiar a búsqueda de categoría...
                    prod.categoria = new categoria
                    {
                        nombre = txtCategoriaNombre.Text
                    };
                    prod.descripcion = txtDescripcion.Text;
                    prod.imagen = new byte[0];
                    prod.precio = Convert.ToDouble(txtPrecio.Text);
                    prod.stock = Convert.ToInt32(txtStock.Text);
                }

                ViewState["EditarId"] = null;
            }
            else
            {
                // ➕ Buscar el menor ID disponible
                int nuevoId = Enumerable.Range(1, lista.Count + 1)
                        .Except(lista.Select(u => u.id))
                        .First();

                producto nuevo = new producto();
                nuevo.id = nuevoId;
                nuevo.nombre = txtNombre.Text;
                //cambiar a búsqueda de categoría...
                nuevo.categoria = new categoria
                {
                    id = 1,
                    nombre = txtCategoriaNombre.Text
                };
                nuevo.descripcion = txtDescripcion.Text;
                nuevo.imagen = new byte[0];
                nuevo.precio = Convert.ToDouble(txtPrecio.Text);
                nuevo.stock = Convert.ToInt32(txtStock.Text);
                productoService.registrarProducto(nuevo);
                lista.Add(nuevo);
            }

            // Guardar y actualizar
            Session["Productos"] = lista;
            gvProductos.DataSource = lista.OrderBy(u => u.id).ToList();
            gvProductos.DataBind();

            // Limpiar
            txtNombre.Text = "";
            txtCategoriaNombre.Text = "";
            txtDescripcion.Text = "";
            txtImagen.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            // Cerrar modal
            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal", "cerrarModal();", true);
        }


        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (Session["Productos"] != null)
            {
                List<producto> lista = (List<producto>)Session["Productos"];

                if (e.CommandName == "Eliminar")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    producto prodAEliminar = lista.FirstOrDefault(u => u.id == id);

                    if (prodAEliminar != null)
                    {
                        lista.Remove(prodAEliminar); // Eliminar de la lista
                        Session["Productos"] = lista;     // Guardar la lista actualizada en la sesión
                        productoService.eliminarProducto(id);

                        gvProductos.DataSource = lista.OrderBy(u => u.id).ToList();
                        gvProductos.DataBind();
                        // Refrescar la tabla
                    }
                }

                if (e.CommandName == "Editar")
                {
                    int id = Convert.ToInt32(e.CommandArgument);

                    // Obtener los datos del usuario desde la base de datos
                    var prod = productoService.buscarProducto(id);

                    if (prod != null)
                    {
                        hfIdProd.Value = prod.id.ToString();
                        TextBox1.Text = prod.nombre;
                        TextBox2.Text = prod.categoria.nombre;
                        TextBox3.Text = prod.descripcion;
                        TextBox4.Text = "";
                        TextBox5.Text = prod.precio.ToString();
                        TextBox6.Text = prod.stock.ToString();

                        // Mostrar modal de modificación
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModalModificar", "mostrarModalModificar();", true);

                    }
                }
            }
        }

        protected void btnActualizarProducto_Click(object sender, EventArgs e)
        {
            List<producto> lista = Session["Producto"] as List<producto>;
            if (lista == null) return;

            int id = int.Parse(hfIdProd.Value);
            producto prod = lista.FirstOrDefault(u => u.id == id);

            if (prod != null)
            {
                prod.nombre = TextBox1.Text;
                //cambiar a búsqueda de categoría...
                prod.categoria = new categoria
                {
                    nombre = TextBox2.Text
                };
                prod.descripcion = TextBox3.Text;
                //prod.imagen = txtImagen.Text;
                prod.precio = Convert.ToDouble(TextBox5.Text);
                prod.stock = Convert.ToInt32(TextBox6.Text);

                productoService.actualizarProducto(prod);
            }

            Session["Productos"] = lista;
            gvProductos.DataSource = lista.OrderBy(u => u.id).ToList();
            gvProductos.DataBind();

            // Cerrar modal
            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModalModificar", "cerrarModalModificar();", true);
        }
    }
}