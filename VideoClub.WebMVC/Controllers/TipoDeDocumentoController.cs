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
using VideoClub.WebMVC.Models.Provincias;
using VideoClub.WebMVC.Models.Tipo_de_Documento;

namespace VideoClub.WebMVC.Controllers
{
    public class TipoDeDocumentoController : Controller
    {
        // GET: TipoDeDocumento
        private readonly IServicioTiposDeDocumentos servicio;
        private readonly IMapper mapper;
        public TipoDeDocumentoController(ServicioTiposDeDocumentos servicio)
        {
            this.servicio = servicio;
            mapper = AutoMapperConfig.Mapper;
        }

        [HttpGet]
        public JsonResult ListarTiposDeDocumentos()
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
       

    }
}