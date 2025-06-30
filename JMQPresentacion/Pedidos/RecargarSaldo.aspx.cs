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
    public partial class RecargarSaldo : System.Web.UI.Page
    {
        private UsuarioWSClient usuarioWSClient;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("/Principal/Principal.aspx");
            }
            usuarioWSClient = new UsuarioWSClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["volverPago"] == "true")
                {
                    pnlRegresar.Visible = true;
                }

                usuario user = Session["Usuario"] as usuario;
                lblSaldo.Text = "S/ " + user?.saldo.ToString("F2");
            }
        }

        protected void btnRecargar_Click(object sender, EventArgs e)
        {
            divError.Style["display"] = "none";
            lblError.Text = "";
            divExito.Style["display"] = "none";
            lblExito.Text = "";

            usuario user = Session["Usuario"] as usuario;
            double monto;

            if (!validarDatos())
                return;

            if (double.TryParse(txtMonto.Text.Trim(), out monto) && monto > 0)
            {
                try
                {
                    // Mostrar spinner mientras se procesa
                    string spinnerScript = @"
                Swal.fire({
                    title: 'Procesando...',
                    text: 'Estamos recargando tu saldo.',
                    allowOutsideClick: false,
                    didOpen: () => {
                        Swal.showLoading();
                    }
                });";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "spinner", spinnerScript, true);

                    // Simulación de recarga
                    user.saldo += monto;
                    usuarioWSClient.actualizarUsuario(user);
                    Session["Usuario"] = user;

                    lblSaldo.Text = "S/ " + user.saldo.ToString("F2");

                    // Mostrar mensaje de éxito después del proceso
                    string successScript = $@"
                    setTimeout(() => {{
                        Swal.fire({{
                            icon: 'success',
                            title: '¡Saldo recargado!',
                            text: 'Se agregó S/ {monto:F2} a tu cuenta.',
                            timer: 2000,
                            showConfirmButton: false
                        }});
                    }}, 500);";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "exitoRecarga", successScript, true);
                }
                catch (System.Exception ex)
                {
                    string errorScript = $@"
                    Swal.fire({{
                        icon: 'error',
                        title: 'Error al recargar',
                        text: '{ex.Message.Replace("'", "\\'")}'
                    }});";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorRecarga", errorScript, true);
                }
            }
            else
            {
                string invalidScript = @"
                Swal.fire({
                    icon: 'warning',
                    title: 'Monto inválido',
                    text: 'Por favor, ingresa un monto mayor a cero.'
                });";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "montoInvalido", invalidScript, true);
            }
        }


        private bool validarDatos()
        {
            string numeroTarjeta = txtNumero.Text.Trim().Replace(" ", "");
            string cvv = txtCVV.Text.Trim();
            string fecha = txtExp.Text.Trim();

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
    }
}