using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Puesto
    {
        private CD_Puesto objcd_puesto = new CD_Puesto();

        public List<Puesto> Listar()
        {
            return objcd_puesto.Listar();
        }

    }
}
