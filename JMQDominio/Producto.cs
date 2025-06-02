using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMQDominio
{
    public class Producto
    {
        public Producto(int idProducto, string nombre, string descripcion, int stock, double precio, Categoria categoria, string imagen)
        {
            this.idProducto = idProducto;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.stock = stock;
            this.precio = precio;
            this.categoria = categoria;
            this.imagen = imagen;
            activo = true;
        }

        public Producto()
        {

        }
        public int idProducto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int stock { get; set; }
        public double precio { get; set; }
        public Categoria categoria { get; set; }
        public string imagen { get; set; }
        public bool activo { get; set; }

    }
}
