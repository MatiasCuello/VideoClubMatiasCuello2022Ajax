using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace VideoClub.WebMVC.Models.Empleado
{
    public class EmpleadoListVm
    {
        public int EmpleadoId { get; set; }
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

        [DisplayName("Tel. FIjo")]
        public string TelefonoFijo { get; set; }

        [DisplayName("Tel. Movil")]
        public string TelefonoMovil { get; set; }


        [DisplayName("Correo")]
        public string CorreoElectronico { get; set; }

        public Entidades.Entidades.TipoDeDocumento TipoDeDocumento { get; set; }
        public Entidades.Entidades.Localidad Localidad { get; set; }
        public Entidades.Entidades.Provincia Provincia { get; set; }
    }
}