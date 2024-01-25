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

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Estado estado = servicio.GetEstadoPorId(id.Value);
            if (estado == null)
            {
                return HttpNotFound("El codigo del estado no existe!");
            }

            EstadoEditVm estadoEditVm = mapper.Map<EstadoEditVm>(estado);
            return View(estadoEditVm);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EstadoEditVm estadoEditVm)
        {
            if (!ModelState.IsValid)
            {
                return View(estadoEditVm);
            }

            Estado estado = mapper.Map<Estado>(estadoEditVm);
            try
            {
                if (servicio.Existe(estado))
                {
                    ModelState.AddModelError(string.Empty, "Estado existente!");
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
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            Estado estado = servicio.GetEstadoPorId(id.Value);
            if (estado == null)
            {
                return HttpNotFound("El codigo del estado no existe!");
            }

            EstadoEditVm estadoEditVm = mapper.Map<EstadoEditVm>(estado);
            return View(estadoEditVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            Estado estado = servicio.GetEstadoPorId(id);
            try
            {
                if (servicio.EstaRelacionado(estado))
                {
                    EstadoEditVm estadoEditVm = mapper.Map<EstadoEditVm>(estado);
                    ModelState.AddModelError(string.Empty, "Estado relacionado!");
                    return View(estadoEditVm);
                }

                servicio.Borrar(estado);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                EstadoEditVm estadoEditVm = mapper.Map<EstadoEditVm>(estado);
                ModelState.AddModelError(string.Empty, e.Message);
                return View(estadoEditVm);
            }
        }
    }
}