using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;

namespace JMQPresentacion.Principal
{
    public partial class Principal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }
        private void CargarProductos()
        {
            /*
            string connString = "tu_conexion_a_base_de_datos";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT Nombre, Precio, ImagenURL FROM Productos";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptProductos.DataSource = dt;
                rptProductos.DataBind();
            }
            */
            //Categoria herramientas = new Categoria { idCategoria = 1, nombre = "Herramientas" };
            //Categoria limpieza = new Categoria { idCategoria = 2, nombre = "Limpieza" };
            //Categoria jardineria = new Categoria { idCategoria = 3, nombre = "Jardinería" };
            //Categoria pinturas = new Categoria { idCategoria = 4, nombre = "Pinturas" };

            //List<Producto> productos = new List<Producto>
            //{
            //    new Producto { idProducto = 1, nombre = "Taladro", precio = 84, categoria = herramientas, imagen = "/Public/images/taladro.jpg" },
            //    new Producto { idProducto = 2, nombre = "Detergente LA OCA", precio = 34, categoria = limpieza, imagen = "/Public/images/detergente.jpeg" },
            //    new Producto { idProducto = 3, nombre = "Podadora 3 en 1", precio = 300, categoria = jardineria, imagen = "/Public/images/podadora.jpeg" },
            //    new Producto { idProducto = 4, nombre = "Pintura Latex Pato 4 Galones", precio = 150, categoria = pinturas, imagen = "/Public/images/pinturaLatex.jpeg" },
            //    new Producto { idProducto = 5, nombre = "Pintura Latex Pato 5 Galones", precio = 160, categoria = pinturas, imagen = "/Public/images/pinturaLatex.jpeg" }
            //};

            //rptProductos.DataSource = productos;
                rptProductos.DataBind();
            }

        protected void btnAgregarProductos_Click(object sender, EventArgs e)
        {
            //if (Session["Cart"] == null)
            //{
            //    Session["Cart"] = new List<Detalle>();
            //    Session["Orden"] = new OrdenVenta { Estado_compra = EstadoCompra.pendiente }; // Los demás datos se llenarán después
            //}
            //List<Detalle> cart = (List<Detalle>)Session["Cart"];
            //// Aquí buscaría el producto en la BD
            //Categoria herramientas = new Categoria { idCategoria = 1, nombre = "Herramientas" };
            //Producto producto1 = new Producto { idProducto = 1, nombre = "Taladro", precio = 84, categoria = herramientas, imagen = "/Public/images/taladro.jpg", stock=5};
            //Detalle detalle = new Detalle {producto = producto1, cantidad=1, precio_unitario=producto1.precio};
            //cart.Add(detalle);
            //Session["Cart"] = cart;
            //Response.Redirect("~/Pedidos/Carrito.aspx");
        }

        protected void btnCotizar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Cotizaciones/ListaCotizaciones.aspx");
        }
    }
}