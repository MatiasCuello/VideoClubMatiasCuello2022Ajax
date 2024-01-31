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
        private readonly IReposiorioSocios repositorio;
        private readonly VideoClubDbContext context;

        public ServicioSocios()
        {
            repositorio = new RepositorioSocios();
            context = new VideoClubDbContext();
        }

        public void Guardar(Socio socio)
        {
            try
            {
                repositorio.Guardar(socio);

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

        public void Borrar(Socio socioId)
        {
            try
            {
                repositorio.Borrar(socioId);
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
