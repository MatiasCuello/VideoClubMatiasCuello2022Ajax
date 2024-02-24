using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using VideoClub.Entidades.Entidades;
using VideoClub.Servicios.Servicios;
using VideoClub.Servicios.Servicios.Facades;
using VideoClub.WebMVC.App_Start;
using VideoClub.WebMVC.Models.Localidad;
using VideoClub.WebMVC.Models.Pelicula;

namespace VideoClub.WebMVC.Controllers
{
    public class LocalidadController : Controller
    {
        private readonly IServicioLocalidades servicio;
        private readonly IServicioProvincias servicioProvincias;
        private readonly IMapper mapper;

        public LocalidadController(IServicioLocalidades servicio, IServicioProvincias servicioProvincias)
        {
            this.servicio = servicio;
            this.servicioProvincias = servicioProvincias;
            mapper = AutoMapperConfig.Mapper;
        }

        // GET: Localidades
        //public JsonResult ListarLocalidades()
        //{
        //    var lista = servicio.GetLista2();
        //    return Json(new {data = lista}, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Index()
        {
            //var localidadEditVm = mapper.Map<List<LocalidadListVm>>(servicio.GetLista2());
            //localidadEditVm = localidadEditVm
            //    .OrderBy(c => c.NombreLocalidad)
            //    .ThenBy(c => c.Provincia)
            //    .ToList();
            //return View(localidadEditVm);
            return View();
        }
        public JsonResult ListarLocalidades()
        {
            var listaLocalidades = mapper.Map<List<LocalidadListVm>>(servicio.GetLista2());
            return Json(new { data = listaLocalidades }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetLocalidad(int localidadId)
        {
            var localidad = servicio.GetLocalidadPorId(localidadId);
            var localidadEncontrada = new Localidad()
            {
                LocalidadId = localidad.LocalidadId,
                NombreLocalidad = localidad.NombreLocalidad,
                ProvinciaId = localidad.ProvinciaId
            };
            return Json(localidadEncontrada, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(string objeto)
        {
            bool respuesta = true;
            string mensaje = string.Empty;

            Localidad localidad = new Localidad();
            localidad = JsonConvert.DeserializeObject<Localidad>(objeto);


            mensaje = ValidarLocalidad(localidad);
            if (mensaje != string.Empty)
            {
                respuesta = false;
                return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                if (servicio.Existe(localidad))
                {
                    respuesta = false;
                    mensaje = "Localidad existente!";
                    return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }
                servicio.Guardar(localidad);
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

        private string ValidarLocalidad(Localidad localidad)
        {
            StringBuilder mensaje = new StringBuilder();
            if (string.IsNullOrEmpty(localidad.NombreLocalidad))
            {
                mensaje.AppendLine("El nombre de la localidad es requerido" + "<br>");
            }
            else if (localidad.NombreLocalidad.Length > 50)
            {
                mensaje.AppendLine("El nombre de la localidad tiene más de 50 caracteres" + "<br>");
            }

            if (localidad.ProvinciaId == 0)
            {
                mensaje.AppendLine("Debe seleccionar una provincia");
            }

            return mensaje.ToString();
        }

        [HttpGet]
        public JsonResult Eliminar(int localidadId)
        {
            bool respuesta = true;
            string mensaje = string.Empty;
            try
            {
                var localidad = servicio.GetLocalidadPorId(localidadId);
                if (servicio.EstaRelacionado(localidad))
                {
                    respuesta = false;
                    mensaje = "Registro relacionado... baja denegada!!!";
                    return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }
                servicio.Borrar(localidadId);
                respuesta = true;
                mensaje = "Registro borrado !";
                return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                respuesta = false;
                mensaje = "Error al intentar dar de baja una ciudad";
                return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}