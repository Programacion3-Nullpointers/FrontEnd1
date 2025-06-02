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
            /*
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login/Login.aspx");
            }
            else
            {
            */
            // Verificar si el carrito tiene productos
            if (Session["Cart"] != null && ((List<Detalle>)Session["Cart"]).Count > 0)
                Response.Redirect("~/Pedidos/DatosEntrega.aspx");
            else
            {
                // Mostrar mensaje de error o redirigir a una página de error
                string script = "alert('El carrito está vacío. Por favor, agrega productos antes de proceder al pago.');";
                ClientScript.RegisterStartupScript(this.GetType(), "alertaCarritoVacio", script, true);
            }
            //}
        }

        private void CargarCarrito()
        {   
            List<Detalle> detalles = (List<Detalle>)Session["Cart"];
            rptCarrito.DataSource = detalles;
            rptCarrito.DataBind();
            lblTotal.Text = "S/ " + detalles.Sum(item => item.cantidad * item.precio_unitario).ToString("F2");
            lblTotal2.Text = lblTotal.Text;
        }
        protected void CambiarCantidad(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.CommandArgument);

            // Obtener la lista desde ViewState
            List<Detalle> detalles = (List<Detalle>)Session["Cart"];

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

            List<Detalle> detalles = (List<Detalle>)Session["Cart"];

            if (detalles != null && index >= 0 && index < detalles.Count)
            {
                detalles.RemoveAt(index);
                Session["Cart"] = detalles;
                CargarCarrito();
            }
        }


    }
}