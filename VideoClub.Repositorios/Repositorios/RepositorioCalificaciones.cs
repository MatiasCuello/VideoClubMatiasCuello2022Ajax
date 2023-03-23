﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;
using VideoClub.Repositorios.Repositorios.Facades;

namespace VideoClub.Repositorios.Repositorios
{
    public class RepositorioCalificaciones : IRepositorioCalificaciones
    {
        private VideoClubDbContext context;

        public RepositorioCalificaciones(VideoClubDbContext context)
        {
            this.context = context;
        }

        public void Borrar(Calificacion calificacion)
        {
            try
            {
                context.Entry(calificacion).State = EntityState.Deleted;
                //context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Calificacion calificacion)
        {
            return false;
        }

        public bool Existe(Calificacion calificacion)
        {
            try
            {
                if (calificacion.CalificacionId == 0)
                {
                    return context.Calificaciones.Any(c => c.Descripcion == calificacion.Descripcion);
                }

                return context.Calificaciones.Any(c => c.Descripcion == calificacion.Descripcion
                                                   && c.CalificacionId != calificacion.CalificacionId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Calificacion GetCalificacionPorId(int id)
        {
            try
            {
                return context.Calificaciones.SingleOrDefault(c => c.CalificacionId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Calificacion> GetLista()
        {
            try
            {
                return context.Calificaciones.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Guardar(Calificacion calificacion)
        {
            try
            {
                if (calificacion.CalificacionId == 0)
                {
                    context.Calificaciones.Add(calificacion);
                }
                else
                {
                    var calificacionInDb = context.Calificaciones
                        .SingleOrDefault(c => c.CalificacionId == calificacion.CalificacionId);
                    calificacionInDb.CalificacionId = calificacion.CalificacionId;
                    calificacionInDb.Descripcion = calificacion.Descripcion;
                    context.Entry(calificacionInDb).State = EntityState.Modified;
                }

                //context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
