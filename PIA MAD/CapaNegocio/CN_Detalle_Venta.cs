using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Detalle_Venta
    {
        private CD_Detalle_Venta objcd_DetalleV = new CD_Detalle_Venta();

        public List<DetalleVenta> Listar()
        {
            return objcd_DetalleV.Listar();
        }

        public List<DetalleVenta> BusquedaAvanzada(string fecha, string caja)
        {
            return objcd_DetalleV.BusquedaAvanzada(fecha, caja);
        }

        public List<DetalleVenta> BusquedaFolio(string folio)
        {
            return objcd_DetalleV.BusquedaFolio(folio);
        }
    }
}
