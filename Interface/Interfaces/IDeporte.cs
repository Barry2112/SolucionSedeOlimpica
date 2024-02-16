using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentacion.Models;

namespace Interface.Interfaces
{
    public interface IDeporte
    {
        List<T_Deporte> ObtenerClientes();
        bool RegistrarDeporte(T_Deporte T_Deporte); 
        bool ModificarDeporte(T_Deporte T_Deporte);
        bool EliminarDeporte(T_Deporte T_Deporte); 
    }
}
