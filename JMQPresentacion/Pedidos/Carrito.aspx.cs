using JMQPresentacion.JMQWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMQPresentacion.Pedidos
{
    public partial class Carrito : System.Web.UI.Page
    {
        private OrdenVentaWSClient ordenVentaService;

        protected void Page_Init(object sender, EventArgs e)
        {
            ordenVentaService = new OrdenVentaWSClient();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VerificarSesion();
                CargarCarrito();
            }
        }

        private void VerificarSesion()
        {
            bool logueado = Session["Usuario"] != null;

            phBotonSesion.Visible = !logueado;
            phBotonCheckout.Visible = logueado;
        }

        private void CargarCarrito()
        {
            if (Session["Cart"] != null)
            {
                List<detalle> detalles = (List<detalle>)Session["Cart"];
                rptCarrito.DataSource = detalles;
                rptCarrito.DataBind();

                decimal total = detalles.Sum(item => item.cantidad * (decimal)item.precio_unitario);

                lblTotal.Text = "S/ " + total.ToString("F2");
                lblTotal2.Text = lblTotal.Text;
            }
            else
            {
                lblTotal.Text = "S/ 0.00";
                lblTotal2.Text = "S/ 0.00";
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            if (Session["Cart"] == null || ((List<detalle>)Session["Cart"]).Count == 0)
            {
                string script = "alert('El carrito está vacío. Por favor, agrega productos antes de continuar.');";
                ClientScript.RegisterStartupScript(this.GetType(), "alertaCarritoVacio", script, true);
                return;
            }

            ordenVenta orden = new ordenVenta
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

            Response.Redirect("~/Pedidos/DatosEntrega.aspx");
        }

        protected void CambiarCantidad(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.CommandArgument);
            List<detalle> detalles = (List<detalle>)Session["Cart"];

            if (btn.Text == "-" && detalles[index].cantidad > 1)
                detalles[index].cantidad--;
            else if (btn.Text == "+" && detalles[index].cantidad < detalles[index].producto.stock)
                detalles[index].cantidad++;

            Session["Cart"] = detalles;
            CargarCarrito();
        }

        protected void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            int index = int.Parse(((Button)sender).CommandArgument);
            List<detalle> detalles = (List<detalle>)Session["Cart"];

            if (index >= 0 && index < detalles.Count)
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
