﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;

namespace VideoClub.Repositorios.Repositorios
{
    public class CalificacionEntityTypeConfiguration:EntityTypeConfiguration<Calificacion>
    {
        public CalificacionEntityTypeConfiguration()
        {
            ToTable("Calificaciones");
        }
    }
}
