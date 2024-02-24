using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;
using VideoClub.Repositorios;
using VideoClub.Repositorios.Repositorios.Facades;
using VideoClub.Servicios.Servicios.Facades;

namespace VideoClub.Servicios.Servicios
{
    public class ServicioLocalidades : IServicioLocalidades
    {
        private readonly IRepositorioLocalidades repositorio;
        private readonly VideoClubDbContext context;
        private readonly UnitOfWork unitOfWork;

        public ServicioLocalidades(IRepositorioLocalidades repositorio, VideoClubDbContext context, UnitOfWork unitOfWork)
        {
            this.repositorio = repositorio;
            this.context = context;
            this.unitOfWork = unitOfWork;

        }
        public List<Localidad> GetLista2()
        {
            try
            {
                return repositorio.GetLista2();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Guardar(Localidad localidad)
        {
            try
            {
                repositorio.Guardar(localidad);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Borrar(int id)
        {
            try
            {
                repositorio.Borrar(id);
                unitOfWork.Save();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Localidad localidad)
        {
            return false;
        }

        public bool Existe(Localidad localidad)
        {
            try
            {
                return repositorio.Existe(localidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Localidad GetLocalidadPorId(int id)
        {
            try
            {
                return repositorio.GetLocalidadPorId(id);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<Localidad> GetLista(int provinciaId)
        {
            try
            {
                return repositorio.GetLista(provinciaId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
