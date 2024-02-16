using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion.Models
{
    public class T_Complejo
    {
        public int ID_Complejo { get; set; }
        public int ID_Tipo_Complejo { get; set; }
        public int ID_Sede_Olimpica { get; set; }
        public string Nombre_Complejo { get; set; }
        public string Localizacion { get; set; }
        public string Jefe_Organizacion { get; set; }
        public double Area_Total { get; set; }
        public double Presupuesto { get; set; } 
    }
}