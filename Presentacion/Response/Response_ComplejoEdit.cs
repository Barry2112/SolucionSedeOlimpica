using Presentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion.Response
{
    public class Response_ComplejoEdit
    {
        public int ID_Complejo { get; set; }
        public int ID_Deporte { get; set; }
        public string Nombre_Complejo { get; set; }
        public string Localizacion { get; set; }
        public string Jefe_Organizacion { get; set; }
        public double Presupuesto { get; set; }
        public string Nombre_Deporte { get; set; } 

        public List<T_Deporte> ListaComplejos { get; set; }
    }
}