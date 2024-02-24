using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;

namespace VideoClub.Repositorios.Repositorios.Facades
{
    public interface IRepositorioSocios
    {
        void Guardar(Socio socio);
        List<Socio> GetLista();
        void Borrar(int id);
        Socio GetSocioPorId(int id);
        bool Existe(Socio socio);
        bool EstaRelacionado(Socio socio);
    }
}
