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
    public class RepositorioSocios : IReposiorioSocios
    {
        private readonly VideoClubDbContext context;

        public RepositorioSocios()
        {
            context = new VideoClubDbContext();
        }
        public void Borrar(Socio socioId)
        {
            try
            {
                var socioInDb = context.Socios.SingleOrDefault(s => s.SocioId == socioId.SocioId);

                context.Entry(socioInDb).State = EntityState.Deleted;
                context.SaveChanges();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public bool EstaRelacionado(Socio socio)
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

        public bool Existe(Socio socio)
        {
            try
            {
                if (socio.SocioId == 0)
                {
                    return context.Socios
                        .Any(s => s.SocioId == socio.SocioId);
                }
                return context.Socios.Any(s => s.Nombre == socio.Nombre &&
                                               s.SocioId != socio.SocioId &&
                                                 s.Apellido != socio.Apellido);
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
                return context.Socios
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

        public Socio GetSocioPorId(int id)
        {
            try
            {
                return context.Socios
                    .SingleOrDefault(s => s.SocioId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Guardar(Socio socio)
        {
            try
            {

                if (socio.Provincia != null)
                {
                    socio.Provincia = null;
                }
                if (socio.Localidad != null)
                {
                    socio.Localidad = null;
                }
                if (socio.TipoDeDocumento != null)
                {
                    socio.TipoDeDocumento = null;
                }

                if (socio.SocioId == 0)
                {
                    context.Socios.Add(socio);
                }
                else
                {
                    var sociosInDb = context.Socios.SingleOrDefault(s => s.SocioId == socio.SocioId);
                    if (sociosInDb == null)
                    {
                        throw new Exception("Código de Socio inexistente");
                    }

                    sociosInDb.Nombre = socio.Nombre;
                    sociosInDb.Apellido = socio.Apellido;
                    sociosInDb.TipoDeDocumentoId = socio.TipoDeDocumentoId;
                    sociosInDb.NroDocumento = socio.NroDocumento;
                    sociosInDb.Direccion = socio.Direccion;
                    sociosInDb.LocalidadId = socio.LocalidadId;
                    sociosInDb.ProvinciaId = socio.ProvinciaId;
                    sociosInDb.TelefonoFijo = socio.TelefonoFijo;
                    sociosInDb.TelefonoMovil = socio.TelefonoMovil;
                    sociosInDb.CorreoElectronico = socio.CorreoElectronico;
                    sociosInDb.FechaDeNacimiento = socio.FechaDeNacimiento;
                    sociosInDb.Sancionado = socio.Sancionado;
                    sociosInDb.Activo = socio.Activo;

                    context.Entry(sociosInDb).State = EntityState.Modified;

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
