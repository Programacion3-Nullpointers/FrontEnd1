using JMQDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMQPresentacion.Pagos
{
    public partial class Pagos : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Pagos"] == null)
                    Session["Pagos"] = new List<ComprobantePago>();

                CargarGrid();
            }
        }

        private void CargarGrid()
        {
            var lista = Session["Pagos"] as List<ComprobantePago>;
            string filtro = ddlFiltroTipo.SelectedValue;

            var data = lista
                .Where(p => filtro == "Todos" || p.GetType().Name == filtro)
                .OrderBy(p => p.id)
                .Select(p => new
                {
                    id = p.id,
                    tipo = p is Boleta ? "Boleta" : "Factura",
                    monto_total = p.monto_total,
                    fecha_pago = p.fecha_pago.ToShortDateString(),
                    detalle = p is Boleta b ? $"Nombre: {b.nombre}, DNI: {b.dni}, Emisión: {b.fecha_emision:dd/MM/yyyy}" :
                              p is Factura f ? $"RUC: {f.RUC}, Razón: {f.razon_social}, Emisión: {f.fecha_emision:dd/MM/yyyy}" : ""
                })
                .ToList();

            gvPagos.DataSource = data;
            gvPagos.DataBind();
        }

        protected void ddlFiltroTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrid();
        }

        protected void btnGuardarPago_Click(object sender, EventArgs e)
        {
            var lista = Session["Pagos"] as List<ComprobantePago> ?? new List<ComprobantePago>();

            int idEditar = ViewState["EditarId"] != null ? Convert.ToInt32(ViewState["EditarId"]) : 0;
            DateTime.TryParse(txtFechaPago.Text, out DateTime fechaPago);
            DateTime.TryParse(txtFechaEmision.Text, out DateTime fechaEmision);
            double.TryParse(txtMonto.Text, out double monto);

            var metodoPago = new MetodoPago(); // Simulado
            var orden = new OrdenVenta(); // Simulado

            if (idEditar > 0)
            {
                var pagoExistente = lista.FirstOrDefault(p => p.id == idEditar);
                if (pagoExistente != null)
                {
                    pagoExistente.fecha_pago = fechaPago;
                    pagoExistente.monto_total = monto;

                    if (pagoExistente is Boleta boleta)
                    {
                        boleta.nombre = txtNombre.Text;
                        boleta.dni = txtDNI.Text;
                        boleta.fecha_emision = fechaEmision;
                    }
                    else if (pagoExistente is Factura factura)
                    {
                        factura.RUC = txtRUC.Text;
                        factura.razon_social = txtRazonSocial.Text;
                        factura.direccion = txtDireccion.Text;
                        factura.fecha_emision = fechaEmision;
                    }
                }
                ViewState["EditarId"] = null;
            }
            else
            {
                int nuevoId = Enumerable.Range(1, lista.Count + 1).Except(lista.Select(p => p.id)).First();

                if (ddlTipoPago.SelectedValue == "Boleta")
                {
                    var nuevaBoleta = new Boleta(nuevoId, orden, metodoPago, fechaPago, monto, txtDNI.Text, txtNombre.Text, fechaEmision);
                    lista.Add(nuevaBoleta);
                }
                else if (ddlTipoPago.SelectedValue == "Factura")
                {
                    var nuevaFactura = new Factura(txtRUC.Text, txtRazonSocial.Text, txtDireccion.Text, fechaEmision, nuevoId, orden, metodoPago, fechaPago, monto);
                    lista.Add(nuevaFactura);
                }
            }

            Session["Pagos"] = lista;
            LimpiarCampos();
            CargarGrid();
            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal", "cerrarModal();", true);
        }

        protected void gvPagos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var lista = Session["Pagos"] as List<ComprobantePago>;
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                var pago = lista.FirstOrDefault(p => p.id == id);
                if (pago != null)
                {
                    lista.Remove(pago);
                    Session["Pagos"] = lista;
                    CargarGrid();
                }
            }
            else if (e.CommandName == "Editar")
            {
                var pago = lista.FirstOrDefault(p => p.id == id);
                if (pago != null)
                {
                    txtFechaPago.Text = pago.fecha_pago.ToString("yyyy-MM-dd");
                    txtMonto.Text = pago.monto_total.ToString();

                    if (pago is Boleta b)
                    {
                        ddlTipoPago.SelectedValue = "Boleta";
                        txtNombre.Text = b.nombre;
                        txtDNI.Text = b.dni;
                        txtFechaEmision.Text = b.fecha_emision.ToString("yyyy-MM-dd");
                    }
                    else if (pago is Factura f)
                    {
                        ddlTipoPago.SelectedValue = "Factura";
                        txtRUC.Text = f.RUC;
                        txtRazonSocial.Text = f.razon_social;
                        txtDireccion.Text = f.direccion;
                        txtFechaEmision.Text = f.fecha_emision.ToString("yyyy-MM-dd");
                    }

                    ViewState["EditarId"] = id;
                    ScriptManager.RegisterStartupScript(this, GetType(), "MostrarModal", "mostrarModal();", true);
                }
            }
        }

        private void LimpiarCampos()
        {
            txtFechaPago.Text = "";
            txtMonto.Text = "";
            txtNombre.Text = "";
            txtDNI.Text = "";
            txtRUC.Text = "";
            txtRazonSocial.Text = "";
            txtDireccion.Text = "";
            txtFechaEmision.Text = "";
        }
    }
}