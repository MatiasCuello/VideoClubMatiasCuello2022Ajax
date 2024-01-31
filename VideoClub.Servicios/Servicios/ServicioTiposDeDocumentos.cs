using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;
using VideoClub.Repositorios.Repositorios;
using VideoClub.Repositorios.Repositorios.Facades;
using VideoClub.Servicios.Servicios.Facades;

namespace VideoClub.Servicios.Servicios
{
    public class ServicioTiposDeDocumentos:IServicioTiposDeDocumentos
    {
        private readonly IRepositorioTiposDeDocumentos repositorio;

        public ServicioTiposDeDocumentos()
        {
            repositorio = new RepositorioTiposDeDocumentos();
        }
        public void Guardar(TipoDeDocumento tipoDeDocumento)
        {
            try
            {
                repositorio.Guardar(tipoDeDocumento);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<TipoDeDocumento> GetLista()
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

        public void Borrar(int tipoDeDocumentoId)
        {
            try
            {
                repositorio.Borrar(tipoDeDocumentoId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public TipoDeDocumento GetTipoPorId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Existe(TipoDeDocumento tipoDeDocumento)
        {
            try
            {
                return repositorio.Existe(tipoDeDocumento);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(TipoDeDocumento tipoDeDocumento)
        {
            try
            {
                return repositorio.EstaRelacionado(tipoDeDocumento);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
