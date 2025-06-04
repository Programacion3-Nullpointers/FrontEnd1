using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMQPresentacion.Cotizaciones
{
    public partial class ListaCotizaciones : System.Web.UI.Page
    {
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
            DataTable dtCotizaciones = null; // Tu método para obtener datos

            if (dtCotizaciones.Rows.Count > 0)
            {
                rptCotizaciones.DataSource = dtCotizaciones;
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
    }
}