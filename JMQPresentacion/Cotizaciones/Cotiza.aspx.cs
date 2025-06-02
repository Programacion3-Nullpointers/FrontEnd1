using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMQPresentacion.Pedidos
{
    public partial class Cotiza : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Clear();
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
        }



    }
}