using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;

namespace JMQPresentacion.Pedidos
{
    public partial class MetodoPago : System.Web.UI.Page
    {
        private ComprobantePagoWSClient comprobanteService;
        protected void Page_Init(object sender, EventArgs e)
        {
            comprobanteService = new ComprobantePagoWSClient();
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
        protected void MetodoPago_Changed(object sender, EventArgs e)
        {
            pnlVisa.Visible = rbVisa.Checked;
            // Agrega condiciones para mostrar u ocultar otros paneles
        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {

            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login/Login.aspx");
            }
            else
            {
                string textoSeleccionado = rblComprobante.SelectedItem.Text;
                if (textoSeleccionado == "Factura")
                {
                    try
                    {
                        factura comprobante = new factura
                        {
                            RUC = ((usuario)Session["Usuario"]).RUC,
                            razon_social = ((usuario)Session["Usuario"]).razonsocial,
                            direccion = ((usuario)Session["Usuario"]).direccion,
                            fecha_emision = DateTime.Now,
                            orden = (ordenVenta)Session["Orden"],
                            metodoPago = metodoPago.tarjeta, //rbVisa.Checked ? metodoPago.tarjeta : metodoPago.efectivo,
                            fecha_pago = DateTime.Now,
                            monto_total = ((List<detalle>)Session["Cart"]).Sum(item => item.cantidad * item.precio_unitario),
                        };
                        comprobanteService.registrarComprobante(comprobante);
                    }
                    catch
                    {
                        divError.Style["display"] = "block";
                        lblError.Text = "Error al realizar el pago.";
                        return;
                    }

                }
                else if (textoSeleccionado == "Boleta")
                {
                    try
                    {
                        boleta comprobante = new boleta
                        {
                            dni = ((usuario)Session["Usuario"]).dni,
                            nombre = ((usuario)Session["Usuario"]).nombreUsuario,
                            fecha_emision = DateTime.Now,
                            orden = (ordenVenta)Session["Orden"],
                            metodoPago = metodoPago.tarjeta, //rbVisa.Checked ? metodoPago.tarjeta : metodoPago.efectivo,
                            fecha_pago = DateTime.Now,
                            monto_total = ((List<detalle>)Session["Cart"]).Sum(item => item.cantidad * item.precio_unitario),
                        };
                        comprobanteService.registrarComprobante(comprobante);
                    }
                    catch
                    {
                        divError.Style["display"] = "block";
                        lblError.Text = "Error al realizar el pago.";
                        return;
                    }
                }
                else
                {
                    divError.Style["display"] = "block";
                    lblError.Text = "Seleccione un tipo de comprobante válido.";
                    return;
                }
                Response.Redirect("/Principal/Principal.aspx");
            }
        }
    }
}