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
    public class RepositorioLocalidades:IRepositorioLocalidades
    {
        private VideoClubDbContext context;

        public RepositorioLocalidades(VideoClubDbContext context)
        {
            this.context = context;
        }


        public void Borrar(Localidad localidad)
        {
            try
            {

                var localidadInDb = context.Localidades.SingleOrDefault(l => l.LocalidadId == localidad.LocalidadId);

                context.Entry(localidadInDb).State = EntityState.Deleted;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Localidad localidad)
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

        public bool Existe(Localidad localidad)
        {
            try
            {
                if (localidad.LocalidadId == 0)
                {
                    return context.Localidades
                        .Any(l => l.NombreLocalidad == localidad.NombreLocalidad);
                }

                return context.Localidades.Any(l => l.NombreLocalidad == localidad.NombreLocalidad && l.LocalidadId == localidad.LocalidadId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Localidad> GetLista()
        {
            try
            {
                return context.Localidades
                    .Include(l => l.Provincia)
                    .ToList();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Guardar(Localidad localidad)
        {
            try
            {
                if (localidad.Provincia != null)
                {
                    localidad.Provincia = null;
                }

                if (localidad.LocalidadId == 0)
                {
                    context.Localidades.Add(localidad);
                }

                else
                {
                    var localidadInDb = context.Localidades.SingleOrDefault(l => l.LocalidadId == localidad.LocalidadId);
                    localidadInDb.LocalidadId = localidad.LocalidadId;
                    localidadInDb.NombreLocalidad = localidad.NombreLocalidad;
                    localidadInDb.ProvinciaId = localidad.ProvinciaId;

                    context.Entry(localidadInDb).State = EntityState.Modified;
                }

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
                return context.Localidades
                    .Include(l => l.Provincia)
                    .SingleOrDefault(l => l.LocalidadId == id);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
