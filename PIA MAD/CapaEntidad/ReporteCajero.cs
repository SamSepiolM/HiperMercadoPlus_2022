using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ReporteCajero
    {
        public Departamento oDepartamento { set; get; }
        public Producto oProducto { set; get; }
        public DetalleVenta oDetalleVenta { set; get; }
        public Empleado oCajero { set; get; }

    }
}
