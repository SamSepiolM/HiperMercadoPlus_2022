using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Caja
    {
        private CD_Caja objcd_Caja = new CD_Caja();

        public List<Caja> Listar()
        {
            return objcd_Caja.Listar();
        }

        public int Registrar(Caja obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Num_caja == "")
            {
                Mensaje += "Es necesario el numero de la caja\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Caja.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Caja obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Num_caja == "")
            {
                Mensaje += "Es necesario el numero de la caja\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Caja.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Caja obj, out string Mensaje)
        {
            return objcd_Caja.Eliminar(obj, out Mensaje);
        }
    }
}