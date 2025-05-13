using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMQDominio
{
    public class Usuario
    {
        public Usuario(int id, string nombreUsuario, string contrasena, bool activo, string correo, TipoUsuario tipoUsuario, string razonsocial, string direccion, string RUC)
        {
            this.id = id;
            this.nombreUsuario = nombreUsuario;
            this.contrasena = contrasena;
            this.activo = activo;
            this.correo = correo;
            this.tipoUsuario = tipoUsuario;
            this.razonsocial = razonsocial;
            this.direccion = direccion;
            this.RUC = RUC;
        }
        public int id { get; set; }
        public string nombreUsuario { get; set; }
        public string contrasena { get; set; }
        public bool activo { get; set; }
        public string correo { get; set; }
        public TipoUsuario tipoUsuario { get; set; }
        public string dni { get; set; }
        public string razonsocial { get; set; }
        public string direccion { get; set; }
        public string RUC { get; set; }

    }
}
