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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Provincia provincia = servicio.GetProvinciaPorId(id.Value);
            if (provincia == null)
            {
                return HttpNotFound("El codigo de la Provincia no existe!");
            }

            ProvinciaEditVm provinciaEditVm = mapper.Map<ProvinciaEditVm>(provincia);
            return View(provinciaEditVm);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProvinciaEditVm provinciaEditVm)
        {
            if (!ModelState.IsValid)
            {
                return View(provinciaEditVm);
            }

            Provincia provincia = mapper.Map<Provincia>(provinciaEditVm);
            try
            {
                if (servicio.Existe(provincia))
                {
                    ModelState.AddModelError(string.Empty, "Provincia existente!");
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
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            Provincia provincia = servicio.GetProvinciaPorId(id.Value);
            if (provincia == null)
            {
                return HttpNotFound("El codigo de la provincia no existe!");
            }

            ProvinciaEditVm provinciaEditVm = mapper.Map<ProvinciaEditVm>(provincia);
            return View(provinciaEditVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            Provincia provincia = servicio.GetProvinciaPorId(id);
            try
            {
                if (servicio.EstaRelacionado(provincia))
                {
                    ProvinciaEditVm provinciaEditVm = mapper.Map<ProvinciaEditVm>(provincia);
                    ModelState.AddModelError(string.Empty, "Provincia relacionada!");
                    return View(provinciaEditVm);
                }

                servicio.Borrar(provincia);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ProvinciaEditVm provinciaEditVm = mapper.Map<ProvinciaEditVm>(provincia);
                ModelState.AddModelError(string.Empty, e.Message);
                return View(provinciaEditVm);
            }
        }

    }
}