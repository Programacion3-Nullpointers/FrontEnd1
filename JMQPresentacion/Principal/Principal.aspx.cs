using JMQPresentacion.Cotizaciones;
using JMQPresentacion.JMQWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMQPresentacion.Principal
{
    public partial class Principal : System.Web.UI.Page
    {
        private ProductoWSClient productoService;

        protected void Page_Init(object sender, EventArgs e)
        {
            productoService = new ProductoWSClient();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();

                LinkButton boton = Master.FindControl("lbCerrarSesion") as LinkButton;
                boton.Visible = Session["Usuario"] != null;

                if (Request.QueryString["logout"] == "1")
                {
                    string script = @"
                    Swal.fire({
                        icon: 'success',
                        title: 'Sesión cerrada',
                        text: 'Has cerrado sesión correctamente.',
                        confirmButtonColor: '#3085d6'
                    });";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "logoutAlert", script, true);
                }
                // ✅ Mostrar bienvenida si viene de Login
                if (Session["MostrarBienvenida"] != null)
                {
                    string nombre = Session["MostrarBienvenida"].ToString();
                    Session.Remove("MostrarBienvenida");

                    string script = $@"
                        Swal.fire({{
                            icon: 'success',
                            title: '¡Bienvenido, {nombre}!',
                            text: 'Nos alegra tenerte de vuelta.',
                            timer: 1800,
                            showConfirmButton: false
                        }});";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "bienvenidaLogin", script, true);
                }
            }
        }

        private void CargarProductos()
        {
            List<producto> productos = productoService.listaProducto().ToList();

            // Filtrar productos con stock > 0
            List<producto> productosConStock = productos.Where(p => p.stock > 0).ToList();

            rptProductos.DataSource = productosConStock;
            rptProductos.DataBind();
        }

        public string ConvertirByteAImagenBase64(byte[] datosImagen)
        {
            if (datosImagen == null || datosImagen.Length == 0)
            {
                string rutaImagenDefecto = Server.MapPath("~/Public/images/imagen_default.jpg");
                byte[] bytesImagenDefecto = System.IO.File.ReadAllBytes(rutaImagenDefecto);
                return "data:image/jpeg;base64," + Convert.ToBase64String(bytesImagenDefecto);
            }
            return "data:image/jpeg;base64," + Convert.ToBase64String(datosImagen);
        }

        protected void btnCotizar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Cotizaciones/ListaCotizaciones.aspx");
        }
    }
}
