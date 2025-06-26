using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace JMQPresentacion.Principal
{
    public partial class PrincipalAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Validar si el usuario ha iniciado sesión
            if (Session["Usuario"] != null)
            {
                var usuario = (JMQPresentacion.JMQWS.usuario)Session["Usuario"];
                string nombre = usuario.nombreUsuario.Split(' ')[0];
                litNombreAdmin.Text = nombre;
            }
        }
    }
}