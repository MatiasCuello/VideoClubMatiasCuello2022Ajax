using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;
using VideoClub.Repositorios.Repositorios.Facades;

namespace VideoClub.Repositorios.Repositorios
{
    public class RepositorioEmpleados : IRepositorioEmpleados
    {
        private readonly VideoClubDbContext context;

        public RepositorioEmpleados(VideoClubDbContext context)
        {
            this.context = context;
        }
        public void Borrar(Empleado empleadoId)
        {
            try
            {
                var empleadoInDb = context.Empleados.SingleOrDefault(e => e.EmpleadoId == empleadoId.EmpleadoId);

                context.Entry(empleadoInDb).State = EntityState.Deleted;

            }
            catch (Exception e)
            {

                throw;
            }
        }

        public bool EstaRelacionado(Empleado empleado)
        {
            try
            {
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Existe(Empleado empleado)
        {
            try
            {
                if (empleado.EmpleadoId== 0)
                {
                    return context.Empleados
                        .Any(e => e.Nombre == empleado.Nombre && e.Apellido != empleado.Apellido);
                }
                return context.Empleados.Any(e => e.Nombre == empleado.Nombre &&
                                               e.EmpleadoId != empleado.EmpleadoId &&
                                               e.Apellido != empleado.Apellido);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Empleado GetEmpleadoPorId(int id)
        {
            try
            {
                return context.Empleados
                    .Include(e => e.TipoDeDocumento)
                    .Include(e => e.Localidad)
                    .Include(e => e.Provincia)
                    .SingleOrDefault(e => e.EmpleadoId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Empleado> GetLista()
        {
            try
            {
                return context.Empleados
                    .Include(p => p.TipoDeDocumento)
                    .Include(p => p.Localidad)
                    .Include(p => p.Provincia)
                    .ToList();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Guardar(Empleado empleado)
        {
            try
            {

                if (empleado.Provincia != null)
                {
                    empleado.Provincia = null;
                }
                if (empleado.Localidad != null)
                {
                    empleado.Localidad = null;
                }
                if (empleado.TipoDeDocumento != null)
                {
                    empleado.TipoDeDocumento = null;
                }

                if (empleado.EmpleadoId == 0)
                {
                    context.Empleados.Add(empleado);
                }
                else
                {
                    var empleadoInDb = context.Empleados.SingleOrDefault(e => e.EmpleadoId == empleado.EmpleadoId);
                    if (empleadoInDb == null)
                    {
                        throw new Exception("Código de empleado inexistente");
                    }

                    empleadoInDb.EmpleadoId = empleado.EmpleadoId;
                    empleadoInDb.Nombre = empleado.Nombre;
                    empleadoInDb.Apellido = empleado.Apellido;
                    empleadoInDb.TipoDeDocumentoId = empleado.TipoDeDocumentoId;
                    empleadoInDb.NroDocumento = empleado.NroDocumento;
                    empleadoInDb.Direccion = empleado.Direccion;
                    empleadoInDb.LocalidadId = empleado.LocalidadId;
                    empleadoInDb.ProvinciaId = empleado.ProvinciaId;
                    empleadoInDb.TelefonoFijo = empleado.TelefonoFijo;
                    empleadoInDb.TelefonoMovil = empleado.TelefonoMovil;
                    empleadoInDb.CorreoElectronico = empleado.CorreoElectronico;

                    context.Entry(empleadoInDb).State = EntityState.Modified;

                }


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
