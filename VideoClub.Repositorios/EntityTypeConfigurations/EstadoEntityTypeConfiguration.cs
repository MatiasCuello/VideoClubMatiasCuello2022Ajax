﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;

namespace VideoClub.Repositorios.EntityTypeConfigurations
{
    public class EstadoEntityTypeConfiguration : EntityTypeConfiguration<Estado>
    {

        public EstadoEntityTypeConfiguration()
        {
            ToTable("Estados");
        }

    }
}
