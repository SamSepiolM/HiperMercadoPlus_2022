using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Reporte_Venta
    {
        private CD_Reporte_Venta objcd_ReporteV = new CD_Reporte_Venta();

        public List<ReporteVenta> Listar()
        {
            return objcd_ReporteV.Listar();
        }
    }
}
