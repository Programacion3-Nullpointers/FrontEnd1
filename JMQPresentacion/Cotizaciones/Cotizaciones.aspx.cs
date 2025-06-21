using JMQDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMQPresentacion.Cotizaciones
{
    public partial class Cotizaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Cotizaciones"] == null)
                {
                    Usuario demoUser = new Usuario(1, "admin01", "123456", true, "admin@jmq.com", new TipoUsuario(), "JMQ SAC", "Av. Principal 123", "20604010123");

                    List<Cotizacion> listaInicial = new List<Cotizacion>
                    {
                        new Cotizacion(1, demoUser, "Pendiente"),
                        new Cotizacion(2, demoUser, "Aprobada")
                    };

                    Session["Cotizaciones"] = listaInicial;
                }

                CargarGrid();
            }
        }

        private void CargarGrid()
        {
            List<Cotizacion> lista = Session["Cotizaciones"] as List<Cotizacion>;

            var gridData = lista.OrderBy(c => c.id)
                                .Select(c => new
                                {
                                    id = c.id,
                                    nombreUsuario = c.usuario.nombreUsuario,
                                    correo = c.usuario.correo,
                                    estado = c.estadoCotizacion
                                }).ToList();

            gvCotizaciones.DataSource = gridData;
            gvCotizaciones.DataBind();
        }

        protected void btnGuardarCotizacion_Click(object sender, EventArgs e)
        {
            List<Cotizacion> lista = Session["Cotizaciones"] as List<Cotizacion>;
            if (lista == null) lista = new List<Cotizacion>();

            int? idEditando = ViewState["EditarId"] as int?;

            if (idEditando != null)
            {
                // Modo edición: solo se cambia el estado
                var cotExistente = lista.FirstOrDefault(c => c.id == idEditando.Value);
                if (cotExistente != null)
                {
                    cotExistente.estadoCotizacion = txtEstado.Text;
                }
                ViewState["EditarId"] = null;
            }
            else
            {
                // Modo nuevo: requiere crear usuario completo
                int nuevoId = Enumerable.Range(1, lista.Count + 1)
                                        .Except(lista.Select(c => c.id))
                                        .First();

                Usuario nuevoUsuario = new Usuario(
                    0, txtNombreUsuario.Text, "", true, txtCorreo.Text,
                    new TipoUsuario(), txtRazonSocial.Text, txtDireccion.Text, txtRUC.Text
                );

                Cotizacion nueva = new Cotizacion(nuevoId, nuevoUsuario, txtEstado.Text);
                lista.Add(nueva);
            }

            Session["Cotizaciones"] = lista;
            CargarGrid();

            // Limpiar solo el campo de estado
            txtEstado.Text = "";

            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal", "cerrarModal();", true);
        }

        protected void gvCotizaciones_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            List<Cotizacion> lista = Session["Cotizaciones"] as List<Cotizacion>;
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                var cot = lista.FirstOrDefault(x => x.id == id);
                if (cot != null)
                {
                    lista.Remove(cot);
                    Session["Cotizaciones"] = lista;
                    CargarGrid();
                }
            }
            else if (e.CommandName == "Editar")
            {
                var cot = lista.FirstOrDefault(x => x.id == id);
                if (cot != null)
                {
                    ViewState["EditarId"] = cot.id;

                    // Solo llenar el estado
                    txtEstado.Text = cot.estadoCotizacion;

                    ScriptManager.RegisterStartupScript(this, GetType(), "MostrarModalEditar", "mostrarModal();", true);
                }
            }

        }
    }
}