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
                    return context.Localidades.Any(l => l.NombreLocalidad == localidad.NombreLocalidad &&
                                                      l.ProvinciaId == localidad.ProvinciaId);
                }

                return context.Localidades.Any(l => l.NombreLocalidad == localidad.NombreLocalidad
                                                  && l.ProvinciaId == localidad.ProvinciaId
                                                  && l.LocalidadId != localidad.LocalidadId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public void Guardar(Localidad localidad)
        {

            if (localidad.Provincia != null)
            {
                context.Provincias.Attach(localidad.Provincia);

            }

            if (localidad.LocalidadId == 0)
            {
                //Cuando el id=0 entonces la entidad es nueva ==>alta
                context.Localidades.Add(localidad);

            }
            else
            {
                var localidadInDb =
                    context.Localidades.SingleOrDefault(l => l.LocalidadId == localidad.LocalidadId);
                if (localidadInDb == null)
                {
                    throw new Exception("Localidad inexistente");
                }

                localidadInDb.NombreLocalidad = localidad.NombreLocalidad;
                localidadInDb.ProvinciaId = localidad.ProvinciaId;
                context.Entry(localidadInDb).State = EntityState.Modified;

            }
        }

        public Localidad GetLocalidadPorId(int id)
        {
            try
            {
                return context.Localidades.SingleOrDefault(l => l.LocalidadId == id);
            }
            catch (Exception e)
            {
                throw new Exception("Error");
            }
        }

        public List<Localidad> GetLista(int provinciaId)
        {
            try
            {
                return context.Localidades.Where(l => l.ProvinciaId == provinciaId)
                    .OrderBy(l =>l.NombreLocalidad)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar leer la tabla de Localidades");
            }
        }
        public List<Localidad> GetLista2()
        {
            try
            {
                return context.Localidades.Include(l => l.Provincia).OrderBy(l => l.Provincia.NombreProvincia)
                    .ThenBy(l => l.NombreLocalidad)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Error al intentar leer la tabla de Ciudades");
            }
        }
    }
}
