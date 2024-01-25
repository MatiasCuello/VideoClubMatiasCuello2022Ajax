using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Entidades.Entidades
{
    public class Provincia : ICloneable
    {
        public int ProvinciaId { get; set; }
        public string NombreProvincia { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
