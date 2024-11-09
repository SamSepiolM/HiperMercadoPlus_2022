using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Venta
    {
        public int IdVenta{ set; get; }
        public Empleado oEmpleado{ set; get; }
        public string TipoDocumento{ set; get; }
        public string NumeroDocumento{ set; get; }
        public decimal MontoPago{ set; get; }
        public decimal MontoCambio{ set; get; }
        public decimal MontoTotal{ set; get; }
        public decimal DescuentoTotal { set; get; }
        public bool unidad_medida { set; get; }
        public Caja oCaja { set; get; }
        public DateTime FechaRegistro { set; get; }
        public List<DetalleVenta> oDetalleVenta { set; get; }
    }
}
