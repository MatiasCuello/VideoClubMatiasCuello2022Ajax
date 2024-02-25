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
using VideoClub.WebMVC.Models.Provincia;
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
        [HttpGet]
        public JsonResult ListarEmpleado(int empleadoId)
        {
            var empleadoVm = mapper.Map<EmpleadoEditVm>(servicio.GetEmpleadoPorId(empleadoId));
            return Json(empleadoVm, JsonRequestBehavior.AllowGet);
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
        public JsonResult GuardarEmpleado(string objeto)
        {
            bool respuesta = true;
            string mensaje = string.Empty;

            Empleado empleado = new Empleado();
            empleado = JsonConvert.DeserializeObject<Empleado>(objeto);

            mensaje = ValidarEmpleado(empleado);
            if (mensaje != string.Empty)
            {
                respuesta = false;
                return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                if (servicio.Existe(empleado))
                {
                    respuesta = false;
                    mensaje = "Empleado existente!";
                    return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }
                servicio.Guardar(empleado);
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
        private string ValidarEmpleado(Empleado empleado)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(empleado.Nombre))
            {
                sb.AppendLine("Nombre del empleado es requerido");
            }
            if (string.IsNullOrEmpty(empleado.Apellido))
            {
                sb.AppendLine("Apellido del empleado es requerido");
            }
            if (empleado.TipoDeDocumentoId == 0)
            {
                sb.AppendLine("Debe seleccionar una tipo de documento");
            }
            if ((empleado.NroDocumento == null))
            {
                sb.AppendLine("Numero de Documento del socio es requerido");
            }
            if (string.IsNullOrEmpty(empleado.Direccion))
            {
                sb.AppendLine("La direccion es requerida");
            }
            if (empleado.ProvinciaId == 0)
            {
                sb.AppendLine("Debe seleccionar una provincia");
            }
            if (empleado.LocalidadId == 0)
            {
                sb.AppendLine("Debe seleccionar una localidad");
            }


            return sb.ToString();
        }
        [HttpPost]
        public JsonResult EliminarEmpleado(int empleadoId)
        {
            var respuesta = true;
            var mensaje = string.Empty;
            try
            {
                var empleado = servicio.GetEmpleadoPorId(empleadoId);
                if (servicio.EstaRelacionado(empleado))
                {
                    respuesta = false;
                    mensaje = "Empleado relacionado... \nNo se pudo eliminar el empleado";
                    return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }
                servicio.Borrar(empleado.EmpleadoId);
                respuesta = true;
                mensaje = "Empleado eliminado!";
                return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                respuesta = false;
                mensaje = "Error al intentar eliminar el empleado";
                return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}