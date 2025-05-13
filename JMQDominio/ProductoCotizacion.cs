using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMQDominio
{
    public class ProductoCotizacion
    {
        public ProductoCotizacion(int id, string descripcion, int cantidad, double precioCotizado, Cotizacion cotizacion)
        {
            this.id = id;
            this.descripcion = descripcion;
            this.cantidad = cantidad;
            this.precioCotizado = precioCotizado;
        }
        public int id { get; set; }
        public String descripcion { get; set; }
        public int cantidad { get; set; }
        public double precioCotizado { get; set; }

    }
}
