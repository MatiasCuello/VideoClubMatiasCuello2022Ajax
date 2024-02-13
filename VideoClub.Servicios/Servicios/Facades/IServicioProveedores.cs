using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;

namespace VideoClub.Servicios.Servicios.Facades
{
    public interface IServicioProveedores
    {
        void Guardar(Proveedor proveedor);
        List<Proveedor> GetLista();
        void Borrar(Proveedor proveedorId);
        Proveedor GetProveedorPorId(int id);
        bool Existe(Proveedor proveedor);
        bool EstaRelacionado(Proveedor proveedor);
    }
}
