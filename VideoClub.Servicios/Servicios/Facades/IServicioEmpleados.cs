using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;

namespace VideoClub.Servicios.Servicios.Facades
{
    public interface IServicioEmpleados
    {
        void Guardar(Empleado empleado);
        List<Empleado> GetLista();
        void Borrar(Empleado empleadoId);
        Empleado GetEmpleadoPorId(int id);
        bool Existe(Empleado empleado);
        bool EstaRelacionado(Empleado empleado);
    }
}
