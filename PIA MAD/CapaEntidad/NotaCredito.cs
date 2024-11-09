using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class NotaCredito
    {
        public int Num_Recibo{ set; get; }
        public string Prod_regresado{ set; get; }
        public decimal Cantidad { set; get; }
        public decimal Subtotal{ set; get; }
        public decimal Total{ set; get; }
        public string Fecha { set; get; }
        public decimal PrecioUni { set; get; }
        public decimal DescuentoP { set; get; }
        public int Num_Devolucion { set; get; }
        public Empleado oEmpleado{ set; get; }
        public Producto oProducto { set; get; }
        public Venta oVenta { set; get; }
        public Caja oCaja { set; get; }
    }
}
