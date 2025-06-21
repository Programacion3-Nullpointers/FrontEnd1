using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;

namespace JMQPresentacion.Pedidos
{
    public partial class Carrito : System.Web.UI.Page
    {
        private OrdenVentaWSClient ordenVentaService;
        protected void Page_Init(object sender, EventArgs e)
        {
            ordenVentaService = new JMQWS.OrdenVentaWSClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Cart"] != null)
                {
                    CargarCarrito();
                }
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login/Login.aspx");
            }
            else
            {
                // Verificar si el carrito tiene productos
                if (Session["Cart"] != null && ((List<detalle>)Session["Cart"]).Count > 0)
                {
                    ordenVenta orden = new ordenVenta
                    {
                        estado_compra = estadoCompra.pendiente,
                        fecha_orden = DateTime.Now,
                        activo = true,
                        usuario = (usuario)Session["Usuario"],
                    };
                    detalle[] arrDetalles = ((List<detalle>)Session["Cart"]).ToArray();
                    orden.detalle = arrDetalles;
                    // Guardar la orden en la base de datos
                    if (Session["Orden"] == null)
                    {
                        ordenVentaService.registrarOrdenVentaService(orden);
                        Session["Orden"] = orden; // Guardar la orden en la sesión para usarla en DatosEntrega.aspx
                    }
                    Response.Redirect("~/Pedidos/DatosEntrega.aspx");
                }
                else
                {
                    // Mostrar mensaje de error o redirigir a una página de error
                    string script = "alert('El carrito está vacío. Por favor, agrega productos antes de proceder al pago.');";
                    ClientScript.RegisterStartupScript(this.GetType(), "alertaCarritoVacio", script, true);
                }
            }
        }

        private void CargarCarrito()
        {
            List<detalle> detalles = (List<detalle>)Session["Cart"];
            rptCarrito.DataSource = detalles;
            rptCarrito.DataBind();
            lblTotal.Text = "S/ " + detalles.Sum(item => item.cantidad * item.precio_unitario).ToString("F2");
            lblTotal2.Text = lblTotal.Text;
        }
        protected void CambiarCantidad(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.CommandArgument);
            RepeaterItem item = (RepeaterItem)btn.NamingContainer;
            //Obtener la lista desde ViewState
            List<detalle> detalles = (List<detalle>)Session["Cart"];

            // Modificar la cantidad
            if (btn.Text == "-")
            {
                if (detalles[index].cantidad > 1) // Evitar cantidades menores a 1
                    detalles[index].cantidad--;
            }
            else if (btn.Text == "+")
            {
                if (detalles[index].cantidad < detalles[index].producto.stock)
                    detalles[index].cantidad++;
            }
            Session["Cart"] = detalles;
            CargarCarrito();
        }
        protected void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            Button btnEliminar = (Button)sender;
            int index = int.Parse(btnEliminar.CommandArgument);

            List<detalle> detalles = (List<detalle>)Session["Cart"];

            if (detalles != null && index >= 0 && index < detalles.Count)
            {
                detalles.RemoveAt(index);
                Session["Cart"] = detalles;
                CargarCarrito();
            }
        }

        public string ConvertirByteAImagenBase64(byte[] datosImagen)
        {
            return "data:image/jpeg;base64," + Convert.ToBase64String(datosImagen);
        }
    }
}