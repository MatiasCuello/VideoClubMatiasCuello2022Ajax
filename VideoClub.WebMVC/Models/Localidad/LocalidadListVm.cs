﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using VideoClub.Entidades.Entidades;

namespace VideoClub.WebMVC.Models.Localidad
{
    public class LocalidadListVm
    {
        public int LocalidadId { get; set; }

        [DisplayName("Localidad")] 
        public string NombreLocalidad { get; set; }

        [DisplayName("Provincia")] 
        public string Provincia { get; set; }




    }
}