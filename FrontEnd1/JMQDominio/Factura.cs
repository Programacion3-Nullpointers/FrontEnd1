using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMQDominio
{
    public class Factura : ComprobantePago
    {

        public Factura(string RUC, string razon_social, string direccion, DateTime fecha_emision, int id, 
            OrdenVenta orden, MetodoPago metodoPago, DateTime fecha_pago, double monto_total)
            : base(id, orden, metodoPago, fecha_pago, monto_total)
        {
          
            this.RUC = RUC;
            this.razon_social = razon_social;
            this.direccion = direccion;
            this.fecha_emision = fecha_emision;
        }


        public string RUC {  get; set; }
        public string razon_social { get; set; }
        public string direccion { get; set; }
        public DateTime fecha_emision  { get; set; }
    }
}
