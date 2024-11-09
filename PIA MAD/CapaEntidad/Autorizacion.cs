using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Autorizacion
    {
        public int IdAutor { set; get; }
        public Puesto oPuesto { set; get; }
        public string NombreMenu { set; get; }
    }
}
