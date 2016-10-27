using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario
{
    class configuracion
    {
        static string cadenaconexionIcontruye = @"Data Source=SQL01\SQLAPPS;Initial Catalog=iConstruyeR;USER ID=sa; PASSWORD=mincivil123";
        static string cadenaconexionInventario = @"Data Source=SQL01\SQLAPPS;Initial Catalog=InventarioPrueba;USER ID=sa; PASSWORD=mincivil123";

        public static string cadenaconIconstruye
        {
            get
            {
                return cadenaconexionIcontruye;
            }
        }
        public static string cadenaconInventario
        {
            get
            {
                return cadenaconexionInventario;
            }
        }
    }
}
