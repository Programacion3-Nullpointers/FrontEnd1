using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;

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
            if (double.TryParse(txtMonto.Text.Trim(), out monto) && monto > 0)
            {
                try
                {
                    user.saldo += monto;
                    usuarioWSClient.actualizarUsuario(user);
                    Session["Usuario"] = user; // Actualizar la sesión con el nuevo saldo
                    divExito.Style["display"] = "block";
                    lblExito.Text = "Saldo recargado exitosamente. Monto agregado: S/ " + monto;
                    lblSaldo.Text = "S/ " + user.saldo.ToString("F2");
                }
                catch (System.Exception ex)
                {
                    divError.Style["display"] = "block";
                    lblError.Text = "Error al recargar el saldo: " + ex.Message;
                    return;
                }
            }
            else
            {
                divError.Style["display"] = "block";
                lblError.Text = "Monto inválido.";
                return;
            }

        }
    }
}