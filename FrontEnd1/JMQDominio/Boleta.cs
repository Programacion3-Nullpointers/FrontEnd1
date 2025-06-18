using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMQDominio
{
    public class Boleta : ComprobantePago
    {
        public Boleta(int id, OrdenVenta orden, MetodoPago metodoPago, DateTime fecha_pago, double monto_total,
             string dni, string nombre, DateTime fecha_emision)
             : base(id, orden, metodoPago, fecha_pago, monto_total)
        {
         
            this.dni = dni;
            this.nombre = nombre;
            this.fecha_emision = fecha_emision;
        }
        public string dni {  get; set; }

        public string nombre { get; set; }

        public DateTime fecha_emision { get; set; }
    }
}
