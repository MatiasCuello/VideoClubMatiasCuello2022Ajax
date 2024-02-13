using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using VideoClub.Servicios.Servicios.Facades;
using VideoClub.WebMVC.App_Start;
using VideoClub.WebMVC.Models.Empleado;
using VideoClub.WebMVC.Models.Socio;

namespace VideoClub.WebMVC.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IServicioEmpleados servicio;
        private readonly IServicioTiposDeDocumentos servicioTipoDoc;
        private readonly IServicioLocalidades servicioLocalidades;
        private readonly IServicioProvincias servicioProvincias;
        private readonly IMapper mapper;

        public EmpleadoController(IServicioEmpleados servicio,
            IServicioTiposDeDocumentos servicioTipoDoc, IServicioLocalidades servicioLocalidades, IServicioProvincias servicioProvincias)
        {
            this.servicio = servicio;
            this.servicioTipoDoc = servicioTipoDoc;
            this.servicioLocalidades = servicioLocalidades;
            this.servicioProvincias = servicioProvincias;
            mapper = AutoMapperConfig.Mapper;
        }

        // GET: Empleado

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ListarEmpleados()
        {
            var lista = servicio.GetLista();
            var listaVm = mapper.Map<List<EmpleadoListVm>>(lista);
            return Json(new { data = listaVm }, JsonRequestBehavior.AllowGet);
        }
    }
}