using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMQPresentacion.JMQWS;

namespace JMQPresentacion
{
    public partial class MainLayout2 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null || ((usuario)Session["Usuario"]).tipoUsuario != tipoUsuario.ADMIN)
            {
                Response.Redirect("/Principal/Principal.aspx");
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["Usuario"] = null;
            Response.Redirect("/Principal/Principal.aspx");
        }
    }
}