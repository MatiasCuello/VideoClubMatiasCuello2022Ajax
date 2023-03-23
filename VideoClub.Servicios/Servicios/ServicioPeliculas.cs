﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;
using VideoClub.Repositorios;
using VideoClub.Repositorios.Repositorios.Facades;
using VideoClub.Servicios.Servicios.Facades;

namespace VideoClub.Servicios.Servicios
{
    public class ServicioPeliculas : IServicioPeliculas
    {
        private readonly RepositorioPeliculas repositorio;
        private readonly VideoClubDbContext context;
        private readonly UnitOfWork unitOfWork;

        public ServicioPeliculas(RepositorioPeliculas repositorio,VideoClubDbContext context,UnitOfWork unitOfWork)
        {
            this.repositorio = repositorio;
            this.context = context;
            this.unitOfWork = unitOfWork;

        }

        public List<Pelicula> GetLista()
        {
            try
            {
                return repositorio.GetLista();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Guardar(Pelicula pelicula)
        {
            try
            {
                repositorio.Guardar(pelicula);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Borrar(Pelicula pelicula)
        {
            try
            {
                repositorio.Borrar(pelicula);
                unitOfWork.Save();
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Pelicula pelicula)
        {
            return false;
        }

        public bool Existe(Pelicula pelicula)
        {
            try
            {
                return repositorio.Existe(pelicula);
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
                return repositorio.GetPeliculaPorId(id);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
