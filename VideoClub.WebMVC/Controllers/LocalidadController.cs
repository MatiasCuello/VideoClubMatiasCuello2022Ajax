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
            ;
            mapper = AutoMapperConfig.Mapper;
        }

        // GET: Localidades
        public ActionResult Index()
        {
            var listaCiudadesVm = mapper.Map<List<LocalidadListVm>>(servicio.GetLista2());
            listaCiudadesVm = listaCiudadesVm
                .OrderBy(c => c.NombreLocalidad)
                .ThenBy(c => c.Provincia)
                .ToList();
            return View(listaCiudadesVm);
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
                //TempData["msg"] = "Ciudad agregada satisfactoriamente!!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                //TempData["error"] = "Error al intentar agregar una ciudad!!";
                return RedirectToAction("Index");
            }
        }


    }
}