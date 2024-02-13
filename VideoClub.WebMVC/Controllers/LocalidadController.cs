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
        public JsonResult ListarLocalidades()
        {
            var lista = servicio.GetLista2();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            var localidadEditVm = mapper.Map<List<LocalidadListVm>>(servicio.GetLista2());
            localidadEditVm = localidadEditVm
                .OrderBy(c => c.NombreLocalidad)
                .ThenBy(c => c.Provincia)
                .ToList();
            return View(localidadEditVm);
        }
        public ActionResult Create()
        {
            var listarProvincias = servicioProvincias.GetLista();
            var provinciasDropDown = listarProvincias.Select(p => new SelectListItem()
            {
                Text = p.NombreProvincia,
                Value = p.ProvinciaId.ToString()
            }).ToList();
            var localidadEditVm = new LocalidadEditVm()
            {
                Provincias = provinciasDropDown
        };
            return View(localidadEditVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocalidadEditVm localidadEditVm)
        {
            if (!ModelState.IsValid)
            {
                var listaProvincias = servicioProvincias.GetLista();
                var proviciasDropDown = listaProvincias.Select(p => new SelectListItem()
                {
                    Text = p.NombreProvincia,
                    Value = p.ProvinciaId.ToString()
                }).ToList();
                localidadEditVm.Provincias = proviciasDropDown;

                return View(localidadEditVm);
            }
            var localidad = mapper.Map<Localidad>(localidadEditVm);
            try
            {
                if (servicio.Existe(localidad))
                {
                    ModelState.AddModelError(string.Empty, "Localidad existente!!!");
                    var listarProvincias = servicioProvincias.GetLista();

                    var provinciasDropDown = listarProvincias.Select(p => new SelectListItem()
                    {
                        Text = p.NombreProvincia,
                        Value = p.ProvinciaId.ToString()
                    }).ToList();
                    localidadEditVm.Provincias = provinciasDropDown;

                    return View(localidadEditVm);
                }
                servicio.Guardar(localidad);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var localidad = servicio.GetLocalidadPorId(id.Value);
            if (localidad == null)
            {
                return HttpNotFound("Código de localidad erróneo!!!");
            }

            var localidadEditVm = mapper.Map<LocalidadEditVm>(localidad);
            var listaProvincias = servicioProvincias.GetLista();

            var provinciassDropDown = listaProvincias.Select(p => new SelectListItem()
            {
                Text = p.NombreProvincia,
                Value = p.ProvinciaId.ToString()
            }).ToList();
            localidadEditVm.Provincias = provinciassDropDown;
            return View(localidadEditVm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LocalidadEditVm localidadEditVm)
        {
            if (!ModelState.IsValid)
            {
                var listaProvincias = servicioProvincias.GetLista();

                var provinciasDropDown = listaProvincias.Select(p => new SelectListItem()
                {
                    Text = p.NombreProvincia,
                    Value = p.ProvinciaId.ToString()
                }).ToList();
                localidadEditVm.Provincias = provinciasDropDown;

                return View(localidadEditVm);
            }

            var localidad = mapper.Map<Localidad>(localidadEditVm);
            try
            {
                if (servicio.Existe(localidad))
                {
                    ModelState.AddModelError(string.Empty, "Localidad existente!");
                    var listaProvincias = servicioProvincias.GetLista();

                    var provinciasDropDown = listaProvincias.Select(p => new SelectListItem()
                    {
                        Text = p.NombreProvincia,
                        Value = p.ProvinciaId.ToString()
                    }).ToList();
                    localidadEditVm.Provincias = provinciasDropDown;

                    return View(localidadEditVm);
                }
                servicio.Guardar(localidad);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var localidad = servicio.GetLocalidadPorId(id.Value);
            if (localidad == null)
            {
                return HttpNotFound("Código de localidad erróneo!!!");
            }

            var localidadEditVm = mapper.Map<LocalidadListVm>(localidad);
            return View(localidadEditVm);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                var localidad = servicio.GetLocalidadPorId(id);
                if (servicio.EstaRelacionado(localidad))
                {
                    var localidadVm = mapper.Map<LocalidadListVm>(localidad);
                    ModelState.AddModelError(string.Empty, "Localidad con registros relacionados... Eliminacion denegada");
                    return View(localidadVm);
                }
                servicio.Borrar(localidad);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return RedirectToAction("Index");
            }
        }

    }
}