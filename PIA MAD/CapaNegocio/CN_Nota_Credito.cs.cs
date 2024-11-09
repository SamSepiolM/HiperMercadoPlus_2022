using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Nota_Credito
    {
        private CD_Nota_Credito objcd_NotaC = new CD_Nota_Credito();

        public List<NotaCredito> Listar()
        {
            return objcd_NotaC.Listar();
        }

        public List<NotaCredito> BusquedaAvanzada(string fecha, string caja)
        {
            return objcd_NotaC.BusquedaAvanzada(fecha, caja);
        }

        public List<NotaCredito> BusquedaFolio(string folio)
        {
            return objcd_NotaC.BusquedaFolio(folio);
        }
    }
}
