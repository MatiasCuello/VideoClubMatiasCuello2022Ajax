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
    public class RepositorioGeneros : IRepositorioGeneros
    {
        private VideoClubDbContext context;

        public RepositorioGeneros(VideoClubDbContext context)
        {
            this.context = context;
        }

        public void Borrar(Genero genero)
        {
            try
            {
                context.Entry(genero).State = EntityState.Deleted;
                //context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Genero genero)
        {
            return false;
        }

        public bool Existe(Genero genero)
        {
            try
            {
                if (genero.GeneroId == 0)
                {
                    return context.Generos.Any(g => g.Descripcion == genero.Descripcion);
                }

                return context.Generos.Any(g => g.Descripcion == genero.Descripcion
                                                   && g.GeneroId != genero.GeneroId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Genero GetGeneroPorId(int id)
        {

            try
            {
                return context.Generos.SingleOrDefault(g => g.GeneroId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Genero> GetLista()
        {
            try
            {
                return context.Generos.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Guardar(Genero genero)
        {
            try
            {
                if (genero.GeneroId == 0)
                {
                    context.Generos.Add(genero);
                }
                else
                {
                    var generoInDb = context.Generos
                        .SingleOrDefault(g => g.GeneroId == genero.GeneroId);
                    generoInDb.GeneroId = genero.GeneroId;
                    generoInDb.Descripcion = genero.Descripcion;
                    context.Entry(generoInDb).State = EntityState.Modified;
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
