using JMQDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMQPresentacion.Pedidos
{
    public partial class Carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Categoria a = new Categoria();
            Producto[] productos = new Producto[]
            {
                new Producto(1, "Martillo", "Martillo rojo", 25, 12.3, a, "google.com"),
                new Producto(1, "Martillo", "Martillo rojo", 25, 12.3, a, "google.com"),
                new Producto(1, "Martillo", "Martillo rojo", 25, 12.3, a, "google.com"),
            };
            ProductosCarrito.DataSource = productos;
            ProductosCarrito.DataBind();
        }
    }
}