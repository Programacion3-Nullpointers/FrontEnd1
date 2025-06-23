using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;  // ← Necesario para acceder a Session
using JMQPresentacion.JMQWS;

namespace JMQPresentacion
{
    public class AgregarCarrito : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            try
            {
                // Protección por si la sesión sigue sin estar disponible
                if (context.Session == null)
                {
                    context.Response.Write("{\"success\": false, \"message\": \"Sesión no disponible.\"}");
                    return;
                }

                int idProducto = int.Parse(context.Request.QueryString["id"]);
                ProductoWSClient productoService = new ProductoWSClient();
                producto producto = productoService.buscarProducto(idProducto);

                if (producto == null)
                {
                    context.Response.Write("{\"success\": false, \"message\": \"Producto no encontrado.\"}");
                    return;
                }

                List<detalle> carrito = (List<detalle>)context.Session["Cart"];
                if (carrito == null)
                {
                    carrito = new List<detalle>();
                }

                detalle existente = carrito.Find(d => d.producto.id == idProducto);
                if (existente != null)
                {
                    existente.cantidad++;
                }
                else
                {
                    carrito.Add(new detalle
                    {
                        producto = producto,
                        cantidad = 1,
                        precio_unitario = producto.precio
                    });
                }

                context.Session["Cart"] = carrito;
                context.Response.Write("{\"success\": true, \"message\": \"Producto agregado al carrito.\"}");
            }
            catch (System.Exception ex)
            {
                context.Response.Write("{\"success\": false, \"message\": \"Error: " + ex.Message + "\"}");
            }
        }

        public bool IsReusable => false;
    }
}
