using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Entidades.Entidades
{
    public class Localidad:ICloneable
    {
        //public Localidad()
        //{

        //    Socios = new HashSet<Socio>();
        //    Proveedores = new HashSet<Proveedor>();
        //    Empleados = new HashSet<Empleado>();
        //}
        public int LocalidadId { get; set; }
        public string NombreLocalidad { get; set; }
        public int ProvinciaId { get; set; }

        public virtual Provincia Provincia { get; set; }

        //public virtual ICollection<Socio> Socios { get; set; }
        //public virtual ICollection<Proveedor> Proveedores { get; set; }
        //public virtual ICollection<Empleado> Empleados { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
