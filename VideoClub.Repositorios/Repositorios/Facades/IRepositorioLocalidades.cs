using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;

namespace VideoClub.Repositorios.Repositorios.Facades
{
    public interface IRepositorioLocalidades
    {
        List<Localidad> GetLista(int provinciaId);

        List<Localidad> GetLista2();
        void Guardar(Localidad localidad);
        bool Existe(Localidad localidad);
        void Borrar(int id);
        bool EstaRelacionado(Localidad localidad);
        Localidad GetLocalidadPorId(int id);
    }
}
