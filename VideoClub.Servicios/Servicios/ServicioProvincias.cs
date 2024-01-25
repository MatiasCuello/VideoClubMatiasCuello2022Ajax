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
    public class ServicioProvincias:IServicioProvincias
    {
        private readonly IRepositorioProvincias repositorio;
        private readonly VideoClubDbContext context;
        private readonly UnitOfWork unitOfWork;

        public ServicioProvincias(RepositorioProvincias repositorio, VideoClubDbContext context, UnitOfWork unitOfWork)
        {
            this.repositorio = repositorio;
            this.context = context;
            this.unitOfWork = unitOfWork;

        }

        public void Borrar(Provincia provincia)
        {
            try
            {
                repositorio.Borrar(provincia);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Provincia provincia)
        {
            try
            {
                return repositorio.EstaRelacionado(provincia);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Existe(Provincia provincia)
        {
            try
            {
                return repositorio.Existe(provincia);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Provincia GetProvinciaPorId(int id)
        {
            try
            {
                return repositorio.GetProvinciaPorId(id);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<Provincia> GetLista()
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

        public void Guardar(Provincia provincia)
        {
            try
            {
                repositorio.Guardar(provincia);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
