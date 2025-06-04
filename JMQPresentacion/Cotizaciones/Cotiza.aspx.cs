using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;

namespace JMQPresentacion.Pedidos
{
    public partial class Cotiza : System.Web.UI.Page
    {
        private ProductoCotizacionWSClient productoCotizacionWSClient;
        private CotizacionWSClient cotizacionWSClient;
        private UsuarioWSClient usuarioWSClient;

        protected void Page_Init(object sender, EventArgs e)
        {
            productoCotizacionWSClient = new JMQWS.ProductoCotizacionWSClient();
            cotizacionWSClient = new JMQWS.CotizacionWSClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int idCotizacion = Convert.ToInt32(Request.QueryString["id"]);
                    CargarCotizacion(idCotizacion);
                }
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            if (Session["Cotizacion"] == null) // Si no hay cotización activa, crear tabla
            {
                dt.Columns.Add("Producto");
                dt.Columns.Add("Cantidad", typeof(int));
                dt.Columns.Add("Precio", typeof(decimal));
                dt.Columns.Add("Subtotal", typeof(decimal));

                Session["Cotizacion"] = dt;
            }
            else
            {
                dt = (DataTable)Session["Cotizacion"];
            }

            // Agregar producto a la tabla
            DataRow row = dt.NewRow();
            row["Producto"] = txtProducto.Text;
            row["Cantidad"] = int.Parse(txtCantidad.Text);
            row["Precio"] = decimal.Parse(txtPrecio.Text);
            row["Subtotal"] = (int.Parse(txtCantidad.Text) * decimal.Parse(txtPrecio.Text));

            dt.Rows.Add(row);
            Session["Cotizacion"] = dt;

            // Mostrar en GridView
            gvCotizacion.DataSource = dt;
            gvCotizacion.DataBind();

            // Calcular total
            decimal total = dt.AsEnumerable().Sum(r => r.Field<decimal>("Subtotal"));
            lblTotal.Text = "Total: S/. " + total.ToString("0.00");

            JMQWS.productoCotizacion prod = new JMQWS.productoCotizacion();
            prod.descripcion = txtProducto.Text;
            prod.cantidad = int.Parse(txtCantidad.Text);

            //productoCotizacionWSClient.RegistrarPrecioProdCoti(prod, prod.cantidad);

        }

        private void CargarCotizacion(int id)
        {
            try
            {
                // Llama a tu servicio web CotizacionWS
                var cotiza = cotizacionWSClient.obtenerCotizacionesPorUsuario(id); // O el método correcto según tu WS

                // Muestra los datos generales (por ejemplo, en labels)
                lblEstado.Text = cotiza.Length.ToString();
                

                // Carga productos asociados
                var productos = productoCotizacionWSClient.listarProductosPorCotizacion(id);

                gvProductos.DataSource = productos;
                gvProductos.DataBind();
            }
            catch (System.Exception ex)
            {
                lblError.Text = "Error al cargar la cotización: " + ex.Message;
            }
        }

        protected void btnEnviarCotizacion_Click(object sender, EventArgs e)
        {
            cotizacion cot = new cotizacion();
            //cotizacionWSClient.registrarCotizacion(cot);

        }

    }
}