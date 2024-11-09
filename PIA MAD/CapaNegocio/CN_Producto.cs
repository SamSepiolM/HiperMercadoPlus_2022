using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Producto
    {
        private CD_Producto objcd_Producto = new CD_Producto();

        public List<Producto> Listar()
        {
            return objcd_Producto.Listar();
        }

        public List<Producto> BuscarNombre()
        {
            return objcd_Producto.BuscarNombre();
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario el nombre del producto\n";
            }
            if (obj.Costo <= 0)
            {
                Mensaje += "Es necesario el costo del producto\n";
            }
            if (obj.Precio_unitario <= 0)
            {
                Mensaje += "Es necesario el precio del producto\n";
            }
            if (obj.Punto_reorden <= 0)
            {
                Mensaje += "Es necesaria el punto de reorden\n";
            }

            if (obj.Descuento <= 0)
            {
                Mensaje += "Es necesariO el descuento\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Producto.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario el nombre del producto\n";
            }
            if (obj.Costo <= 0)
            {
                Mensaje += "Es necesario el costo del producto\n";
            }
            if (obj.Precio_unitario <= 0)
            {
                Mensaje += "Es necesario el precio del producto\n";
            }
            if (obj.Punto_reorden <= 0)
            {
                Mensaje += "Es necesaria el punto de reorden\n";
            }
            if (obj.Descuento <= 0)
            {
                Mensaje += "Es necesario el descuento\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Producto.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Producto obj, out string Mensaje)
        {
            return objcd_Producto.Eliminar(obj, out Mensaje);
        }
    }
}
