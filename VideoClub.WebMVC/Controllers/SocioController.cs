using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using VideoClub.Servicios.Servicios.Facades;
using VideoClub.WebMVC.App_Start;
using VideoClub.WebMVC.Models.Pelicula;
using VideoClub.WebMVC.Models.Socio;

namespace VideoClub.WebMVC.Controllers
{
    public class SocioController : Controller
    {
        private readonly IServicioSocios servicio;
        private readonly IServicioTiposDeDocumentos servicioTipoDoc;
        private readonly IServicioLocalidades servicioLocalidades;
        private readonly IServicioProvincias servicioProvincias;
        private readonly IMapper mapper;

        public SocioController(
            IServicioSocios servicio, IServicioTiposDeDocumentos servicioTipoDoc,
            IServicioLocalidades serIservicioLocalidades, IServicioProvincias servicioProvincias)
        {
            this.servicio = servicio;
            this.servicioTipoDoc = servicioTipoDoc;
            this.servicioLocalidades = serIservicioLocalidades;
            this.servicioProvincias = servicioProvincias;
            mapper = AutoMapperConfig.Mapper;
        }
        // GET: Socio
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ListarSocios()
        {
            var lista = servicio.GetLista();
            var listaVm = mapper.Map<List<SocioListVm>>(lista);
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
    }
}