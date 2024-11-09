using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Detalle_Venta
    {
        public List<DetalleVenta> Listar()
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    //query.AppendLine("SELECT DV.IdDetalleVenta, DV.NombreProducto, DV.Cantidad, DV.Subtotal, DV.PrecioVenta, DV.PrecioUnitario, DV.Descuento, V.Fecha, DV.IdProducto, DV.Devuelto, C.Id, C.Num_caja FROM Venta V");
                    //query.AppendLine("INNER JOIN DetalleVenta DV ON V.IdVenta=DV.IdVenta");
                    //query.AppendLine("INNER JOIN Caja C ON V.IdCaja=C.Id INNER JOIN Producto P ON DV.IdProducto=P.Codigo_producto");

                    //SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    //cmd.CommandType = CommandType.Text;

                    SqlCommand cmd = new SqlCommand("SP_ListarDetalleVenta", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new DetalleVenta()
                            {
                                IdDetalleVenta = Convert.ToInt32(dr["IdDetalleVenta"]),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                Cantidad = Convert.ToDecimal(dr["Cantidad"]),
                                Subtotal = decimal.Parse(dr["Subtotal"].ToString()),
                                PrecioVenta = decimal.Parse(dr["PrecioVenta"].ToString()),
                                PrecioUnitario = Convert.ToDecimal(dr["PrecioUnitario"]),
                                Descuento = Convert.ToDecimal(dr["Descuento"]),
                                Fecha = dr["Fecha"].ToString(),
                                oProducto = new Producto() { Codigo_producto = Convert.ToInt32(dr["IdProducto"]) },
                                //oEmpleado = new Empleado() { Nombre = dr["Nombre"].ToString(), ApePaterno = dr["ApePaterno"].ToString(), ApeMaterno = dr["ApeMaterno"].ToString() },
                                oCaja = new Caja() { Id = Convert.ToInt32(dr["Id"]), Num_caja = dr["Num_caja"].ToString() },
                                Devuelto= Convert.ToBoolean(dr["Devuelto"].ToString())
                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<DetalleVenta>();
                }
            }
            return lista;
        }
        public List<DetalleVenta> BusquedaAvanzada(string fecha, string caja)
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT IdVenta, Fecha, Id, Num_caja FROM BusquedaAvanzadaVenta ");
                    //query.AppendLine("SELECT V.IdVenta, V.Fecha, C.Id, C.Num_caja FROM Venta V");
                    //query.AppendLine("INNER JOIN Caja C ON V.IdCaja=C.Id");
                    query.AppendLine("WHERE Num_caja=" + caja);


                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new DetalleVenta()
                            {
                                IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                Fecha = dr["Fecha"].ToString(),
                                //oEmpleado = new Empleado() { Nombre = dr["Nombre"].ToString(), ApePaterno = dr["ApePaterno"].ToString(), ApeMaterno = dr["ApeMaterno"].ToString() },
                                oCaja = new Caja() { Id = Convert.ToInt32(dr["Id"]), Num_caja = dr["Num_caja"].ToString() },

                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<DetalleVenta>();
                }
            }
            return lista;
        }
        public List<DetalleVenta> BusquedaFolio(string folio)
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT IdVenta, MontoTotal, MontoPago, MontoCambio, DescuentoT, IdDetalleVenta, NombreProducto, Cantidad, Subtotal, PrecioVenta, PrecioUnitario, Descuento, Fecha, IdProducto, Devuelto, Id, Num_caja FROM BusquedaFolioVenta");
                    //query.AppendLine("SELECT V.IdVenta, V.MontoTotal, V.MontoPago, V.MontoCambio, V.DescuentoT, DV.IdDetalleVenta, DV.NombreProducto, DV.Cantidad, DV.Subtotal, DV.PrecioVenta, DV.PrecioUnitario, DV.Descuento, V.Fecha, DV.IdProducto, DV.Devuelto, C.Id, C.Num_caja FROM Venta V");
                    //query.AppendLine("INNER JOIN DetalleVenta DV ON V.IdVenta=DV.IdVenta");
                    //query.AppendLine("INNER JOIN Caja C ON V.IdCaja=C.Id INNER JOIN Producto P ON DV.IdProducto=P.Codigo_producto");

                    query.AppendLine("WHERE IdVenta=" + folio);


                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new DetalleVenta()
                            {
                                IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                IdDetalleVenta = Convert.ToInt32(dr["IdDetalleVenta"]),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                Cantidad = Convert.ToDecimal(dr["Cantidad"]),
                                Subtotal = Convert.ToDecimal(dr["Subtotal"]),
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"]),
                                Fecha = dr["Fecha"].ToString(),
                                PrecioUnitario = Convert.ToDecimal(dr["PrecioUnitario"]),
                                Descuento = Convert.ToDecimal(dr["Descuento"]),
                                oProducto = new Producto() { Codigo_producto = Convert.ToInt32(dr["IdProducto"]) },
                                //oEmpleado = new Empleado() { Nombre = dr["Nombre"].ToString(), ApePaterno = dr["ApePaterno"].ToString(), ApeMaterno = dr["ApeMaterno"].ToString() },
                                oCaja = new Caja() { Id = Convert.ToInt32(dr["Id"]), Num_caja = dr["Num_caja"].ToString() },
                                Devuelto = Convert.ToBoolean(dr["Devuelto"].ToString()),
                                oVenta=new Venta() { MontoCambio= Convert.ToDecimal(dr["MontoCambio"]), MontoPago= Convert.ToDecimal(dr["MontoPago"]) , MontoTotal= Convert.ToDecimal(dr["MontoTotal"]), DescuentoTotal= Convert.ToDecimal(dr["DescuentoT"]) }
                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<DetalleVenta>();
                }
            }
            return lista;
        }
    }
}
