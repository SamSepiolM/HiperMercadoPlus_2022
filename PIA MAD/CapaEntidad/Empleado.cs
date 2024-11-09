using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Empleado
    {
        public int IdEmpleado { set; get; }
        public string Contrasenia{ set; get; }
        public string Nombre{ set; get; }
        public string ApePaterno{ set; get; }
        public string ApeMaterno{ set; get; }
        public string CURP{ set; get; }
        public string NumNomina { set; get; }
        public string Correo{ set; get; }
        public string Telefono{ set; get; }
        public string Domicilio { set; get; }
        public bool Estado { set; get; }
        public Puesto oPuesto { set; get; }
        public string FechaRegistro { set; get; }
    }
}
