using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class MetodoPago
    {
        public int IdMetodo{ set; get; }
        public string NombreMetodo{ set; get; }
        public int IdVentaM { set; get; }
        public decimal MontoPago { set; get; }
    }
}
