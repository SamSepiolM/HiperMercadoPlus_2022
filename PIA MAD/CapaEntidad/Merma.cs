using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Merma
    {
        public int Id_Merma { set; get; }
        public string prodNombre { set; get; }
        public int prodMerma { set; get; }
        public NotaCredito oNotaCredito { set; get; }
    }
}
