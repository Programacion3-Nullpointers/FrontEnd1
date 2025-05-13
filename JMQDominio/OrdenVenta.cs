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
            this.id = id;
            this.estado_compra = estado_compra;
            this.fecha_orden = fecha_orden;
            this.usuario = usuario;
        }
        public int id { get; set; }
        public EstadoCompra estado_compra { get; set; }
        public DateTime fecha_orden { get; set; }
        public bool activo {  get; set; }
        public Usuario usuario { get; set; }
        public List<Detalle> detalles { get; set; }
    }
}
