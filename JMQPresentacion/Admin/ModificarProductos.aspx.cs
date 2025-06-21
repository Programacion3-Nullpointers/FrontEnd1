using JMQPresentacion.JMQWS;
using System;
using System.Collections.Generic;
using System.IO;
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
            // Validar si el usuario ha iniciado sesión
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Acceso/NoAutorizado.aspx");
                return;
            }
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
                if (fuImagen.HasFile)
                {
                    nuevo.imagen = fuImagen.FileBytes;
                }
                else
                {
                    string rutaImagen = Server.MapPath("~/Public/images/imagen_default.jpg");
                    nuevo.imagen = System.IO.File.ReadAllBytes(rutaImagen);
                }
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

        protected void btnCargarFoto_Click(object sender, EventArgs e)
        {
            if (fileUploadFotoProducto.HasFile)
            {
                string extension = Path.GetExtension(fileUploadFotoProducto.FileName).ToLower();
                if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif")
                {
                    string filename = Guid.NewGuid().ToString() + extension;
                    string ruta = Server.MapPath("~/Public/images/") + filename;

                    fileUploadFotoProducto.SaveAs(ruta);

                    imgPreviewMod.ImageUrl = "~/Public/images/" + filename;

                    using (FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read))
                    {
                        BinaryReader br = new BinaryReader(fs);
                        Session["foto"] = br.ReadBytes((int)fs.Length);
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModalModificar", "mostrarModalModificar();", true);
                }
                else
                {
                    // Puede usarse un mensaje de alerta si lo deseas
                }
            }
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

                    // Obtener el producto desde el servicio
                    var prod = productoService.buscarProducto(id);

                    if (prod != null)
                    {
                        hfIdProd.Value = prod.id.ToString();
                        txtNombreMod.Text = prod.nombre;
                        txtCategoriaMod.Text = prod.categoria.nombre;
                        txtDescripcionMod.Text = prod.descripcion;
                        txtPrecioMod.Text = prod.precio.ToString();
                        txtStockMod.Text = prod.stock.ToString();

                        // ✅ Checkbox "Activo"
                        chkActivoMod.Checked = prod.activo;

                        // ✅ Cargar imagen previa
                        if (prod.imagen != null && prod.imagen.Length > 0)
                        {
                            string base64 = Convert.ToBase64String(prod.imagen);
                            imgPreviewMod.ImageUrl = "data:image/png;base64," + base64;
                        }
                        else
                        {
                            //string relativePath = "~/Public/images/imagen_default.jpg";
                            imgPreviewMod.ImageUrl = Page.ResolveUrl("~/Public/images/imagen_default.jpg");
                        }

                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModalModificar", "mostrarModalModificar();", true);
                    }
                }

            }
        }

        protected void btnActualizarProducto_Click(object sender, EventArgs e)
        {
            List<producto> lista = Session["Productos"] as List<producto>;
            if (lista == null) return;

            int id = int.Parse(hfIdProd.Value);
            producto prod = lista.FirstOrDefault(u => u.id == id);

            if (prod != null)
            {
                prod.nombre = txtNombreMod.Text;

                prod.categoria = new categoria
                {
                    id = 1,
                    nombre = txtCategoriaMod.Text
                };
                prod.descripcion = txtDescripcionMod.Text;

                // ✅ Asignar nueva imagen si se subió
                if (fuImagen.HasFile)
                {
                    prod.imagen = fuImagen.FileBytes;
                }

                prod.precio = Convert.ToDouble(txtPrecioMod.Text);
                prod.stock = Convert.ToInt32(txtStockMod.Text);
                prod.activo = chkActivoMod.Checked;

                if (Session["foto"] != null)
                {
                    prod.imagen = (byte[])Session["foto"];
                }


                productoService.actualizarProducto(prod);
            }

            Session["Productos"] = lista;
            gvProductos.DataSource = lista.OrderBy(u => u.id).ToList();
            gvProductos.DataBind();

            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModalModificar", "cerrarModalModificar();", true);
        }


    }
}