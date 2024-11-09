using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Departamento
    {
        private CD_Departamento objcd_Departamento = new CD_Departamento();

        public List<Departamento> Listar()
        {
            return objcd_Departamento.Listar();
        }

        public int Registrar(Departamento obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario el nombre\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Departamento.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Departamento obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario el nombre\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Departamento.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Departamento obj, out string Mensaje)
        {
            return objcd_Departamento.Eliminar(obj, out Mensaje);
        }
    }
}
