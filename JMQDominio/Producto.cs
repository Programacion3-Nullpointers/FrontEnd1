using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMQDominio
{
    public class Producto
    {
        public Producto(int idProducto, string nombre, string descripcion, int stock, double precio, Categoria categoria, string imagen)
        {
            IdProducto = idProducto;
            Nombre = nombre;
            Descripcion = descripcion;
            Stock = stock;
            Precio = precio;
            Categoria = categoria;
            Imagen = imagen;
        }
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public double Precio { get; set; }
        public Categoria Categoria { get; set; }
        public string Imagen { get; set; }

    }
}
