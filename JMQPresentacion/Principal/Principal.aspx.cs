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
                CargarCategoriasDesdeWS();
                CargarProductosFiltrados();

                //LinkButton boton = Master.FindControl("lbCerrarSesion") as LinkButton;
                //boton.Visible = Session["Usuario"] != null;

                //if (Request.QueryString["logout"] == "1")
                //{
                //    string script = @"
                //    Swal.fire({
                //        icon: 'success',
                //        title: 'Sesión cerrada',
                //        text: 'Has cerrado sesión correctamente.',
                //        confirmButtonColor: '#3085d6'
                //    });";

                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "logoutAlert", script, true);
                //}
                // ✅ Mostrar bienvenida si viene de Login
                LinkButton boton = Master.FindControl("btnLogout") as LinkButton;
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

        protected void btnBuscarNombre_Click(object sender, EventArgs e)
        {
            string termino = txtBuscarNombre.Text.Trim().ToLower();
            var lista = (Session["TodosLosProductos"] as producto[])?.ToList();

            if (!string.IsNullOrEmpty(termino) && lista != null)
            {
                var resultado = lista
                    .Where(p => p.nombre != null && p.nombre.ToLower().Contains(termino))
                    .ToList();

                lblMensaje.Visible = resultado.Count == 0;
                lblMensaje.Text = resultado.Count == 0 ? "⚠️ Producto no encontrado." : "";

                rptProductos.DataSource = resultado;
                rptProductos.DataBind();
            }
            else
            {
                lblMensaje.Text = "Ingrese un nombre válido para buscar.";
                lblMensaje.Visible = true;
                rptProductos.DataSource = null;
                rptProductos.DataBind();
            }
        }

        private void CargarProductosFiltrados()
        {
            string categoriaNombre = ddlCategoria.SelectedValue;
            bool activo = true;

            bool filtroCategoria = !string.IsNullOrEmpty(categoriaNombre) && categoriaNombre != "0";
            bool filtroOfertas = chkOfertas.Checked;

            producto[] productosFiltrados;

            if (!filtroCategoria && !filtroOfertas)
            {
                productosFiltrados = productoService.listaProducto();
                Session["TodosLosProductos"] = productosFiltrados;
            }
            else
            {
                string categoriaFinal = filtroCategoria ? categoriaNombre : null;
                bool conDescuento = filtroOfertas;

                productosFiltrados = productoService.filtrarProductos(
                    categoriaFinal,
                    activo,
                    0.0,
                    double.MaxValue,
                    0,
                    int.MaxValue,
                    conDescuento
                );
                Session["TodosLosProductos"] = productosFiltrados;
            }

            lblMensaje.Visible = productosFiltrados == null || !productosFiltrados.Any();
            lblMensaje.Text = "⚠️ No se encontraron productos.";

            rptProductos.DataSource = productosFiltrados;
            rptProductos.DataBind();
        }

        private void CargarCategoriasDesdeWS()
        {
            CategoriaWSClient categoriaService = new CategoriaWSClient();
            var categorias = categoriaService.ListarCategorias();

            ddlCategoria.Items.Clear();
            ddlCategoria.Items.Add(new ListItem("Todas las categorías", "")); // default

            foreach (var cat in categorias)
            {
                ddlCategoria.Items.Add(new ListItem(cat.nombre, cat.nombre));
            }
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarProductosFiltrados();
        }

        protected void chkOfertas_CheckedChanged(object sender, EventArgs e)
        {
            CargarProductosFiltrados();
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

        //protected void btnAdmin_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/Admin/ListaCotizaciones.aspx");
        //}


    }
}
