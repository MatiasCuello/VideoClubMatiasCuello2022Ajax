using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VideoClub.WebMVC.Models.Localidad
{
    public class LocalidadEditVm
    {
        public int LocalidadId { get; set; }

        [Required(ErrorMessage = "El nombre de la localidad es requerido")]
        [StringLength(50, ErrorMessage = "El nombre debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Localidad")]
        public string NombreLocalidad { get; set; }

        [Required(ErrorMessage = "La provincia es requerida")]
        [Display(Name = "Provincia")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una provincia")]
        public int ProvinciaId { get; set; }

        public List<SelectListItem> Provincias { get; set; }
    }
}