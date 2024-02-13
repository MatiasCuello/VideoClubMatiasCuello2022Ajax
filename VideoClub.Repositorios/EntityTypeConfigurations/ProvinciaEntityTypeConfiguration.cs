using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;

namespace VideoClub.Repositorios.EntityTypeConfigurations
{
    public class ProvinciaEntityTypeConfiguration:EntityTypeConfiguration<Provincia>
    {
        public ProvinciaEntityTypeConfiguration()
        {
            ToTable("Provincias");
        }
    }
}
