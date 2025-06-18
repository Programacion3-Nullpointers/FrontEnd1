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
        private EntregaWSClient entregaService;
        protected void Page_Init(object sender, EventArgs e)
        {
            entregaService= new JMQWS.EntregaWSClient();
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
                divError.Style["display"] = "none";
                lblError.Text = "";
                //comprobaciones
                if ((pnlDespacho.Visible && (string.IsNullOrWhiteSpace(txtDireccion.Text) || string.IsNullOrWhiteSpace(txtNumero.Text))) ||
                    (pnlRetiro.Visible && string.IsNullOrWhiteSpace(txtDni.Text)))
                {
                    lblError.Text = "Complete todos los campos obligatorios.";
                    divError.Style["display"] = "block";
                    return;
                }
                if (pnlRetiro.Visible && txtDni.Text.Length != 8)
                {
                    lblError.Text = "DNI inválido.";
                    divError.Style["display"] = "block";
                    return;
                }
                if (pnlDespacho.Visible && (!txtNumero.Text.All(char.IsDigit) || int.Parse(txtNumero.Text) < 0))
                {
                    lblError.Text = "Ingrese un número de dirección válido.";
                    divError.Style["display"] = "block";
                    return;
                }
                entrega entrega1 = new entrega
                    {
                        orden = (ordenVenta)Session["OrdenVenta"],
                        fecha_entrega = DateTime.Now.AddDays(7),
                        tipoEntrega = pnlDespacho.Visible ? tipoEntrega.DELIVERY : tipoEntrega.RECOJO
                    };
                if (pnlDespacho.Visible) {
                    string direccion = string.Join(" ", new[] { txtDireccion.Text, txtNumero.Text, txtPisoDpto.Text, txtReferencia.Text }
                        .Where(s => !string.IsNullOrWhiteSpace(s)));
                    entrega1.direccion = direccion;
                }
                else
                    entrega1.dniRecibo = txtDni.Text;
                //insertar Entrega a la BD
                entregaService.RegistrarEntrega(entrega1);
                Response.Redirect("~/Pedidos/MetodoPago.aspx");
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