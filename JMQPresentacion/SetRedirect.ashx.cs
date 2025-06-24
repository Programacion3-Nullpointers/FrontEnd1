using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace JMQPresentacion
{
    /// <summary>
    /// Descripción breve de SetRedirect
    /// </summary>
    public class SetRedirect : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string url = context.Request.QueryString["url"];
            if (!string.IsNullOrEmpty(url))
            {
                context.Session["RedirectAfterLogin"] = url;
            }
        }

        public bool IsReusable => false;
    }
}