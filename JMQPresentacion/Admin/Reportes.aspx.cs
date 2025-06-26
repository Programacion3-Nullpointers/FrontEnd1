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
        private CategoriaWSClient categoriaWSClient;
        private ProductoWSClient productoWSClient;

        protected void Page_Init(object sender, EventArgs e)
        {
            usuarioWSClient = new UsuarioWSClient();
            productoWSClient = new ProductoWSClient();
            categoriaWSClient = new CategoriaWSClient();
            CargarCategorias();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void CargarCategorias()
        {
            List<categoria> categorias = categoriaWSClient.ListarCategorias().ToList();

            ddlCategorias.DataSource = categorias;
            ddlCategorias.DataTextField = "nombre"; 
            ddlCategorias.DataValueField = "id";     
            ddlCategorias.DataBind();

            // Opción adicional manual: "Ninguno"
            ddlCategorias.Items.Insert(0, new ListItem("Ninguno", "0"));
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
                        int? stockMin = int.TryParse(StockMin.Text, out int tempStockMin) ? tempStockMin : (int?)null;
                        int? stockMax = int.TryParse(StockMax.Text, out int tempStockMax) ? tempStockMax : (int?)null;
                        int categoriaId = int.Parse(ddlCategorias.SelectedValue);
                        int?[] args = new int?[] { stockMin, stockMax, categoriaId };

                        // Lógica para reporte de stock
                        FileBuffer = productoWSClient.reporteStock(args);
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
