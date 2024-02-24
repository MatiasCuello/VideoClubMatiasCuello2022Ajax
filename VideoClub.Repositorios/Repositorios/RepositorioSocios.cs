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
    public class RepositorioSocios : IRepositorioSocios
    {
        private readonly VideoClubDbContext context;

        public RepositorioSocios(VideoClubDbContext context)
        {
            this.context = context;
        }
        public void Borrar(int id)
        {
            Socio socioInDb = null;
            try
            {
                socioInDb = context.Socios
                    .SingleOrDefault(s => s.SocioId == id);
                if (socioInDb == null)
                    return;
                context.Entry(socioInDb).State = EntityState.Deleted;
                //_context.SaveChanges();
            }
            catch (Exception e)
            {
                context.Entry(socioInDb).State = EntityState.Unchanged;
                throw new Exception(e.Message);
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
                        .Any(s => s.Nombre == socio.Nombre && s.Apellido != socio.Apellido);
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
                return context.Socios.SingleOrDefault(s => s.SocioId == id);
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
                    context.Provincias.Attach(socio.Provincia);
                }
                if (socio.Localidad != null)
                {
                    context.Localidades.Attach(socio.Localidad);
                }
                if (socio.TipoDeDocumento != null)
                {
                    context.TiposDeDocumentos.Attach(socio.TipoDeDocumento);
                }

                if (socio.SocioId == 0)
                {
                    context.Socios.Add(socio);
                }
                else
                {
                    var socioInDb = context.Socios.SingleOrDefault(s => s.SocioId == socio.SocioId);
                    if (socioInDb == null)
                    {
                        throw new Exception("Código de Socio inexistente");
                    }

                    socioInDb.SocioId = socio.SocioId;
                    socioInDb.Nombre = socio.Nombre;
                    socioInDb.Apellido = socio.Apellido;
                    socioInDb.TipoDeDocumentoId = socio.TipoDeDocumentoId;
                    socioInDb.NroDocumento = socio.NroDocumento;
                    socioInDb.Direccion = socio.Direccion;
                    socioInDb.LocalidadId = socio.LocalidadId;
                    socioInDb.ProvinciaId = socio.ProvinciaId;
                    socioInDb.TelefonoFijo = socio.TelefonoFijo;
                    socioInDb.TelefonoMovil = socio.TelefonoMovil;
                    socioInDb.CorreoElectronico = socio.CorreoElectronico;
                    socioInDb.FechaDeNacimiento = socio.FechaDeNacimiento;
                    socioInDb.Sancionado = socio.Sancionado;
                    socioInDb.Activo = socio.Activo;

                    context.Entry(socioInDb).State = EntityState.Modified;

                }
                

            }
            catch (Exception e)
            {
                throw new Exception("Error al intentar guardar");
            }
        }
    }
}
