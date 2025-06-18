using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMQDominio
{
    public class Descuento
    {

        public Descuento(int id, int porcentaje)
        {
            this.id = id;
            this.num_porcentaje = porcentaje;
        }
        public int id { get; set; }
        public int num_porcentaje { get; set; }

    }
}
