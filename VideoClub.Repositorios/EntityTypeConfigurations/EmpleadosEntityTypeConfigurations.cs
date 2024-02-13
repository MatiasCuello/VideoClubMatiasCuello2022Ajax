using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;

namespace VideoClub.Repositorios.EntityTypeConfigurations
{
    public class EmpleadosEntityTypeConfigurations:EntityTypeConfiguration<Empleado>
    {
        public EmpleadosEntityTypeConfigurations()
        {
            ToTable("Empleados");
        }
    }
}
