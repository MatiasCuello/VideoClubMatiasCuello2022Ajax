using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using VideoClub.Entidades.Entidades;
using VideoClub.Servicios.Servicios.Facades;
using VideoClub.WebMVC.App_Start;
using VideoClub.WebMVC.Models.Empleado;
using VideoClub.WebMVC.Models.Localidad;
using VideoClub.WebMVC.Models.Proveedor;
using VideoClub.WebMVC.Models.Provincia;
using VideoClub.WebMVC.Models.Socio;

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
        [HttpGet]
        public JsonResult ListarProveedores()
        {
            var lista = servicio.GetLista();
            var listaVm = mapper.Map<List<ProveedorListVm>>(lista);
            return Json(new { data = listaVm }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarProveedor(int proveedorId)
        {
            var proveedorVm = mapper.Map<ProveedorEditVm>(servicio.GetProveedorPorId(proveedorId));
            return Json(proveedorVm, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarProvincias()
        {
            var listaProvinciasVm = mapper.Map<List<ProvinciaListVm>>(servicioProvincias.GetLista());
            return Json(new { data = listaProvinciasVm }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarLocalidades(int provinciaId)
        {
            var listaLocalidadesVm = mapper.Map<List<LocalidadListVm>>(servicioLocalidades.GetLista(provinciaId));
            return Json(new { data = listaLocalidadesVm }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarProveedor(string objeto)
        {
            bool respuesta = true;
            string mensaje = string.Empty;

            Proveedor proveedor = new Proveedor();
            proveedor = JsonConvert.DeserializeObject<Proveedor>(objeto);

            mensaje = ValidarProveedor(proveedor);
            if (mensaje != string.Empty)
            {
                respuesta = false;
                return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                if (servicio.Existe(proveedor))
                {
                    respuesta = false;
                    mensaje = "Proveedor existente!";
                    return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }
                servicio.Guardar(proveedor);
                respuesta = true;
                mensaje = "Registro guardado";
                return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                respuesta = false;
                mensaje = "Error al intentar guardar el registro";
                return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

            }
        }
        private string ValidarProveedor(Proveedor proveedor)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(proveedor.CUIT))
            {
                sb.AppendLine("CUIT del proveedor es requerido");
            }
            if (string.IsNullOrEmpty(proveedor.RazonSocial))
            {
                sb.AppendLine("Razon Social del proveedor es requerida");
            }
            if (string.IsNullOrEmpty(proveedor.PersonaDeContacto))
            {
                sb.AppendLine("La Persona de Contacto es requerida");
            }
            if (string.IsNullOrEmpty(proveedor.Direccion))
            {
                sb.AppendLine("La direccion es requerida");
            }
            if (proveedor.ProvinciaId == 0)
            {
                sb.AppendLine("Debe seleccionar una provincia");
            }
            if (proveedor.LocalidadId == 0)
            {
                sb.AppendLine("Debe seleccionar una localidad");
            }



            return sb.ToString();
        }
        [HttpPost]
        public JsonResult EliminarProveedor(int proveedorId)
        {
            var respuesta = true;
            var mensaje = string.Empty;
            try
            {
                var proveedor = servicio.GetProveedorPorId(proveedorId);
                if (servicio.EstaRelacionado(proveedor))
                {
                    respuesta = false;
                    mensaje = "Proveedor relacionado... \nNo se pudo eliminar el proveedor";
                    return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }
                servicio.Borrar(proveedor.ProveedorId);
                respuesta = true;
                mensaje = "Proveedor eliminado!";
                return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                respuesta = false;
                mensaje = "Error al intentar eliminar el proveedor";
                return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}