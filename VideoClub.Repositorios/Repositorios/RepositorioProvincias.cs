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

            public void Borrar(int id)
            {
                Provincia provinciaInDb = null;
                try
                {
                    provinciaInDb = context.Provincias
                        .SingleOrDefault(p => p.ProvinciaId == id);
                    if (provinciaInDb == null)
                        return;
                    context.Entry(provinciaInDb).State = EntityState.Deleted;
                    //_context.SaveChanges();
                }
                catch (Exception e)
                {
                    context.Entry(provinciaInDb).State = EntityState.Unchanged;
                    throw new Exception(e.Message);
                }
            }

            public bool EstaRelacionado(Provincia provincia)
            {
                try
                {
                    return context.Localidades.Any(l => l.ProvinciaId == provincia.ProvinciaId);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            public bool Existe(Provincia provincia)
            {
                try
                {
                    if (provincia.ProvinciaId == 0)
                    {
                        return context.Provincias.Any(p => p.NombreProvincia == provincia.NombreProvincia);
                    }

                    return context.Provincias.Any(p => p.NombreProvincia == provincia.NombreProvincia
                                                       && p.ProvinciaId != provincia.ProvinciaId);
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

