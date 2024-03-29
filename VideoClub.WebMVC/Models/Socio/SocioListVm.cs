﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoClub.WebMVC.Models.Socio
{
    public class SocioListVm
    {
        public int SocioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        [DisplayName("Tipo Doc")]
        public int TipoDeDocumentoId { get; set; }

        [DisplayName("Nro Doc")]
        public string NroDocumento { get; set; }

        public string Direccion { get; set; }

        [DisplayName("Localidad")]
        public int LocalidadId { get; set; }

        [DisplayName("Provincia")]
        public int ProvinciaId { get; set; }

        [DisplayName("Correo")]
        public string CorreoElectronico { get; set; }

        [DisplayName("Tel. FIjo")]
        public string TelefonoFijo { get; set; }

        [DisplayName("Tel. Movil")]
        public string TelefonoMovil { get; set; }

        [DisplayName("Fecha Nac")]
        public string FechaDeNacimiento { get; set; }
        public bool Activo { get; set; } = true;
        public bool Sancionado { get; set; } = false;

        public Entidades.Entidades.TipoDeDocumento TipoDeDocumento { get; set; }
        public Entidades.Entidades.Localidad Localidad { get; set; }
        public Entidades.Entidades.Provincia Provincia { get; set; }
    }
}