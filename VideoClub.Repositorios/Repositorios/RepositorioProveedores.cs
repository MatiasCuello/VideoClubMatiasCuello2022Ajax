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
    public class RepositorioProveedores : IRepositorioProveedores
    {
        private readonly VideoClubDbContext context;

        public RepositorioProveedores(VideoClubDbContext context)
        {
            this.context = context;
        }
        public void Borrar(int id)
        {
            Proveedor proveedorInDb = null;
            try
            {
                proveedorInDb = context.Proveedores
                    .SingleOrDefault(p => p.ProveedorId == id);
                if (proveedorInDb == null)
                    return;
                context.Entry(proveedorInDb).State = EntityState.Deleted;
                //_context.SaveChanges();
            }
            catch (Exception e)
            {
                context.Entry(proveedorInDb).State = EntityState.Unchanged;
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Proveedor proveedor)
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

        public bool Existe(Proveedor proveedor)
        {
            try
            {
                if (proveedor.ProveedorId == 0)
                {
                    return context.Proveedores
                        .Any(p => p.CUIT == proveedor.CUIT && p.RazonSocial != proveedor.RazonSocial);
                }
                return context.Proveedores.Any(p => p.CUIT == proveedor.CUIT &&
                                                  p.ProveedorId != proveedor.ProveedorId &&
                                                  p.RazonSocial != proveedor.RazonSocial);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Proveedor> GetLista()
        {
            try
            {
                return context.Proveedores
                    .Include(p => p.Localidad)
                    .Include(p => p.Provincia)
                    .ToList();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Proveedor GetProveedorPorId(int id)
        {
            try
            {
                return context.Proveedores
                    .Include(p => p.Localidad)
                    .Include(p => p.Provincia)
                    .SingleOrDefault(p => p.ProveedorId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Guardar(Proveedor proveedor)
        {
            try
            {

                if (proveedor.Provincia != null)
                {
                    proveedor.Provincia = null;
                }
                if (proveedor.Localidad != null)
                {
                    proveedor.Localidad = null;
                }

                if (proveedor.ProveedorId == 0)
                {
                    context.Proveedores.Add(proveedor);
                }
                else
                {
                    var proveedorInDb = context.Proveedores.SingleOrDefault(p => p.ProveedorId == proveedor.ProveedorId);
                    if (proveedorInDb == null)
                    {
                        throw new Exception("Código de proveedor inexistente");
                    }

                    proveedorInDb.ProveedorId = proveedor.ProveedorId;
                    proveedorInDb.CUIT = proveedor.CUIT;
                    proveedorInDb.RazonSocial = proveedor.RazonSocial;
                    proveedorInDb.PersonaDeContacto = proveedor.PersonaDeContacto;
                    proveedorInDb.Direccion = proveedor.Direccion;
                    proveedorInDb.LocalidadId = proveedor.LocalidadId;
                    proveedorInDb.ProvinciaId = proveedor.ProvinciaId;
                    proveedorInDb.TelefonoFijo = proveedor.TelefonoFijo;
                    proveedorInDb.TelefonoMovil = proveedor.TelefonoMovil;
                    proveedorInDb.CorreoElectronico = proveedor.CorreoElectronico;

                    context.Entry(proveedorInDb).State = EntityState.Modified;

                }


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
