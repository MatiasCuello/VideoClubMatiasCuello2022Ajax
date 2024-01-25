using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using VideoClub.Servicios.Servicios;
using VideoClub.Servicios.Servicios.Facades;
using VideoClub.WebMVC.App_Start;

namespace VideoClub.WebMVC.Controllers
{
    public class ProvinciasController : Controller
    {
        // GET: Provincias
        private readonly IServicioProvincias servicio;
        private readonly IMapper mapper;

        public ProvinciasController(ServicioProvincias servicio)
        {
            this.servicio = servicio;
            mapper = AutoMapperConfig.Mapper;
        }

        [HttpGet]
        public JsonResult ListarProvincias()
        {
            var lista = servicio.GetLista();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            var lista = servicio.GetLista();
            return View(lista);
        }
    }
}