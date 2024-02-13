using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;

namespace VideoClub.Repositorios.Repositorios.Facades
{
    public interface IRepositorioEmpleados
    {
        void Guardar(Empleado empleado);
        List<Empleado> GetLista();
        void Borrar(Empleado empleadoId);
        Empleado GetEmpleadoPorId(int id);
        bool Existe(Empleado empleado);
        bool EstaRelacionado(Empleado empleado);
    }
}
