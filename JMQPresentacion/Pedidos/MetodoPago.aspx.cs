using JMQPresentacion.JMQWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMQPresentacion.Pedidos
{
    public partial class MetodoPago : System.Web.UI.Page
    {
        private OrdenVentaWSClient ordenVentaService;
        private EntregaWSClient entregaService;
        private BoletaWSClient boletaService;
        private FacturaWSClient facturaService;
        private UsuarioWSClient usuarioWSClient;
        private double precioCompra;
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
            usuarioWSClient = new UsuarioWSClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Cart"] != null)
                {
                    CargarResumen();
                }

                if (Session["Usuario"] != null)
                {
                    var user = (usuario)Session["Usuario"]; 

                    if (user.tipoUsuario.ToString() == "EMPRESA")
                    {
                        var itemBoleta = rblComprobante.Items.FindByValue("Boleta");
                        if (itemBoleta != null)
                        {
                            rblComprobante.Items.Remove(itemBoleta);
                        }

                        var itemFactura = rblComprobante.Items.FindByValue("Factura");
                        if (itemFactura != null)
                        {
                            itemFactura.Selected = true;
                            pnlFactura.Visible = true;
                        }
                    }
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
            Button btnPresionado = (Button)sender;
            usuario user = Session["Usuario"] as usuario;
            if (user == null)
            {
                Response.Redirect("~/Login/Login.aspx");
                return;
            }

            if (btnPresionado.ID == "btnPagar" && !validarDatos())
            {
                return;
            }
            if (btnPresionado.ID == "btnPagarSaldo")
            {
                double precio = ((List<detalle>)Session["Cart"]).Sum(item => item.cantidad * item.precio_unitario);
                if (user.saldo < precio)
                {
                    divError.Style["display"] = "block";
                    lblError.Text = "Saldo insuficiente para realizar el pago.";
                    return;
                }
            }
            // 🔒 Bloquear botón y cambiar texto
            string bloquearBoton = $@"
                document.getElementById('{btnPresionado.ClientID}').disabled = true;
                document.getElementById('{btnPresionado.ClientID}').innerText = 'Procesando...';";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "bloqueoBoton", bloquearBoton, true);

            // 🌀 Spinner de carga
            string spinnerScript = @"
                Swal.fire({
                    title: 'Procesando pago...',
                    text: 'Por favor espera un momento.',
                    allowOutsideClick: false,
                    didOpen: () => {
                        Swal.showLoading();
                    }
                });";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "spinnerPago", spinnerScript, true);

            string textoSeleccionado = rblComprobante.SelectedItem.Text;

            try
            {
                ordenVenta orden1 = Session["Orden"] as ordenVenta;
                orden1 = ordenVentaService.registrarOrdenVentaService(orden1);
                entrega entrega1 = Session["Entrega"] as entrega;
                entrega1.orden = orden1;
                entregaService.RegistrarEntrega(entrega1);

                if (textoSeleccionado == "Factura")
                {
                    if (string.IsNullOrWhiteSpace(txtRazonSocial.Text) || string.IsNullOrWhiteSpace(txtRUC.Text))
                    {
                        divError.Style["display"] = "block";
                        lblError.Text = "Debe completar la Razón Social y el RUC para emitir una factura.";
                        return;
                    }

                    factura factura1 = new factura
                    {
                        RUC = txtRUC.Text.Trim(),
                        razon_social = txtRazonSocial.Text.Trim(),
                        direccion = ((usuario)Session["Usuario"]).direccion,
                        fecha_emision = DateTime.Now,
                        orden = orden1,
                        metodoPago = (btnPresionado.ID == "btnEfectivo") ? metodoPago.efectivo : metodoPago.tarjeta,
                        fecha_pago = DateTime.Now,
                        monto_total = ((List<detalle>)Session["Cart"]).Sum(item => item.cantidad * item.precio_unitario),
                    };

                    facturaService.RegistrarFactura(factura1);
                    if (btnPresionado.ID == "btnPagarSaldo") reducirSaldo(factura1.monto_total);
                }
                else if (textoSeleccionado == "Boleta")
                {
                    boleta boleta1 = new boleta
                    {
                        dni = ((usuario)Session["Usuario"]).dni,
                        nombre = ((usuario)Session["Usuario"]).nombreUsuario,
                        fecha_emision = DateTime.Now,
                        orden = orden1,
                        metodoPago = (btnPresionado.ID == "btnEfectivo") ? metodoPago.efectivo : metodoPago.tarjeta,
                        fecha_pago = DateTime.Now,
                        monto_total = ((List<detalle>)Session["Cart"]).Sum(item => item.cantidad * item.precio_unitario),
                    };

                    boletaService.registrarBoleta(boleta1);
                    if (btnPresionado.ID == "btnPagarSaldo") reducirSaldo(boleta1.monto_total);
                }
                else
                {
                    divError.Style["display"] = "block";
                    lblError.Text = "Seleccione un tipo de comprobante válido.";
                    return;
                }

                // 🧹 Limpiar carrito y orden
                Session["Cart"] = null;
                Session["Orden"] = null;
                Session["Entrega"] = null;

                // ✅ Confirmación y redirección
                string successScript = @"
                    setTimeout(() => {
                        Swal.fire({
                            icon: 'success',
                            title: '¡Pago realizado!',
                            text: 'Gracias por tu compra.',
                            showConfirmButton: false,
                            timer: 2000
                        }).then(() => {
                            window.location.href = '/Principal/Principal.aspx';
                        });
                    }, 500);";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pagoExitoso", successScript, true);
            }
            catch (System.Exception ex)
            {
                // ❌ Mostrar error
                divError.Style["display"] = "block";
                lblError.Text = "Error al realizar el pago.";
                Console.WriteLine($"Error: {ex.Message}");

                // 🔓 Restaurar el botón si hay error
                string desbloqueoScript = $@"
                    document.getElementById('{btnPresionado.ClientID}').disabled = false;
                    document.getElementById('{btnPresionado.ClientID}').innerText = 'PAGAR';";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "desbloqueoBoton", desbloqueoScript, true);
            }
        }


        protected void rblComprobante_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlFactura.Visible = rblComprobante.SelectedValue == "Factura";
        }

        protected void MetodoPago_Changed(object sender, EventArgs e)
        {
            // Mostrar campos solo si se selecciona un método de pago con tarjeta
            pnlVisa.Visible = rbInterbank.Checked || rbVisa.Checked;
            pnlSaldo.Visible = rbSaldo.Checked;
            if (rbSaldo.Checked)
            {
                usuario user = Session["Usuario"] as usuario;
                lblSaldoPago.Text = "S/ " + user.saldo.ToString("F2");
            }
            pnlEfectivo.Visible = rbEfectivo.Checked;
        }

        protected void btnRecargarSaldo_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Pedidos/RecargarSaldo.aspx?volverPago=true");
        }

        private bool validarDatos()
        {
            string numeroTarjeta = txtNumeroTarjeta.Text.Trim().Replace(" ", "");
            string cvv = txtCVV.Text.Trim();
            string fecha = txtFechaExp.Text.Trim();

            // Validación de número de tarjeta
            if (!Regex.IsMatch(numeroTarjeta, @"^\d{16}$"))
            {
                divError.Style["display"] = "block";
                lblError.Text = "El número de tarjeta debe tener exactamente 16 dígitos.";
                return false;
            }

            // Validación de CVV
            if (!Regex.IsMatch(cvv, @"^\d{3}$"))
            {
                divError.Style["display"] = "block";
                lblError.Text = "El CVV debe tener 3 dígitos numéricos.";
                return false;
            }

            // Validación de fecha de expiración
            if (!Regex.IsMatch(fecha, @"^(0[1-9]|1[0-2])\/\d{2}$"))
            {
                divError.Style["display"] = "block";
                lblError.Text = "La fecha de expiración debe tener el formato MM/AA.";
                return false;
            }
            return true;
        }

        private void reducirSaldo(double cantidad)
        {
            usuario user = Session["Usuario"] as usuario;
            user.saldo -= cantidad;
            usuarioWSClient.actualizarUsuario(user);
            Session["Usuario"] = user; // Actualizar la sesión con el nuevo saldo
        }
    }

}