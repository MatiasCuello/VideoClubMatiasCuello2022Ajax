using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;

namespace VideoClub.Servicios.Servicios.Facades
{
    public interface IServicioLocalidades
    {
        List<Localidad> GetLista2();
        List<Localidad> GetLista(int provinciaId);
        void Guardar(Localidad localidad);
        bool Existe(Localidad localidad);
        void Borrar(Localidad localidad);
        bool EstaRelacionado(Localidad localidad);
        Localidad GetLocalidadPorId(int id);
    }
}
