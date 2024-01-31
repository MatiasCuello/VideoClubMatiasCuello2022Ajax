using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;

namespace VideoClub.Repositorios.Repositorios.Facades
{
    public interface IReposiorioSocios
    {
        void Guardar(Socio socio);
        List<Socio> GetLista();
        void Borrar(Socio socioId);
        Socio GetSocioPorId(int id);
        bool Existe(Socio socio);
        bool EstaRelacionado(Socio socio);
    }
}
