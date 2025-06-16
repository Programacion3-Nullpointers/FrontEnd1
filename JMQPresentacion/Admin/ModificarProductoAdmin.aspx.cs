using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;

namespace JMQPresentacion.Admin
{
    public partial class ModificarProductoAdmin : System.Web.UI.Page
    {
        private ProductoWSClient daoProducto = new ProductoWSClient();
        private CategoriaWSClient daoCategoria = new CategoriaWSClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

        private void CargarProductos()
        {
            var lista = daoProducto.listaProducto().ToList();
            gvProductos.DataSource = lista;
            gvProductos.DataBind();
        }

        protected void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            producto nuevo = new producto();
            nuevo.nombre = txtNombre.Text;
            nuevo.descripcion = txtDescripcion.Text;
            nuevo.precio = Double.Parse(txtPrecio.Text);
            nuevo.stock = Int32.Parse(txtStock.Text);
            nuevo.activo = true;

            nuevo.categoria = new categoria();
            nuevo.categoria.nombre = txtCategoriaNombre.Text;

            byte[] imagen = (byte[])Session["imagen"];
            if (imagen == null)
            {
                Response.Write("<script>alert('Debe subir una imagen.');</script>");
                return;
            }
            nuevo.imagen = imagen;

            daoProducto.registrarProducto(nuevo);
            CargarProductos();
            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal", "cerrarModal();", true);
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "Eliminar")
            {
                daoProducto.eliminarProducto(id);
                CargarProductos();
            }
            else if (e.CommandName == "Editar")
            {
                producto producto = daoProducto.buscarProducto(id);
                hfIdProd.Value = producto.id.ToString();

                txtNombre.Text = producto.nombre;
                txtCategoriaNombre.Text = producto.categoria?.nombre ?? "";
                txtDescripcion.Text = producto.descripcion;
                txtPrecio.Text = producto.precio.ToString("N2");
                txtStock.Text = producto.stock.ToString();

                Session["imagen"] = producto.imagen;
                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModalModificar", "mostrarModalModificar();", true);
            }
        }

        protected void btnActualizarProducto_Click(object sender, EventArgs e)
        {
            int id = int.Parse(hfIdProd.Value);
            producto producto = daoProducto.buscarProducto(id);

            producto.nombre = txtNombre.Text;
            producto.descripcion = txtDescripcion.Text;
            producto.precio = Double.Parse(txtPrecio.Text);
            producto.stock = Int32.Parse(txtStock.Text);
            producto.categoria = new categoria();
            producto.categoria.nombre = txtCategoriaNombre.Text;

            var nuevaImagen = CargarFoto();
            producto.imagen = nuevaImagen ?? (byte[])Session["imagen"];

            daoProducto.actualizarProducto(producto);
            CargarProductos();
            ScriptManager.RegisterStartupScript(this, GetType(), "cerrarModalModificar", "cerrarModalModificar();", true);
        }

        protected void Cargar_Foto(object sender, EventArgs e)
        {
            if (IsPostBack && fileUploadImgProducto.PostedFile != null && fileUploadImgProducto.HasFile)
            {
                string extension = System.IO.Path.GetExtension(fileUploadImgProducto.FileName);
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png" || extension.ToLower() == ".gif")
                {
                    string filename = Guid.NewGuid().ToString() + extension;
                    string filePath = Server.MapPath("~/Uploads/") + filename;
                    fileUploadImgProducto.SaveAs(filePath);
                    imgProducto.ImageUrl = "~/Uploads/" + filename;
                    imgProducto.Visible = true;
                    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    Session["imagen"] = br.ReadBytes((int)fs.Length);
                    fs.Close();
                }
                else
                {
                    Response.Write("Por favor, selecciona un archivo de imagen válido.");
                }
            }
        }

        private byte[] CargarFoto()
        {
            if (fileUploadImgProducto.HasFile)
            {
                string extension = Path.GetExtension(fileUploadImgProducto.FileName).ToLower();
                if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif")
                {
                    using (var br = new BinaryReader(fileUploadImgProducto.PostedFile.InputStream))
                    {
                        return br.ReadBytes(fileUploadImgProducto.PostedFile.ContentLength);
                    }
                }
                else
                {
                    throw new System.Exception("Formato de imagen no válido. Solo se permiten JPG, PNG, JPEG y GIF.");
                }
            }
            return null;
        }

        protected void lbRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Admin/PrincipalAdmin.aspx");
        }
    }
}
