using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class NotasCredito
    {
        public int IdVenta { set; get; }
        //public int Num_Devolucion { set; get; }
        public string NombreProducto { set; get; }
        public decimal Cantidad { set; get; }
        public decimal Subtotal { set; get; }
        public decimal PrecioVenta { set; get; }
        public string Fecha { set; get; }
        public decimal PrecioUnitario { set; get; }
        public decimal Descuento { set; get; }
        public decimal MontoCambio { set; get; }
        public int IdDetalleVenta { set; get; }
        public bool unidad_medida{ set; get; }
        public bool Devolucion { set; get; }
        public bool Merma { set; get; }
        public Empleado oEmpleado { set; get; }
        public Producto oProducto { set; get; }
        public Caja oCaja { set; get; }
        public Venta oVenta { set; get; }
        public DateTime FechaRegistro { set; get; }
    }
}
