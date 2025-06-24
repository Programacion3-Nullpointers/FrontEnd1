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
        protected void btnGenerarReporte_Click(object sender, EventArgs e)
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
                        int? mesInicioProd = int.TryParse(ddlMesInicioProd.SelectedValue, out int tempMesInicio) ? tempMesInicio : (int?)null;
                        int? anioInicioProd = int.TryParse(txtAnioInicioProd.Text, out int tempAnioInicio) ? tempAnioInicio : (int?)null;
                        int? mesFinProd = int.TryParse(ddlMesFinProd.SelectedValue, out int tempMesFin) ? tempMesFin : (int?)null;
                        int? anioFinProd = int.TryParse(txtAnioFinProd.Text, out int tempAnioFin) ? tempAnioFin : (int?)null;
                        // Lógica para reporte de productos más vendidos
                        FileBuffer = productoWSClient.reporteMasVendidos();
                        break;

                    case "btnGenerarStock":
                        int? mesInicioStock = int.TryParse(ddlMesInicioStock.SelectedValue, out int tempMesInicioStock) ? tempMesInicioStock : (int?)null;
                        int? anioInicioStock = int.TryParse(txtAnioInicioStock.Text, out int tempAnioInicioStock) ? tempAnioInicioStock : (int?)null;
                        int? mesFinStock = int.TryParse(ddlMesFinStock.SelectedValue, out int tempMesFinStock) ? tempMesFinStock : (int?)null;
                        int? anioFinStock = int.TryParse(txtAnioFinStock.Text, out int tempAnioFinStock) ? tempAnioFinStock : (int?)null;
                        // Lógica para reporte de stock
                        FileBuffer = productoWSClient.reporteStock();
                        break;

                    case "btnGenerarClientes":
                        int? mesInicioClientes = int.TryParse(ddlMesInicioClientes.SelectedValue, out int tempMesInicioClientes) ? tempMesInicioClientes : (int?)null;
                        int? anioInicioClientes = int.TryParse(txtAnioInicioClientes.Text, out int tempAnioInicioClientes) ? tempAnioInicioClientes : (int?)null;
                        int? mesFinClientes = int.TryParse(ddlMesFinClientes.SelectedValue, out int tempMesFinClientes) ? tempMesFinClientes : (int?)null;
                        int? anioFinClientes = int.TryParse(txtAnioFinClientes.Text, out int tempAnioFinClientes) ? tempAnioFinClientes : (int?)null;
                        int? minCompras = int.TryParse(txtMinCompras.Text, out int tempMinCompras) ? tempMinCompras : (int?)null;
                        // Lógica para reporte de clientes recurrentes
                        FileBuffer = usuarioWSClient.reporteClientes();
                        break;
                }
                if (FileBuffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", FileBuffer.Length.ToString());
                    Response.BinaryWrite(FileBuffer);
                }
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", $"alert('Error al generar el reporte: {ex.Message}');", true);
            }

        }
    }
}
