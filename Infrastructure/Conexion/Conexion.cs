using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Infrastructure.Conexion
{
    public class Conexion
    { 
        public static string CN = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
    }
}
