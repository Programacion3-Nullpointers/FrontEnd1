using JMQDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMQPresentacion.Pedidos
{
    public partial class DatosEntrega : System.Web.UI.Page
    {
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
            List<Detalle> detalles = (List<Detalle>)Session["Cart"];
            lblTotal.Text = "S/ " + detalles.Sum(item => item.cantidad * item.precio_unitario).ToString("F2");
            lblTotal2.Text = lblTotal.Text;
        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {
            /*
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login/Login.aspx");
            }
            else
            {
            */
                Entrega entrega = new Entrega
                {
                    orden = (OrdenVenta)Session["OrdenVenta"],
                    fechaEntrega = DateTime.Now, //?
                    //direccion = ...
                    //dniRecibo = txtDni.Text,
                    tipoEntrega = pnlDespacho.Visible ? TipoEntrega.DELIVERY : TipoEntrega.RECOJO
                };
                if (pnlDespacho.Visible)
                    entrega.direccion = $"{txtDireccion.Text} {txtNumero.Text} {txtPisoDpto.Text} {txtReferencia.Text}";
                else
                    entrega.dniRecibo = txtDni.Text;
                // insertar Entrega a la BD
                    // insertar(entrega);
                    Response.Redirect("~/Pedidos/MetodoPago.aspx");
                //}
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