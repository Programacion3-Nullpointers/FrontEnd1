using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMQPresentacion.Pedidos
{
    public partial class Cotiza : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Clear();
            }
        }
        protected void SubmitForm(object sender, EventArgs e)
        {
            string[] descriptions = Request.Form.GetValues("desc");
            string[] quantities = Request.Form.GetValues("cantidad");

            if (descriptions != null && quantities != null)
            {
                for (int i = 0; i < descriptions.Length; i++)
                {
                    Response.Write($"Descripción: {descriptions[i]}, Cantidad: {quantities[i]}<br>");
                }
            }
        }
    }
}