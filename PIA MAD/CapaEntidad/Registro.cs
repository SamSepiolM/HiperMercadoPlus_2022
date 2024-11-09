using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Registro
    {
        public int IdRegistro{ set; get; }
        public Empleado oEmpleado{ set; get; }
        public Caja oCaja { set; get; }
        public Producto oProducto{ set; get; }
    }
}
