using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Empleado
    {
        private CD_Empleado objcd_empleado = new CD_Empleado();

        public List<Empleado> Listar()
        {
            return objcd_empleado.Listar();
        }

        public List<Empleado> Login()
        {
            return objcd_empleado.Login();
        }

        public List<Empleado> ListarCajeros()
        {
            return objcd_empleado.ListarCajeros();
        }

        public int Registrar(Empleado obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Nombre == ""){
                Mensaje += "Es necesario el nombre\n";
            }
            if (obj.ApePaterno == "")
            {
                Mensaje += "Es necesario el apellido paterno\n";
            }
            if (obj.ApeMaterno == "")
            {
                Mensaje += "Es necesario el apellido materno\n";
            }
            if (obj.CURP == "")
            {
                Mensaje += "Es necesario el CURP\n";
            }
            if (obj.Contrasenia == "")
            {
                Mensaje += "Es necesaria la contrasenia\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else {
                return objcd_empleado.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Empleado obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario el nombre\n";
            }
            if (obj.ApePaterno == "")
            {
                Mensaje += "Es necesario el apellido paterno\n";
            }
            if (obj.ApeMaterno == "")
            {
                Mensaje += "Es necesario el apellido materno\n";
            }
            if (obj.CURP == "")
            {
                Mensaje += "Es necesario el CURP\n";
            }
            if (obj.Contrasenia == "")
            {
                Mensaje += "Es necesaria la contrasenia\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_empleado.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Empleado obj, out string Mensaje)
        {
            return objcd_empleado.Eliminar(obj, out Mensaje);
        }
    }
}
