﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoClub.WebMVC.Models.Tipo_de_Documento
{
    public class TipoDeDocumentoEditVm
    {
        public int TipoDeDocumentoId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "el campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 2)]
        public string Descripcion { get; set; }
    }
}