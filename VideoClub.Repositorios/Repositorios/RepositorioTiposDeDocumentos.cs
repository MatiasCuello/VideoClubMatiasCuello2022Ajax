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
    public class RepositorioTiposDeDocumentos : IRepositorioTiposDeDocumentos
    {
        private readonly VideoClubDbContext context;
        public RepositorioTiposDeDocumentos()
        {
            context = new VideoClubDbContext();
        }   
        public void Borrar(int tipoDeDocumentoId)
        {
            try
            {
                var tdInDb = context.TiposDeDocumentos.SingleOrDefault(t => t
                    .TipoDeDocumentoId == tipoDeDocumentoId);
                if (tdInDb == null)
                {
                    throw new Exception("Tipo de Documento no encontrado");
                }

                context.Entry(tdInDb).State = EntityState.Deleted;
                context.SaveChanges();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(TipoDeDocumento tipoDeDocumento)
        {
            throw new NotImplementedException();
            //try
            //{
            //    if (context.Clientes.Any(p => p.TipoDeDocumentoId == tipoDeDocumento.TipoDeDocumentoId))
            //    {
            //        return context.Clientes.Any();

            //    }
            //    if (context.Proveedores.Any(p => p.TipoDeDocumentoId == tipoDeDocumento.TipoDeDocumentoId))
            //    {
            //        return context.Proveedores.Any();
            //    }

            //    return false;

            //}
            //catch (Exception e)
            //{

            //    throw new Exception(e.Message);
            //}
        }

        public bool Existe(TipoDeDocumento tipoDeDocumento)
        {
            try
            {
                if (tipoDeDocumento.TipoDeDocumentoId == 0)
                {
                    return context.TiposDeDocumentos
                        .Any(tp => tp.Descripcion == tipoDeDocumento.Descripcion);
                }
                return context.TiposDeDocumentos.Any(tp => tp.Descripcion == tipoDeDocumento.Descripcion &&
                                                          tp.TipoDeDocumentoId != tipoDeDocumento.TipoDeDocumentoId);
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
                return context.TiposDeDocumentos
                    .AsNoTracking()
                    .ToList();
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

        public void Guardar(TipoDeDocumento tipoDeDocumento)
        {

            try
            {

                if (tipoDeDocumento.TipoDeDocumentoId == 0)
                {
                    context.TiposDeDocumentos.Add(tipoDeDocumento);
                }
                else
                {
                    var tipoDeDocumentoInDb = context.TiposDeDocumentos.SingleOrDefault(p => p.TipoDeDocumentoId == tipoDeDocumento.TipoDeDocumentoId);
                    if (tipoDeDocumentoInDb == null)
                    {
                        throw new Exception("Código de Tipo Documento inexistente");
                    }

                    tipoDeDocumentoInDb.Descripcion = tipoDeDocumento.Descripcion;


                    context.Entry(tipoDeDocumentoInDb).State = EntityState.Modified;

                }

                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
