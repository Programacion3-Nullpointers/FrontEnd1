using JMQPresentacion.JMQWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMQPresentacion.Admin
{
    public partial class ModificarDescuentos : System.Web.UI.Page
    {
        private DescuentoWSClient descuentoWSClient;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || ((usuario)Session["Usuario"]).tipoUsuario != tipoUsuario.ADMIN)
            {
                // Redirigir al login u otra acción
                Response.Redirect("~/Login/Login.aspx");
                return;
            }
            descuentoWSClient = new JMQWS.DescuentoWSClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Si no hay lista guardada en sesión, inicialízala con algunos datos de ejemplo
                if (Session["Descuentos"] == null)
                {
                    List<descuento> listaInicial = new List<descuento>();
                    listaInicial = descuentoWSClient.ListarDescuentos().ToList();


                    Session["Descuentos"] = listaInicial;
                }

                // Mostrar la tabla
                List<descuento> lista = Session["Descuentos"] as List<descuento>;
                gvDescuentos.DataSource = lista.OrderBy(u => u.id).ToList();
                gvDescuentos.DataBind();
            }
        }

        protected void btnGuardarDescuento_Click(object sender, EventArgs e)
        {
            List<descuento> lista = Session["Descuentos"] as List<descuento>;
            if (lista == null) lista = new List<descuento>();

            int idEditar = ViewState["EditarId"] != null ? Convert.ToInt32(ViewState["EditarId"]) : 0;

            if (idEditar > 0)
            {
                // Editar usuario existente
                descuento user = lista.FirstOrDefault(x => x.id == idEditar);
                if (user != null)
                {
                    user.numDescuento = Convert.ToInt32(txtnumDescuento.Text);
                    
                    //if (txtactivo.Text == "Activo")
                    //{
                    //    user.activo = true;
                    //}
                    //else if (txtactivo.Text == "Inactivo")
                    //{
                    //    user.activo = false;
                    //}
                    
                }

                ViewState["EditarId"] = null;
            }
            else
            {
                // ➕ Buscar el menor ID disponible
                int nuevoId = Enumerable.Range(1, lista.Count + 1)
                        .Except(lista.Select(u => u.id))
                        .First();

                descuento nuevo = new descuento();
                nuevo.id = nuevoId;
                nuevo.numDescuento = Convert.ToInt32(txtnumDescuento.Text);
                //nuevo.activo = txtactivo.Text.Trim() == "Activo";


                descuentoWSClient.RegistrarDescuento(nuevo);
                lista.Add(nuevo);
            }

            // Guardar y actualizar
            Session["Descuentos"] = lista;
            gvDescuentos.DataSource = lista.OrderBy(u => u.id).ToList();
            gvDescuentos.DataBind();

            // Limpiar
            txtnumDescuento.Text = "";
            //txtactivo.Text = "";
            

            // Cerrar modal
            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal", "cerrarModal();", true);
        }

        protected void gvDescuentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (Session["Descuentos"] != null)
            {
                List<descuento> lista = (List<descuento>)Session["Descuentos"];

                if (e.CommandName == "Eliminar")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    descuento usuarioAEliminar = lista.FirstOrDefault(u => u.id == id);

                    if (usuarioAEliminar != null)
                    {
                        lista.Remove(usuarioAEliminar); // Eliminar de la lista
                        Session["Descuentos"] = lista;     // Guardar la lista actualizada en la sesión
                        descuentoWSClient.EliminarDescuento(id);

                        gvDescuentos.DataSource = lista.OrderBy(u => u.id).ToList();
                        gvDescuentos.DataBind();
                        // Refrescar la tabla
                    }
                }

                if (e.CommandName == "Editar")
                {
                    int id = Convert.ToInt32(e.CommandArgument);

                    // Obtener los datos del usuario desde la base de datos
                    var usuario = descuentoWSClient.BuscarDescuento(id); // Este método lo defines tú

                    if (usuario != null)
                    {
                        hfIdDescuento.Value = usuario.id.ToString();
                        txtDescuentoMod.Text = usuario.numDescuento.ToString();
                        //if (usuario.activo == true)
                        //{
                        //    txtActivoMod.Text = "Activo";
                        //}
                        //else
                        //{
                        //    txtActivoMod.Text = "Inactivo";
                        //}


                            // Mostrar modal de modificación
                            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModalModificar", "mostrarModalModificar();", true);

                    }
                }
            }
        }
        protected void btnActualizarDescuento_Click(object sender, EventArgs e)
        {
            List<descuento> lista = Session["Descuentos"] as List<descuento>;
            if (lista == null) return;

            int id = int.Parse(hfIdDescuento.Value);
            descuento user = lista.FirstOrDefault(u => u.id == id);

            if (user != null)
            {
                user.numDescuento = Convert.ToInt32(txtDescuentoMod.Text);
                //if (txtActivoMod.Text == "Activo")
                //{
                //    user.activo = true;
                //}
                //else if (txtActivoMod.Text == "Inactivo")
                //{
                //    user.activo = false;
                //}


                    descuentoWSClient.ActualizarDescuento(user);
            }

            Session["Descuentos"] = lista;
            gvDescuentos.DataSource = lista.OrderBy(u => u.id).ToList();
            gvDescuentos.DataBind();

            // Cerrar modal
            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModalModificar", "cerrarModalModificar();", true);
        }

    }
}