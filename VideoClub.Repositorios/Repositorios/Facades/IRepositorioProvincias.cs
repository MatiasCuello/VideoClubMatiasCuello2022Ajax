using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;

namespace VideoClub.Repositorios.Repositorios.Facades
{
    public interface IRepositorioProvincias
    {

        List<Provincia> GetLista();
        Provincia GetProvinciaPorId(int id);
        void Guardar(Provincia provincia);
        bool Existe(Provincia provincia);
        bool EstaRelacionado(Provincia provincia);
        void Borrar(Provincia provincia);

    }
}
