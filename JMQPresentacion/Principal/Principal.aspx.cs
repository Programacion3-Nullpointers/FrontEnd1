using JMQPresentacion.Cotizaciones;
using JMQPresentacion.JMQWS;
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
        private ProductoWSClient productoService;
        protected void Page_Init(object sender, EventArgs e)
        {
            productoService = new JMQWS.ProductoWSClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }
        private void CargarProductos()
        {
            List<producto> productos = productoService.listaProducto().ToList();
            rptProductos.DataSource = productos;
            rptProductos.DataBind();
            }

        protected void btnAgregarProductos_Click(object sender, EventArgs e)
        {
            if (Session["Cart"] == null)
            {
                Session["Cart"] = new List<detalle>();
                Session["Orden"] = new ordenVenta { estado_compra = estadoCompra.pendiente }; // Los demás datos se llenarán después
            }
            List<detalle> cart = (List<detalle>)Session["Cart"];
            Button btn = (Button)sender;
            int idProducto = int.Parse(btn.CommandArgument);
            // Aquí buscaría el producto en la BD
            producto producto1 = productoService.buscarProducto(idProducto);
            detalle detalle1 = new detalle { producto = producto1, cantidad = 1, precio_unitario = producto1.precio };
            cart.Add(detalle1);
            Session["Cart"] = cart;
            Response.Redirect("~/Pedidos/Carrito.aspx");
        }

        public string ConvertirByteAImagenBase64(byte[] datosImagen)
        {
            return "data:image/jpeg;base64," + Convert.ToBase64String(datosImagen);
        }

        protected void btnCotizar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Cotizaciones/ListaCotizaciones.aspx");
        }

    }
}