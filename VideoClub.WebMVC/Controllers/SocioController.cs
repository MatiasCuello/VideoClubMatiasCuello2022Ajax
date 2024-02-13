using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using VideoClub.Entidades.Entidades;
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

        public SocioController(IServicioSocios servicio, IServicioTiposDeDocumentos servicioTipoDoc, IServicioLocalidades serIservicioLocalidades, IServicioProvincias servicioProvincias)
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
            return Json(new { data = listaVm }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarSocio(string objeto)
        {
            object resultado = null;
            string mensaje = string.Empty;

            try
            {

                Socio socioRecibido = new Socio();
                socioRecibido = JsonConvert.DeserializeObject<Socio>(objeto);
                mensaje = ValidarSocio(socioRecibido);

                if (mensaje == String.Empty)
                {
                    if (!servicio.Existe(socioRecibido))
                    {
                        servicio.Guardar(socioRecibido);
                        resultado = socioRecibido.SocioId;
                        mensaje = "Socio agregado/editado";
                    }
                    else
                    {
                        resultado = 0;
                        mensaje = "Socio existente";
                    }
                }
                else
                {
                    resultado = 0;
                }
            }
            catch (Exception e)
            {
                resultado = 0;
                mensaje = e.Message;

            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        private string ValidarSocio(Socio socio)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(socio.Nombre))
            {
                sb.AppendLine("Nombre del socio es requerido");
            }
            if (string.IsNullOrEmpty(socio.Apellido))
            {
                sb.AppendLine("Apellido del socio es requerido");
            }

            if ((socio.NroDocumento == null))
            {
                sb.AppendLine("Numero de Documento del socio es requerido");
            }
            if (socio.ProvinciaId == 0)
            {
                sb.AppendLine("Debe seleccionar una provincia");
            }
            if (socio.LocalidadId == 0)
            {
                sb.AppendLine("Debe seleccionar una localidad");
            }
            if (socio.TipoDeDocumentoId == 0)
            {
                sb.AppendLine("Debe seleccionar una tipo de documento");
            }

            return sb.ToString();
        }
        [HttpPost]
        public JsonResult EliminarSocio(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            try
            {
                Socio socio = servicio.GetSocioPorId(id);
                if (!servicio.EstaRelacionado(socio))
                {
                    servicio.Borrar(socio);
                    respuesta = true;
                    mensaje = "Socio eliminado";
                }
                else
                {
                    respuesta = false;
                    mensaje = "Socio relacionado... Baja denegada";
                }
            }
            catch (Exception e)
            {
                respuesta = false;
                mensaje = e.Message;
            }

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult ObtenerLocalidadesPorProvincia(int provinciaId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("ObtenerLocalidadesPorProvincia", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProvinciaId", provinciaId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var localidades = new List<object>();
                        while (reader.Read())
                        {
                            localidades.Add(new { LocalidadId = reader.GetInt32(0), NombreLocalidad = reader.GetString(1) });
                        }

                        return Json(localidades, JsonRequestBehavior.AllowGet);
                    }
                }
            }
        }
    }
}