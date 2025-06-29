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
            usuarioWSClient = new JMQWS.UsuarioWSClient();
            productoWSClient = new JMQWS.ProductoWSClient();
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
            ddlCategorias.Items.Insert(0, new ListItem("Ninguno", ""));
        }
        protected void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Byte[] FileBuffer = null;
            int?[] args;
            if (btn == null) return;
            try
            {
                // Manda todos los parámetros a productoWSClient.reporte(), y de ahí se llena la query para el reporte
                switch (btn.ID)
                {
                    case "btnGenerarProd":
                        int? mesInicioProd = int.TryParse(ddlMesInicioProd.SelectedValue, out int tempMesInicio) ? tempMesInicio : (int?)null;
                        int? anioInicioProd = int.TryParse(txtAnioInicioProd.Text, out int tempAnioInicio) ? tempAnioInicio : (int?)null;
                        int? mesFinProd = int.TryParse(ddlMesFinProd.SelectedValue, out int tempMesFin) ? tempMesFin : (int?)null;
                        int? anioFinProd = int.TryParse(txtAnioFinProd.Text, out int tempAnioFin) ? tempAnioFin : (int?)null;
                        args = new int?[] { mesInicioProd, anioInicioProd, mesFinProd, anioFinProd};
                        // Lógica para reporte de productos más vendidos
                        FileBuffer = productoWSClient.reporteMasVendidos(args);
                        break;

                    case "btnGenerarStock":
                        int? stockMin = int.TryParse(StockMin.Text, out int tempStockMin) ? tempStockMin : (int?)null;
                        int? stockMax = int.TryParse(StockMax.Text, out int tempStockMax) ? tempStockMax : (int?)null;
                        int? categoriaId = int.TryParse(ddlCategorias.SelectedValue, out int tempIdCat) ? tempIdCat : (int?)null;
                        args = new int?[] { stockMin, stockMax, categoriaId};

                        // Lógica para reporte de stock
                        FileBuffer = productoWSClient.reporteStock(args);
                        break;

                    case "btnGenerarClientes":
                        int? mesInicioClientes = int.TryParse(ddlMesInicioClientes.SelectedValue, out int tempMesInicioClientes) ? tempMesInicioClientes : (int?)null;
                        int? anioInicioClientes = int.TryParse(txtAnioInicioClientes.Text, out int tempAnioInicioClientes) ? tempAnioInicioClientes : (int?)null;
                        int? mesFinClientes = int.TryParse(ddlMesFinClientes.SelectedValue, out int tempMesFinClientes) ? tempMesFinClientes : (int?)null;
                        int? anioFinClientes = int.TryParse(txtAnioFinClientes.Text, out int tempAnioFinClientes) ? tempAnioFinClientes : (int?)null;
                        int? minCompras = int.TryParse(txtMinCompras.Text, out int tempMinCompras) ? tempMinCompras : (int?)null;
                        args = new int?[] { mesInicioClientes, anioInicioClientes, mesFinClientes, anioFinClientes, minCompras};
                        // Lógica para reporte de clientes recurrentes
                        FileBuffer = usuarioWSClient.reporteClientes(args);
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
