using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMQDominio
{
    public class OrdenVenta
    {
        public OrdenVenta()
        {
        }

        public OrdenVenta(int id, EstadoCompra estado_compra, DateTime fecha_orden, Usuario usuario)
        {
            this.Id = id;
            this.Estado_compra = estado_compra;
            this.Fecha_orden = fecha_orden;
            this.Usuario = usuario;
        }
        public int Id { get; set; }
        public EstadoCompra Estado_compra { get; set; }
        public DateTime Fecha_orden { get; set; }
        public bool Activo {  get; set; }
        public Usuario Usuario { get; set; }
        public List<Detalle> Detalles { get; set; }
    }
}
