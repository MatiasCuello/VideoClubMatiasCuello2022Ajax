using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace VideoClub.WebMVC.Models.Proveedor
{
    public class ProveedorListVm
    {
        public int ProveedorId { get; set; }
        public string CUIT { get; set; }

        [DisplayName("Razon Social")]
        public string RazonSocial { get; set; }

        [DisplayName("Pers. Contacto")]
        public string PersonaDeContacto { get; set; }
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

        public Entidades.Entidades.Localidad Localidad { get; set; }
        public Entidades.Entidades.Provincia Provincia { get; set; }
    }
}