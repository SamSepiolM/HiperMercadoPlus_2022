using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Departamento
    {
        public int Clave{ set; get; }
        public string Nombre{ set; get; }
        public int Descuento{ set; get; }
        public bool DescActivo{ set; get; }
        public bool Devolucion { set; get; }
    }
}
