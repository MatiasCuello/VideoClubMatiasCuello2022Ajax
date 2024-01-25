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
    public class RepositorioProvincias:IRepositorioProvincias
    {

            private VideoClubDbContext context;

            public RepositorioProvincias(VideoClubDbContext context)
            {
                this.context = context;
            }

            public void Borrar(Provincia provincia)
            {
                try
                {
                    context.Entry(provincia).State = EntityState.Deleted;
                    //context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            public bool EstaRelacionado(Provincia provincia)
            {
                return false;
            }

            public bool Existe(Provincia provincia)
            {
                try
                {
                    if (provincia.ProvinciaId == 0)
                    {
                        return context.Provincias.Any(c => c.NombreProvincia == provincia.NombreProvincia);
                    }

                    return context.Provincias.Any(c => c.NombreProvincia == provincia.NombreProvincia
                                                       && c.ProvinciaId != provincia.ProvinciaId);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            public Provincia GetProvinciaPorId(int id)
            {
                try
                {
                    return context.Provincias.SingleOrDefault(c => c.ProvinciaId == id);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            public List<Provincia> GetLista()
            {
                try
                {
                    return context.Provincias.ToList();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            public void Guardar(Provincia Provincia)
            {
                try
                {
                    if (Provincia.ProvinciaId == 0)
                    {
                        context.Provincias.Add(Provincia);
                    }
                    else
                    {
                        var ProvinciaInDb = context.Provincias
                            .SingleOrDefault(c => c.ProvinciaId == Provincia.ProvinciaId);
                        ProvinciaInDb.ProvinciaId = Provincia.ProvinciaId;
                        ProvinciaInDb.NombreProvincia = Provincia.NombreProvincia;
                        context.Entry(ProvinciaInDb).State = EntityState.Modified;
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

