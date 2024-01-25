using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoClub.WebMVC.Models.Provincias
{
    public class ProvinciaEditVm
    {
        public int ProvinciaId { get; set; }

        [DisplayName("Descripcion")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "el campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 2)]
        public string NombreProvincia { get; set; }
    }
}