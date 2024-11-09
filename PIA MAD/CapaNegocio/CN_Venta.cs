using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Venta
    {
        private CD_Venta objcd_Venta = new CD_Venta();

        public List<Venta> Listar()
        {
            return objcd_Venta.Listar();
        }

        public int Registrar(Venta obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.TipoDocumento == "")
            {
                Mensaje += "Es necesario el tipo de documento\n";
            }
            if (obj.NumeroDocumento == "")
            {
                Mensaje += "Es necesario el numero de documento\n";
            }
            if (obj.MontoPago <= 0)
            {
                Mensaje += "Es necesario el Monto de pago\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Venta.Registrar(obj, out Mensaje);
            }
        }

        public int RegistrarDetalle(DetalleVenta obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Venta.RegistrarDetalle(obj, out Mensaje);
            }
        }

        public decimal ObtenerSubtotal(DetalleVenta obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Venta.ObtenerSubtotal(obj, out Mensaje);
            }
        }

        public decimal ObtenerDescuento(DetalleVenta obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Venta.ObtenerDescuento(obj, out Mensaje);
            }
        }

        public decimal ObtenerTotalVenta(DetalleVenta obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Venta.ObtenerTotalVenta(obj, out Mensaje);
            }
        }

        public decimal ObtenerUtilidadVenta(DetalleVenta obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Venta.ObtenerUtilidadVenta(obj, out Mensaje);
            }
        }

        public void iniciarTransaccion(out string Mensaje)
        {
            Mensaje = string.Empty;

            if (Mensaje != string.Empty)
            {
                return ;
            }
            else
            {
                objcd_Venta.iniciarTransaccion(out Mensaje);
            }
        }

        public void confirmarTransaccion(out string Mensaje)
        {
            Mensaje = string.Empty;

            if (Mensaje != string.Empty)
            {
                return;
            }
            else
            {
                objcd_Venta.confirmarTransaccion(out Mensaje);
            }
        }

        public void cancelarTransaccion(out string Mensaje)
        {
            Mensaje = string.Empty;

            if (Mensaje != string.Empty)
            {
                return;
            }
            else
            {
                objcd_Venta.cancelarTransaccion(out Mensaje);
            }
        }

        public bool Editar(Venta obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.TipoDocumento == "")
            {
                Mensaje += "Es necesario el tipo de documento\n";
            }
            if (obj.NumeroDocumento == "")
            {
                Mensaje += "Es necesario el numero de documento\n";
            }
            if (obj.MontoPago <= 0)
            {
                Mensaje += "Es necesario el Monto de pago\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Venta.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Venta obj, out string Mensaje)
        {
            return objcd_Venta.Eliminar(obj, out Mensaje);
        }
    }
}
