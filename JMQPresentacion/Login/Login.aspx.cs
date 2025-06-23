using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;

namespace JMQPresentacion.Login
{
    public partial class Login : System.Web.UI.Page
    {
        private UsuarioWSClient usuarioWSCLClient;

        protected void Page_Init(object sender, EventArgs e)
        {
            usuarioWSCLClient = new JMQWS.UsuarioWSClient();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Leer parámetro de redirección si existe en la URL
                string redireccion = Request.QueryString["redirect"];
                if (!string.IsNullOrEmpty(redireccion))
                {
                    Session["RedirectAfterLogin"] = "/" + redireccion.TrimStart('/');
                }

                // Si ya inició sesión, redirige
                if (Session["Usuario"] != null)
                {
                    Response.Redirect("/Principal/Principal.aspx");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            divError.Style["display"] = "none";
            lblError.Text = "";

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtContr.Text))
            {
                lblError.Text = "Complete todos los campos.";
                divError.Style["display"] = "block";
                return;
            }

            usuario user = usuarioWSCLClient.BuscarUsuarioPorCorreo(txtEmail.Text);

            if (user != null)
            {
                if (user.contrasena == txtContr.Text)
                {
                    Session["Usuario"] = user;

                    // Obtener nombre
                    string nombreMostrar = user.nombreUsuario.Split(' ')[0];

                    // Si es admin, redirigir directamente sin JS
                    if (user.tipoUsuario == tipoUsuario.ADMIN)
                    {
                        Response.Redirect("/Admin/PrincipalAdmin.aspx");
                        return;
                    }

                    // Si viene de una página protegida, redirigir sin esperar JS
                    if (Session["RedirectAfterLogin"] != null)
                    {
                        string redirect = Session["RedirectAfterLogin"].ToString();
                        Session.Remove("RedirectAfterLogin");
                        Response.Redirect(redirect);
                        return;
                    }

                    // Redirección común con SweetAlert
                    string script = $@"
                        Swal.fire({{
                            icon: 'success',
                            title: '¡Bienvenido, {nombreMostrar}!',
                            text: 'Nos alegra tenerte de vuelta.',
                            timer: 1800,
                            showConfirmButton: false
                        }});
                        setTimeout(function() {{
                            window.location.href = '/Principal/Principal.aspx';
                        }}, 1800);";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "bienvenidaLogin", script, true);
                    return;
                }

                else
                {
                    lblError.Text = "Contraseña incorrecta.";
                    divError.Style["display"] = "block";
                    return;
                }
            }
            else
            {
                lblError.Text = "Usuario no encontrado.";
                divError.Style["display"] = "block";
                return;
            }
        }
    }
}
