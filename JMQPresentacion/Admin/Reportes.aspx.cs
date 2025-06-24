using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using JMQPresentacion.JMQWS;
using System.Web.UI.WebControls;

namespace JMQPresentacion.Admin
{
    public partial class Reportes : System.Web.UI.Page
    {
        private UsuarioWSClient usuarioWSClient;
        private ProductoWSClient productoWSClient;

        protected void Page_Init(object sender, EventArgs e)
        {
            usuarioWSClient = new JMQWS.UsuarioWSClient();
            productoWSClient = new JMQWS.ProductoWSClient();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void GenerarReporte_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Byte[] FileBuffer = null;
            if (btn == null) return;
            try
            {
                // Aún no está eso, recordar quitar *este* comentario cuando se implemente xd
                // Manda todos los parámetros a WS.reporte(), y de ahí se crea una query para el reporte
                switch (btn.ID)
                {
                    case "btnGenerarProd":
                        int mesInicioProd = int.Parse(ddlMesInicioProd.SelectedValue);
                        int anioInicioProd = int.Parse(txtAnioInicioProd.Text);
                        int mesFinProd = int.Parse(ddlMesFinProd.SelectedValue);
                        int anioFinProd = int.Parse(txtAnioFinProd.Text);
                        // Lógica para reporte de productos más vendidos
                        Session["ReportePDF"] = productoWSClient.reporteMasVendidos();
                        break;

                    case "btnGenerarStock":
                        int mesInicioStock = int.Parse(ddlMesInicioStock.SelectedValue);
                        int anioInicioStock = int.Parse(txtAnioInicioStock.Text);
                        int mesFinStock = int.Parse(ddlMesFinStock.SelectedValue);
                        int anioFinStock = int.Parse(txtAnioFinStock.Text);
                        // Lógica para reporte de stock
                        Session["ReportePDF"] = productoWSClient.reporteStock();
                        break;

                    case "btnGenerarClientes":
                        int mesInicioClientes = int.Parse(ddlMesInicioClientes.SelectedValue);
                        int anioInicioClientes = int.Parse(txtAnioInicioClientes.Text);
                        int mesFinClientes = int.Parse(ddlMesFinClientes.SelectedValue);
                        int anioFinClientes = int.Parse(txtAnioFinClientes.Text);
                        int minCompras = int.Parse(txtMinCompras.Text);
                        // Lógica para reporte de clientes recurrentes
                        Session["ReportePDF"] = usuarioWSClient.reporteClientes();
                        break;
                }
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", $"alert('Error al generar el reporte: {ex.Message}');", true);
            }

        }
    }
}
