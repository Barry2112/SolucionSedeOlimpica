using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion.Response
{
    public class Response_Complejo
    {
        public int ID_Complejo { get; set; }
        public string Nombre_Sede_Olimpica { get; set; }
        public string Descripcion_Tipo_Complejo { get; set; }
        public string Nombre_Deporte { get; set; }
        public string Nombre_Complejo { get; set; }
        public string Localizacion { get; set; }
        public string Jefe_Organizacion { get; set; }
        public double Area_Total { get; set; }
        public double Presupuesto { get; set; } 
    }
}