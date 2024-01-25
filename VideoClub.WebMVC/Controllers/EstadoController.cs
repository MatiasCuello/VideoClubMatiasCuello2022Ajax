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
using VideoClub.WebMVC.Models.Calificacion;
using VideoClub.WebMVC.Models.Estado;

namespace VideoClub.WebMVC.Controllers
{
    public class EstadoController : Controller
    {
        // GET: Estado

        private readonly IServicioEstados servicio;
        private readonly IMapper mapper;
        public EstadoController(ServicioEstados servicio)
        {
            this.servicio = servicio;
            mapper = AutoMapperConfig.Mapper;
        }

        [HttpGet]
        public JsonResult ListarEstados()
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
        public ActionResult Create(EstadoEditVm estadoEditVm)
        {
            if (!ModelState.IsValid)
            {
                return View(estadoEditVm);
            }
            try
            {
                Estado estado = mapper.Map<Estado>(estadoEditVm);
                if (servicio.Existe(estado))
                {
                    ModelState.AddModelError(string.Empty, "Estado existente!!!");
                    return View(estadoEditVm);
                }
                servicio.Guardar(estado);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(estadoEditVm);
            }
        }
    

    }
}