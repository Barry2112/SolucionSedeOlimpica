using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Modelo
{
    public class T_Evento
    {
        public int ID_Evento { get; set; }
        public int ID_Complejo { get; set; }
        public string Nombre_Evento { get; set; }
        public string Descripcion_Evento { get; set; }
        public int Duracion { get; set; }
        public int NroParticipantes { get; set; }
        public DateTime Fecha { get; set; }
    }
}