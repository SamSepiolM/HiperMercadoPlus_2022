using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Reporte_Cajero
    {
        private CD_Reporte_Cajero objcd_ReporteC = new CD_Reporte_Cajero();

        public List<ReporteCajero> Listar()
        {
            return objcd_ReporteC.Listar();
        }
    }
}
