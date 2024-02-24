using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;

namespace VideoClub.Servicios.Servicios.Facades
{
    public interface IServicioSocios
    {
        void Guardar(Socio socio);
        List<Socio> GetLista();
        void Borrar(int id);
        Socio GetSocioPorId(int id);
        bool Existe(Socio socio);
        bool EstaRelacionado(Socio socio);
    }
}
