using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMQDominio
{
    public class ComprobantePago
    {
      
        public ComprobantePago(int id, OrdenVenta orden, MetodoPago metodoPago, DateTime fecha_pago, double monto_total)
        {
            this.id = id;
            this.orden = orden;
            this.metodoPago = metodoPago;
            this.fecha_pago = fecha_pago;
            this.monto_total = monto_total;
        }

        public int id;
        public OrdenVenta orden;
        public MetodoPago metodoPago;
        public DateTime fecha_pago;
        public double monto_total;
    }
}
