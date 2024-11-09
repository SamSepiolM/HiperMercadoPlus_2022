using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ReporteVenta
    {
        public Departamento oDepartamento { set; get; }
        public Producto oProducto { set; get; }
        public DetalleVenta oDetalleVenta { set; get; }
        public Caja oCaja { set; get; }
    }
}
