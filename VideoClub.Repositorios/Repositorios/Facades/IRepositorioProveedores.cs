using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;

namespace VideoClub.Repositorios.Repositorios.Facades
{
    public interface IRepositorioProveedores
    {
        void Guardar(Proveedor proveedor);
        List<Proveedor> GetLista();
        void Borrar(int id);
        Proveedor GetProveedorPorId(int id);
        bool Existe(Proveedor proveedor);
        bool EstaRelacionado(Proveedor proveedor);
    }
}
