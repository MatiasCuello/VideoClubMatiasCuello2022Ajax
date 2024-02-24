using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using VideoClub.Entidades.Entidades;
using VideoClub.Servicios.Servicios.Facades;
using VideoClub.WebMVC.App_Start;
using VideoClub.WebMVC.Models.Localidad;
using VideoClub.WebMVC.Models.Provincia;
using VideoClub.WebMVC.Models.Socio;

namespace VideoClub.WebMVC.Controllers
{
    public class SocioController : Controller
    {
        private readonly IServicioSocios servicio;
        private readonly IServicioTiposDeDocumentos servicioTipoDoc;
        private readonly IServicioLocalidades servicioLocalidades;
        private readonly IServicioProvincias servicioProvincias;
        private readonly IMapper mapper;

        public SocioController(IServicioSocios servicio, IServicioTiposDeDocumentos servicioTipoDoc, IServicioLocalidades serIservicioLocalidades, IServicioProvincias servicioProvincias)
        {
            this.servicio = servicio;
            this.servicioTipoDoc = servicioTipoDoc;
            this.servicioLocalidades = serIservicioLocalidades;
            this.servicioProvincias = servicioProvincias;
            mapper = AutoMapperConfig.Mapper;
        }
        // GET: Socio
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarSocios()
        {
            var listaSociosVm = mapper.Map<List<SocioListVm>>(servicio.GetLista());
            return Json(new { data = listaSociosVm }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarSocio(int socioId)
        {
            var socioVm = mapper.Map<SocioEditVm>(servicio.GetSocioPorId(socioId));
            return Json(socioVm, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarProvincias()
        {
            var listaProvinciasVm = mapper.Map<List<ProvinciaListVm>>(servicioProvincias.GetLista());
            return Json(new { data = listaProvinciasVm }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarLocalidades(int provinciaId)
        {
            var listaLocalidadesVm = mapper.Map<List<LocalidadListVm>>(servicioLocalidades.GetLista(provinciaId));
            return Json(new { data = listaLocalidadesVm }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarSocio(string objeto)
        {
            bool respuesta = true;
            string mensaje = string.Empty;

            Socio socio = new Socio();
            socio = JsonConvert.DeserializeObject<Socio>(objeto);

            mensaje = ValidarSocio(socio);
            if (mensaje != string.Empty)
            {
                respuesta = false;
                return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                if (servicio.Existe(socio))
                {
                    respuesta = false;
                    mensaje = "Socio existente!";
                    return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }
                servicio.Guardar(socio);
                respuesta = true;
                mensaje = "Registro guardado";
                return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                respuesta = false;
                mensaje = "Error al intentar guardar el registro";
                return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

            }
        }
        private string ValidarSocio(Socio socio)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(socio.Nombre))
            {
                sb.AppendLine("Nombre del socio es requerido");
            }
            if (string.IsNullOrEmpty(socio.Apellido))
            {
                sb.AppendLine("Apellido del socio es requerido");
            }
            if (socio.TipoDeDocumentoId == 0)
            {
                sb.AppendLine("Debe seleccionar una tipo de documento");
            }
            if ((socio.NroDocumento == null))
            {
                sb.AppendLine("Numero de Documento del socio es requerido");
            }
            if (string.IsNullOrEmpty(socio.Direccion))
            {
                sb.AppendLine("La direccion es requerida");
            }
            if (socio.ProvinciaId == 0)
            {
                sb.AppendLine("Debe seleccionar una provincia");
            }
            if (socio.LocalidadId == 0)
            {
                sb.AppendLine("Debe seleccionar una localidad");
            }
           


            return sb.ToString();
        }
        [HttpPost]
        public JsonResult EliminarSocio(int socioId)
        {
            var respuesta = true;
            var mensaje = string.Empty;
            try
            {
                var socio = servicio.GetSocioPorId(socioId);
                if (servicio.EstaRelacionado(socio))
                {
                    respuesta = false;
                    mensaje = "Socio relacionado... \nNo se pudo eliminar el socio";
                    return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }
                servicio.Borrar(socio.SocioId);
                respuesta = true;
                mensaje = "Socio eliminado!";
                return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                respuesta = false;
                mensaje = "Error al intentar eliminar el socio";
                return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}