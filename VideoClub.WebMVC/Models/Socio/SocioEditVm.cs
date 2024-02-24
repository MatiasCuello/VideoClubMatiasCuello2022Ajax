using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoClub.WebMVC.Models.Socio
{
    public class SocioEditVm
    {
        public int SocioId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "el campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 2)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "el campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 2)]
        public string Apellido { get; set; }

        [DisplayName("TipoDoc")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un Tipo de Documento")]
        public int TipoDeDocumentoId { get; set; }

        [DisplayName("Nro Doc")]

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(8, ErrorMessage = "el campo {0} debe tener {8} caracteres", MinimumLength = 8)]
        public string NroDocumento { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "el campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 2)]
        public string Direccion { get; set; }

        [DisplayName("Localidad")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una Localidad")]
        public int LocalidadId { get; set; }

        [DisplayName("Provincia")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una Provincia")]
        public int ProvinciaId { get; set; }

        [DisplayName("Correo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "el campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 2)]
        public string CorreoElectronico { get; set; }

        [DisplayName("Tel. FIjo")]
        public string TelefonoFijo { get; set; }

        [DisplayName("Tel. Movil")]
        public string TelefonoMovil { get; set; }

        [DisplayName("FechaNac")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "el campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 2)]
        public string FechaDeNacimiento { get; set; }
        public bool Activo { get; set; } = true;
        public bool Sancionado { get; set; } = false;

        public Entidades.Entidades.TipoDeDocumento TipoDeDocumento { get; set; }
        public Entidades.Entidades.Localidad Localidad { get; set; }
        public Entidades.Entidades.Provincia Provincia { get; set; }
    }
}