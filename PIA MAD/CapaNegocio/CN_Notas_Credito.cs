using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Notas_Credito
    {
        private CD_NotasCredito objcd_NotasC = new CD_NotasCredito();

        public List<NotasCredito> Listar()
        {
            return objcd_NotasC.Listar();
        }

        public List<NotasCredito> BusquedaFolio(string folio)
        {
            return objcd_NotasC.BusquedaFolio(folio);
        }
        public List<NotasCredito> BusquedaProducto(string folio, string idproducto)
        {
            return objcd_NotasC.BusquedaProducto(folio, idproducto);
        }

        public List<NotasCredito> BusquedaCantidadExisteNota(string folio, string idproducto)
        {
            return objcd_NotasC.BusquedaCantidadExisteNota(folio, idproducto);
        }

        public int Registrar(NotasCredito obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_NotasC.Registrar(obj, out Mensaje);
            }
        }

        public int RegistrarDetalle(NotasCredito obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_NotasC.RegistrarDetalle(obj, out Mensaje);
            }
        }
    }
}
