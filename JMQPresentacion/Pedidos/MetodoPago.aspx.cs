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
        private OrdenVentaWSClient ordenVentaService;
        private EntregaWSClient entregaService;
        private BoletaWSClient boletaService;
        private FacturaWSClient facturaService;
        
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("/Principal/Principal.aspx");
            }
            ordenVentaService = new OrdenVentaWSClient();
            entregaService = new EntregaWSClient();
            boletaService = new BoletaWSClient();
            facturaService = new FacturaWSClient();
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
                string textoSeleccionado = rblComprobante.SelectedItem.Text;
                if (textoSeleccionado == "Factura")
                {
                    // Validar campos obligatorios para factura
                    if (string.IsNullOrWhiteSpace(txtRazonSocial.Text) || string.IsNullOrWhiteSpace(txtRUC.Text))
                    {
                        divError.Style["display"] = "block";
                        lblError.Text = "Debe completar la Razón Social y el RUC para emitir una factura.";
                        return;
                    }
                    try
                    {
                        ordenVenta orden1 = Session["Orden"] as ordenVenta;
                        orden1 = ordenVentaService.registrarOrdenVentaService(orden1);
                        entrega entrega1 = Session["Entrega"] as entrega;
                        entrega1.orden = orden1;
                        entregaService.RegistrarEntrega(entrega1);
                        factura factura1 = new factura
                        {
                            RUC = txtRUC.Text.Trim(),
                            razon_social = txtRazonSocial.Text.Trim(),
                            direccion = ((usuario)Session["Usuario"]).direccion,
                            fecha_emision = DateTime.Now,
                            orden = orden1,
                            metodoPago = metodoPago.tarjeta, //rbVisa.Checked ? metodoPago.tarjeta : metodoPago.efectivo,
                            fecha_pago = DateTime.Now,
                            monto_total = ((List<detalle>)Session["Cart"]).Sum(item => item.cantidad * item.precio_unitario),
                        };
                        facturaService.RegistrarFactura(factura1);
                        Session["Cart"] = null;
                        Session["Orden"] = null;
                    }
                    catch (ArgumentException ex)
                    {
                        divError.Style["display"] = "block";
                        lblError.Text = "Error al realizar el pago.";
                        Console.WriteLine($"Error: {ex.Message}");
                        return;
                    }

                }
                else if (textoSeleccionado == "Boleta")
                {
                    try
                    {
                        ordenVenta orden1 = Session["Orden"] as ordenVenta;
                        orden1 = ordenVentaService.registrarOrdenVentaService(orden1);
                        entrega entrega1 = Session["Entrega"] as entrega;
                        entrega1.orden = orden1;
                        entregaService.RegistrarEntrega(entrega1);
                        boleta boleta1 = new boleta
                        {
                            dni = ((usuario)Session["Usuario"]).dni,
                            nombre = ((usuario)Session["Usuario"]).nombreUsuario,
                            fecha_emision = DateTime.Now,
                            orden = orden1,
                            metodoPago = metodoPago.tarjeta,
                            fecha_pago = DateTime.Now,
                            monto_total = ((List<detalle>)Session["Cart"]).Sum(item => item.cantidad * item.precio_unitario),
                        };
                        boletaService.registrarBoleta(boleta1);
                        Session["Cart"] = null;
                        Session["Orden"] = null;
                    }
                    catch (ArgumentException ex)
                    {
                        divError.Style["display"] = "block";
                        lblError.Text = "Error al realizar el pago.";
                        Console.WriteLine($"Error: {ex.Message}");
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

        protected void rblComprobante_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlFactura.Visible = rblComprobante.SelectedValue == "Factura";
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Cart"] = null;
            Session["Orden"] = null;
            Response.Redirect("/Principal/Principal.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Pedidos/Carrito.aspx");
        }

        protected void MetodoPago_Changed(object sender, EventArgs e)
        {
            // Mostrar campos solo si se selecciona un método de pago con tarjeta
            pnlVisa.Visible = rbInterbank.Checked || rbVisa.Checked;
            pnlSaldo.Visible = rbSaldo.Checked;
        }

        protected void btnRecargarSaldo_Click(object sender, EventArgs e)
        {
            //Response.Redirect("/Pedidos/Carrito.aspx");
        }

    }

}