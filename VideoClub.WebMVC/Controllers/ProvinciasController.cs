using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using VideoClub.Entidades.Entidades;
using VideoClub.Servicios.Servicios;
using VideoClub.Servicios.Servicios.Facades;
using VideoClub.WebMVC.App_Start;
using VideoClub.WebMVC.Models.Calificacion;
using VideoClub.WebMVC.Models.Provincia;
using VideoClub.WebMVC.Models.Provincias;

namespace VideoClub.WebMVC.Controllers
{
    public class ProvinciasController : Controller
    {
        // GET: Provincia
        private readonly IServicioProvincias servicio;
        private readonly IMapper mapper;

        public ProvinciasController(ServicioProvincias servicio)
        {
            this.servicio = servicio;
            mapper = AutoMapperConfig.Mapper;
        }

        public ActionResult Index()
        {
            var lista = servicio.GetLista();
            return View(lista);
        }

        [HttpGet]
        public JsonResult ListarProvincias()
        {
            var listaVm = mapper.Map<List<ProvinciaListVm>>(servicio.GetLista());

            return Json(new { data = listaVm }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetProvincia(int provinciaId)
        {
            var provinciaVm = mapper.Map<ProvinciaEditVm>(servicio.GetProvinciaPorId(provinciaId));
            return Json(provinciaVm, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Guardar(Provincia provincia)
        {
            bool respuesta = true;
            string mensaje = string.Empty;

            if (servicio.Existe(provincia))
            {
                respuesta = false;
                mensaje = "Provincia Repetida!";
                return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                servicio.Guardar(provincia);
                respuesta = true;
                mensaje = "Registro guardado";
                return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                respuesta = false;
                mensaje = "Error al intentar guardar el registro";
                return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult Eliminar(int provinciaId)
        {
            var respuesta = true;
            var mensaje = string.Empty;
            try
            {
                var provincia = servicio.GetProvinciaPorId(provinciaId);
                if (servicio.EstaRelacionado(provincia))
                {
                    respuesta = false;
                    mensaje = "Provincia relacionada... \nNo se pudo eliminar la provincia";
                    return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }
                servicio.Borrar(provincia.ProvinciaId);
                respuesta = true;
                mensaje = "Provincia eliminada!";
                return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                respuesta = false;
                mensaje = "Error al intentar eliminar la provincia";
                return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}