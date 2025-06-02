using JMQDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMQPresentacion.Pedidos
{
    public partial class MetodoPago : System.Web.UI.Page
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
        protected void MetodoPago_Changed(object sender, EventArgs e)
        {
            pnlVisa.Visible = rbVisa.Checked;
            // Agrega condiciones para mostrar u ocultar otros paneles
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
                MetodoPago metodo;
                // insertar Entrega a la BD
                // insertar(metodo);
                Response.Redirect("~/Pedidos/MetodoPago.aspx");
            //}
        }
    }
}