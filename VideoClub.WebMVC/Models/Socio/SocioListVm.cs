using System;
using System.Collections.Generic;
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

        [Display(Name = "TipoDeDocumento")]
        public int TipoDeDocumentoId { get; set; }
        public string NumeroDeDocumento { get; set; }

        public string Direccion { get; set; }

        [Display(Name = "Localidad")]
        public int LocalidadId { get; set; }

        [Display(Name = "Provincia")]
        public int ProvinciaId { get; set; }
        public string CorreoElectronico { get; set; }

        public DateTime FechaDeNacimiento { get; set; }
        public bool Activa { get; set; } = true;
        public bool Alquilado { get; set; } = false;

        public Entidades.Entidades.TipoDeDocumento TipoDeDocumento { get; set; }
        public Entidades.Entidades.Localidad Localidad { get; set; }
        public Entidades.Entidades.Provincia Provincia { get; set; }
    }
}