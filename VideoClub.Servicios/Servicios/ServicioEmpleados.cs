using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;
using VideoClub.Repositorios;
using VideoClub.Repositorios.Repositorios;
using VideoClub.Servicios.Servicios.Facades;

namespace VideoClub.Servicios.Servicios
{
    public class ServicioEmpleados : IServicioEmpleados
    {
        private readonly RepositorioEmpleados repositorio;
        private readonly VideoClubDbContext context;
        private readonly UnitOfWork unitOfWork;

        public ServicioEmpleados(RepositorioEmpleados repositorio, VideoClubDbContext context, UnitOfWork unitOfWork)
        {
            this.repositorio = repositorio;
            this.context = context;
            this.unitOfWork = unitOfWork;
        }
        public void Borrar(Empleado empleadoId)
        {
            try
            {
                repositorio.Borrar(empleadoId);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Empleado empleado)
        {
            try
            {
                return repositorio.EstaRelacionado(empleado);
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
                return repositorio.Existe(empleado);
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
                return repositorio.GetEmpleadoPorId(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<Empleado> GetLista()
        {
            try
            {
                return repositorio.GetLista();
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
                repositorio.Guardar(empleado);
                unitOfWork.Save();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
