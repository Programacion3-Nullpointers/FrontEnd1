using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;

namespace JMQPresentacion.Pedidos
{
    public partial class DatosEntrega : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Cart"] != null)
                {
                    CargarResumen();
                }
            }
        }

        private void CargarResumen()
    {
            List<detalle> detalles = (List<detalle>)Session["Cart"];
            lblTotal.Text = "S/ " + detalles.Sum(item => item.cantidad * item.precio_unitario).ToString("F2");
            lblTotal2.Text = lblTotal.Text;
        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {

            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login/Login.aspx");
            }
            else
            {

                //entrega entrega1 = new entrega
                //{
                //    orden = (ordenVenta)Session["OrdenVenta"],
                //    fechaEntrega = DateTime.Now, //?
                //    tipoEntrega = pnlDespacho.Visible ? tipoEntrega.DELIVERY : tipoEntrega.RECOJO
                //};
                //if (pnlDespacho.Visible)
                //    entrega1.direccion = $"{txtDireccion.Text} {txtNumero.Text} {txtPisoDpto.Text} {txtReferencia.Text}";
                //else
                //    entrega1.dniRecibo = txtDni.Text;
                // insertar Entrega a la BD
                // insertar(entrega);
                Response.Redirect("~/Pedidos/MetodoPago.aspx");
                //}
            }
        }

        protected void btnDespacho_Click(object sender, EventArgs e)
        {
            pnlDespacho.Visible = true;
            pnlRetiro.Visible = false;
        }

        protected void btnRetiro_Click(object sender, EventArgs e)
        {
            pnlDespacho.Visible = false;
            pnlRetiro.Visible = true;
        }
    }
}