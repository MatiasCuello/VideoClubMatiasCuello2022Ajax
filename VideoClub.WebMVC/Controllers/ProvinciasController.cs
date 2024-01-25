using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using VideoClub.Entidades.Entidades;
using VideoClub.Servicios.Servicios;
using VideoClub.Servicios.Servicios.Facades;
using VideoClub.WebMVC.App_Start;
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
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProvinciaEditVm provinciaEditVm)
        {
            if (!ModelState.IsValid)
            {
                return View(provinciaEditVm);
            }
            try
            {
                Provincia provincia = mapper.Map<Provincia>(provinciaEditVm);
                if (servicio.Existe(provincia))
                {
                    ModelState.AddModelError(string.Empty, "Provincia existente!!!");
                    return View(provinciaEditVm);
                }
                servicio.Guardar(provincia);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(provinciaEditVm);
            }
        }
    }
}