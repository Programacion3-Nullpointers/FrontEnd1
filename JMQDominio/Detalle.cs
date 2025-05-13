using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JMQDominio
{
    public class Detalle
    {
        public Detalle(int id, Producto producto, int cantidad, double precio_unitario)
        {
            this.id = id;
            this.producto = producto;
            this.cantidad = cantidad;
            this.precio_unitario = precio_unitario;
        }

        public int id {  get; set; }
        public Producto producto { get; set; }
        public int cantidad { get; set; }
        public double precio_unitario { get; set; }
    }
}
