using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMQDominio
{
    public class Entrega
    {
        public int id { get; set; }
        public OrdenVenta orden { get; set; }
        public DateTime fechaEntrega { get; set; }
        public string direccion { get; set; }
        public string dniRecibo { get; set; }
        public TipoEntrega tipoEntrega { get; set; }

        // Constructor vacío
        public Entrega() { }

        // Constructor con inicialización de propiedades
        public Entrega(int id, OrdenVenta orden, DateTime fechaEntrega, string direccion, string dniRecibo, TipoEntrega tipoEntrega)
        {
            this.id = id;
            this.orden = orden;
            this.fechaEntrega = fechaEntrega;
            this.direccion = direccion;
            this.dniRecibo = dniRecibo;
            this.tipoEntrega = tipoEntrega;
        }
    }

}
