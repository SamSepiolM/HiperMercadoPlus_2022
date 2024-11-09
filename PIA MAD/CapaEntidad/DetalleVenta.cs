using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DetalleVenta
    {
        public int IdDetalleVenta{ set; get; }
        public int IdVenta { set; get; }
        public Venta oVenta { set; get; }
        public Producto oProducto{ set; get; }
        public MetodoPago oMetodoPago {set; get; }
        public decimal MontoPago { set; get; }
        public Caja oCaja { set; get; }
        public decimal PrecioVenta{ set; get; }
        public decimal PrecioUnitario { set; get; }
        public decimal Cantidad { set; get; }
        public decimal Subtotal{ set; get; }
        public decimal Descuento { set; get; }
        public decimal Utilidad { set; get; }
        public string Fecha{ set; get; }
        public string NombreProducto { set; get; }
        public bool unidad_medida { set; get; }
        public bool Devuelto { set; get; }

    }
}
