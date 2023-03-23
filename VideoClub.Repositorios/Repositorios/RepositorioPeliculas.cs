using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;

namespace VideoClub.Repositorios.Repositorios.Facades
{
    public class RepositorioPeliculas:IRepositorioPeliculas
    {
        private VideoClubDbContext context;

        public RepositorioPeliculas(VideoClubDbContext context)
        {
            this.context = context;
        }
   

        public void Borrar(Pelicula pelicula)
        {
            try
            {

                var peliculaInDb = context.Peliculas.SingleOrDefault(p => p.PeliculaId == pelicula.PeliculaId);

                context.Entry(peliculaInDb).State = EntityState.Deleted;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Pelicula pelicula)
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

        public bool Existe(Pelicula pelicula)
        {
            try
            {
                if (pelicula.PeliculaId == 0)
                {
                    return context.Peliculas
                        .Any(p => p.Titulo == pelicula.Titulo);
                }

                return context.Peliculas.Any(p => p.Titulo == pelicula.Titulo && p.PeliculaId == pelicula.PeliculaId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Pelicula> GetLista()
        {
            try
            {
                return context.Peliculas
                    .Include(p => p.Genero)
                    .Include(p => p.Estado)
                    .Include(p => p.Calificacion)
                    .Include(p => p.Soporte)
                    .ToList();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Pelicula GetPeliculaPorId(int id)
        {
            try
            {
                return context.Peliculas
                    .Include(p => p.Genero)
                    .Include(p => p.Estado)
                    .Include(p => p.Calificacion)
                    .Include(p => p.Soporte)
                    .SingleOrDefault(p => p.PeliculaId == id);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void Guardar(Pelicula pelicula)
        {
            try
            {
                if (pelicula.Genero != null)
                {
                    pelicula.Genero = null;
                }
                if (pelicula.Estado != null)
                {
                    pelicula.Estado = null;
                }
                if (pelicula.Calificacion != null)
                {
                    pelicula.Calificacion = null;
                }
                if (pelicula.Soporte != null)
                {
                    pelicula.Soporte = null;
                }

                if (pelicula.PeliculaId == 0)
                {
                    context.Peliculas.Add(pelicula);
                }

                else
                {
                    var peliculaInDb = context.Peliculas.SingleOrDefault(p => p.PeliculaId == pelicula.PeliculaId);
                    peliculaInDb.PeliculaId = pelicula.PeliculaId;
                    peliculaInDb.Titulo = pelicula.Titulo;
                    peliculaInDb.GeneroId = pelicula.GeneroId;
                    peliculaInDb.FechaIncorporacion = pelicula.FechaIncorporacion;
                    peliculaInDb.EstadoId = pelicula.EstadoId;
                    peliculaInDb.DuracionEnMinutos = pelicula.DuracionEnMinutos;
                    peliculaInDb.CalificacionId = pelicula.CalificacionId;
                    peliculaInDb.SoporteId = pelicula.SoporteId;
                    peliculaInDb.Alquilado = pelicula.Alquilado;
                    peliculaInDb.Activa = pelicula.Activa;

                    context.Entry(peliculaInDb).State = EntityState.Modified;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
    }
}
