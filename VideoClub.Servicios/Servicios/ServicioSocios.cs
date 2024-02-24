using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;
using VideoClub.Repositorios;
using VideoClub.Repositorios.Repositorios;
using VideoClub.Repositorios.Repositorios.Facades;
using VideoClub.Servicios.Servicios.Facades;

namespace VideoClub.Servicios.Servicios
{
    public class ServicioSocios:IServicioSocios
    {
        private readonly RepositorioSocios repositorio;
        private readonly VideoClubDbContext context;
        private readonly UnitOfWork unitOfWork;

        public ServicioSocios(RepositorioSocios repositorio, VideoClubDbContext context, UnitOfWork unitOfWork)
        {
            this.repositorio = repositorio;
            this.context = context;
            this.unitOfWork = unitOfWork;
        }

        public void Guardar(Socio socio)
        {
            try
            {
                repositorio.Guardar(socio);
                unitOfWork.Save();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<Socio> GetLista()
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

        public Socio GetSocioPorId(int id)
        {
            try
            {
                return repositorio.GetSocioPorId(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool Existe(Socio socio)
        {
            try
            {
                return repositorio.Existe(socio);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Socio socio)
        {
            try
            {
                return repositorio.EstaRelacionado(socio);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
