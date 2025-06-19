using JMQPresentacion.JMQWS;
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

        private CotizacionWSClient cotizacionWSClient;
        private UsuarioWSClient usuarioWSClient;
        protected void Page_Init(object sender, EventArgs e)
        {
            cotizacionWSClient = new JMQWS.CotizacionWSClient();
            usuarioWSClient = new UsuarioWSClient();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Acceso/NoAutorizado.aspx");
                return;
            }

            if (!IsPostBack)
            {
                if (Session["Cotizaciones"] == null)
                {
                    usuario demoUser = new usuario{
                        id = 1,
                        nombreUsuario = "admin01",
                        contrasena = "123456",
                        activo = true,
                        correo = "admin@jmq.com",
                        tipoUsuario =new tipoUsuario(), 
                        razonsocial = "JMQ SAC", 
                        direccion = "Av. Principal 123", 
                        RUC = "20604010123"
                    };


                    //List<cotizacion> listaInicial = new List<cotizacion>
                    //{
                    //    new cotizacion{id = 1,usuario = demoUser,estadoCotizacion = "Pendiente" },
                    //    new cotizacion{id = 2,usuario = demoUser, estadoCotizacion = "Aprobada" }
                    //};

                    List<cotizacion> listaInicial = cotizacionWSClient.listarCotizaciones().ToList();

                    Session["Cotizaciones"] = listaInicial;
                }

                CargarGrid();
            }
        }

        private void CargarGrid()
        {
            Session["Cotizaciones"] = cotizacionWSClient.listarCotizaciones().ToList();
            List<cotizacion> lista = Session["Cotizaciones"] as List<cotizacion>;

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
            List<cotizacion> lista = Session["Cotizaciones"] as List<cotizacion>;
            if (lista == null) lista = new List<cotizacion>();

            int? idEditando = ViewState["EditarId"] as int?;

            if (idEditando != null)
            {
                // Modo edición: solo se cambia el estado
                var cotExistente = lista.FirstOrDefault(c => c.id == idEditando.Value);
                if (cotExistente != null)
                {
                    cotExistente.estadoCotizacion = txtEstado.Text;
                    
                    cotizacionWSClient.actualizarEstadoCotizacion(cotExistente.id,cotExistente.estadoCotizacion);
                }
                ViewState["EditarId"] = null;
            }
            else
            {
                // Modo nuevo: requiere crear usuario completo
                int nuevoId = Enumerable.Range(1, lista.Count + 1)
                                        .Except(lista.Select(c => c.id))
                                        .First();

                //usuario nuevoUsuario = new usuario {
                //    id = 0,nombreUsuario = txtNombreUsuario.Text,dni = "",activo = true,correo = txtCorreo.Text,
                //    tipoUsuario = new tipoUsuario(),razonsocial = txtRazonSocial.Text, direccion = txtDireccion.Text,
                //    RUC = txtRUC.Text
                //};
                usuario nuevoUsuario = usuarioWSClient.BuscarUsuarioPorCorreo(txtCorreo.Text);

                cotizacion nueva = new cotizacion { id = nuevoId, usuario = nuevoUsuario, estadoCotizacion = txtEstado.Text };
                lista.Add(nueva);

            }

            Session["Cotizaciones"] = cotizacionWSClient.listarCotizaciones();
            CargarGrid();

            // Limpiar solo el campo de estado
            txtEstado.Text = "";

            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal", "cerrarModal();", true);
        }

        protected void gvCotizaciones_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            List<cotizacion> lista = Session["Cotizaciones"] as List<cotizacion>;
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                var cot = lista.FirstOrDefault(x => x.id == id);
                if (cot != null)
                {
                    //lista.Remove(cot);
                    
                    Session["Cotizaciones"] = lista;
                    cotizacionWSClient.eliminarCotizacion(cot.id);
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
                    //txtEstado.Text = cot.estadoCotizacion;
                    //string estadoCoti = txtEstado.Text;
                    //cotizacionWSClient.actualizarEstadoCotizacion(cot.id, estadoCoti);
                    ScriptManager.RegisterStartupScript(this, GetType(), "MostrarModalEditar", "mostrarModal();", true);
                }
            }

        }
    }
}