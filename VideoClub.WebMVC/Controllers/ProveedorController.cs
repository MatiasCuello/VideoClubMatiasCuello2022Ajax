using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using VideoClub.Servicios.Servicios.Facades;
using VideoClub.WebMVC.App_Start;
using VideoClub.WebMVC.Models.Empleado;
using VideoClub.WebMVC.Models.Proveedor;

namespace VideoClub.WebMVC.Controllers
{
    public class ProveedorController : Controller
    {
        private readonly IServicioProveedores servicio;
        private readonly IServicioLocalidades servicioLocalidades;
        private readonly IServicioProvincias servicioProvincias;
        private readonly IMapper mapper;

        public ProveedorController(IServicioProveedores servicio, IServicioLocalidades servicioLocalidades, IServicioProvincias servicioProvincias)
        {
            this.servicio = servicio;
            this.servicioLocalidades = servicioLocalidades;
            this.servicioProvincias = servicioProvincias;
            mapper = AutoMapperConfig.Mapper;
        }
        // GET: Proveedor
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ListarProveedores()
        {
            var lista = servicio.GetLista();
            var listaVm = mapper.Map<List<ProveedorListVm>>(lista);
            return Json(new { data = listaVm }, JsonRequestBehavior.AllowGet);
        }
    }
}