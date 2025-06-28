using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using JMQPresentacion.JMQWS;

namespace JMQPresentacion.Cotizaciones
{
    public partial class ListaCotizaciones : System.Web.UI.Page
    {
        private CotizacionWSClient cotizacionWSCLClient;

        protected void Page_Init(object sender, EventArgs e)
        {

            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login/Login.aspx");
            }
            cotizacionWSCLClient = new JMQWS.CotizacionWSClient();
            rptCotizaciones.ItemDataBound += rptCotizaciones_ItemDataBound;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCotizaciones();
            }
        }

        private void CargarCotizaciones()
        {
            // Ejemplo: Obtener datos de la base de datos o servicio

            usuario user = Session["Usuario"] as usuario;
            if (user == null)
            {
                // Redirigir al login u otra acción
                Response.Redirect("~/Login/Login.aspx");

                return;
            }

            var cotizaciones = cotizacionWSCLClient.obtenerCotizacionesPorUsuario(user.id);
            
            if (cotizaciones != null && cotizaciones.Length > 0)
            {
                rptCotizaciones.DataSource = cotizaciones;
                rptCotizaciones.DataBind();
                rptCotizaciones.Visible = true;
                pnlSinCotizaciones.Visible = false;
            }
            else
            {
                rptCotizaciones.Visible = false;
                pnlSinCotizaciones.Visible = true;
            }
        }


        // Método auxiliar para estilizar el estado (opcional)
        public string GetEstadoCssClass(object estado)
        {
            switch (estado.ToString())
            {
                case "Aprobado": return "bg-success";
                case "Pendiente": return "bg-warning";
                case "Rechazado": return "bg-danger";
                default: return "bg-secondary";
            }
        }

        protected void rptCotizaciones_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var cotizacion = (cotizacion)e.Item.DataItem;

                var rptProductos = (Repeater)e.Item.FindControl("rptProductos");
                rptProductos.DataSource = cotizacion.productos;
                rptProductos.DataBind();
            }
        }

        protected void VerDetalle_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int idCotizacion = Convert.ToInt32(btn.CommandArgument);

            // Aquí puedes hacer lo que necesites, por ejemplo:
            // redirigir a otra página con el detalle, pasando el id como parámetro:
            Response.Redirect($"DetalleCotizacion.aspx?id={idCotizacion}");

            // O cargar datos en un modal o panel en la misma página, según tu lógica
        }

        protected void btnCotiza_Click(object sender, EventArgs e)
        {


            // Aquí puedes hacer lo que necesites, por ejemplo:
            // redirigir a otra página con el detalle, pasando el id como parámetro:
            Response.Redirect("~/Cotizaciones/Cotiza.aspx");
            // O cargar datos en un modal o panel en la misma página, según tu lógica
        }

    }
}