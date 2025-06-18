using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMQDominio
{
    public class Cotizacion
    {
        public Cotizacion(int id, Usuario usuario, string estadoCotizacion)
        {
            this.id = id;
            this.usuario = usuario;
            this.estadoCotizacion = estadoCotizacion;
        }
        public int id { get; set; }
        public Usuario usuario { get; set; }
        public String estadoCotizacion { get; set; }
        public List<ProductoCotizacion> productos { get; set; }

    }
}
