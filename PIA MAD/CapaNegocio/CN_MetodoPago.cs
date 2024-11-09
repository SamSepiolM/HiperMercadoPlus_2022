using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_MetodoPago
    {
        private CD_MetodoPago objcd_MetodoPago = new CD_MetodoPago();
        public List<MetodoPago> Listar()
        {
            return objcd_MetodoPago.Listar();
        }

        public List<MetodoPago> ListarFolio(string folio)
        {
            return objcd_MetodoPago.ListarFolio(folio);
        }

        public int Registrar(MetodoPago obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_MetodoPago.Registrar(obj, out Mensaje);
            }
        }

        public void ActualizarMetodo(MetodoPago obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (Mensaje != string.Empty)
            {
                return;
            }
            else
            {
                objcd_MetodoPago.ActualizarMetodo(obj, out Mensaje);
            }
        }
    }
}
