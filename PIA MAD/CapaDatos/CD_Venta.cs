using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Venta
    {
        public List<Venta> Listar()
        {
            List<Venta> lista = new List<Venta>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    //StringBuilder query = new StringBuilder();
                    //query.AppendLine(" Select IdVenta, IdCajero, TipoDocumento, NumeroDocumento, MontoPago, MontoCambio, MontoTotal, IdCaja FROM Venta V");
                    //query.AppendLine("INNER JOIN Empleado E ON E.IdEmpleado= V.IdCajero");

                    //SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    //cmd.CommandType = CommandType.Text;

                    SqlCommand cmd = new SqlCommand("SP_ListarVenta", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Venta()
                            {
                                IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                oEmpleado = new Empleado() { IdEmpleado = Convert.ToInt32(dr["IdCajero"]), Nombre = dr["Nombre"].ToString(), ApePaterno = dr["ApePaterno"].ToString(), ApeMaterno=dr["ApeMaterno"].ToString() },
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoPago = Convert.ToDecimal(dr["MontoPago"].ToString()),
                                MontoCambio = Convert.ToDecimal(dr["MontoCambio"].ToString()),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString()),
                                oCaja = new Caja() { Num_caja = Convert.ToString(dr["Num_caja"])},

                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<Venta>();
                }
            }
            return lista;
        }

        public int Registrar(Venta obj, out string Mensaje)
        {
            int idProductogenerado = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_RegVenta", oconexion);
                    cmd.Parameters.AddWithValue("IdCajero", obj.oEmpleado.IdEmpleado);
                    cmd.Parameters.AddWithValue("TipoDocumento", obj.TipoDocumento);
                    cmd.Parameters.AddWithValue("NumeroDocumento", obj.NumeroDocumento);
                    cmd.Parameters.AddWithValue("MontoPago", obj.MontoPago);
                    cmd.Parameters.AddWithValue("MontoCambio", obj.MontoCambio);
                    cmd.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("IdCaja", obj.oCaja.Id);
                    cmd.Parameters.AddWithValue("DescuentoT", obj.DescuentoTotal);
                    cmd.Parameters.AddWithValue("Fecha", obj.FechaRegistro);


                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    idProductogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }

            catch (Exception ex)
            {
                idProductogenerado = 0;
                Mensaje = ex.Message;
            }
            return idProductogenerado;
        }

        public int RegistrarDetalle(DetalleVenta obj, out string Mensaje)
        {
            int idProductogenerado = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_RegVentaDetalle", oconexion);
                    cmd.Parameters.AddWithValue("IdVenta", obj.oVenta.IdVenta);
                    cmd.Parameters.AddWithValue("IdProducto", obj.oProducto.Codigo_producto);
                    cmd.Parameters.AddWithValue("NombreProducto", obj.NombreProducto);
                    //cmd.Parameters.AddWithValue("Metodo_pago", obj.oMetodoPago.IdMetodo);
                    cmd.Parameters.AddWithValue("PrecioVenta", obj.PrecioVenta);
                    cmd.Parameters.AddWithValue("PrecioUnitario", obj.PrecioUnitario);
                    cmd.Parameters.AddWithValue("Cantidad", obj.Cantidad);
                    cmd.Parameters.AddWithValue("Subtotal", obj.Subtotal);
                    cmd.Parameters.AddWithValue("Descuento", obj.Descuento);
                    cmd.Parameters.AddWithValue("Unidad_medida", obj.unidad_medida);
                    cmd.Parameters.AddWithValue("Utilidad", obj.Utilidad);


                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    idProductogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }

            catch (Exception ex)
            {
                idProductogenerado = 0;
                Mensaje = ex.Message;
            }
            return idProductogenerado;
        }

        public void iniciarTransaccion(out string Mensaje)
        {
            int idProductogenerado = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_IniciarTran", oconexion);

                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }

            catch (Exception ex)
            {
                idProductogenerado = 0;
                Mensaje = ex.Message;
            }
            return;
        }

        public void confirmarTransaccion(out string Mensaje)
        {
            int idProductogenerado = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ConfirmarTran", oconexion);

                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }

            catch (Exception ex)
            {
                idProductogenerado = 0;
                Mensaje = ex.Message;
            }
            return;
        }

        public void cancelarTransaccion(out string Mensaje)
        {
            int idProductogenerado = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_CancelarTran", oconexion);

                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }

            catch (Exception ex)
            {
                idProductogenerado = 0;
                Mensaje = ex.Message;
            }
            return;
        }

        public bool Editar(Venta obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ModifProd", oconexion);

                    cmd.Parameters.AddWithValue("IdVenta", obj.IdVenta);
                    cmd.Parameters.AddWithValue("IdCajero", obj.oEmpleado.IdEmpleado);
                    cmd.Parameters.AddWithValue("TipoDocumento", obj.TipoDocumento);
                    cmd.Parameters.AddWithValue("NumeroDocumento", obj.NumeroDocumento);
                    cmd.Parameters.AddWithValue("MontoPago", obj.MontoPago);
                    cmd.Parameters.AddWithValue("MontoCambio", obj.MontoCambio);
                    cmd.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("IdCaja", obj.oCaja.Id);


                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }

            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }
            return respuesta;
        }

        public bool Eliminar(Venta obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ElimVenta", oconexion);
                    cmd.Parameters.AddWithValue("IdVenta", obj.IdVenta);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }

            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }
            return respuesta;
        }

        public decimal ObtenerSubtotal(DetalleVenta obj, out string Mensaje)
        {
            decimal subtotal = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("select dbo.ObtenerSubtotal(@PrecioV, @Cantidad)", oconexion))
                    {
                        cmd.Connection.Open();
                        cmd.Parameters.AddWithValue("PrecioV", obj.PrecioVenta);
                        cmd.Parameters.AddWithValue("Descuento", obj.Descuento);
                        cmd.Parameters.AddWithValue("Cantidad", obj.Cantidad);
                        cmd.Parameters.AddWithValue("costoProducto", obj.PrecioUnitario);

                        subtotal = Convert.ToDecimal(cmd.ExecuteScalar());
                    }
                    
                }
            }

            catch (Exception ex)
            {
                subtotal = 0;
                Mensaje = ex.Message;
            }
            return subtotal;
        }

        public decimal ObtenerDescuento(DetalleVenta obj, out string Mensaje)
        {
            decimal subtotal = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("select dbo.ObtenerDescuento(@Descuento, @Cantidad, @PrecioV)", oconexion))
                    {
                        cmd.Connection.Open();
                        cmd.Parameters.AddWithValue("Descuento", obj.Descuento);
                        cmd.Parameters.AddWithValue("Cantidad", obj.Cantidad);
                        cmd.Parameters.AddWithValue("PrecioV", obj.PrecioVenta);

                        subtotal = Convert.ToDecimal(cmd.ExecuteScalar());
                    }

                }
            }

            catch (Exception ex)
            {
                subtotal = 0;
                Mensaje = ex.Message;
            }
            return subtotal;
        }

        public decimal ObtenerTotalVenta(DetalleVenta obj, out string Mensaje)
        {
            decimal subtotal = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("select dbo.ObtenerTotalVenta(@PrecioV, @Descuento, @Cantidad)", oconexion))
                    {
                        cmd.Connection.Open();
                        cmd.Parameters.AddWithValue("PrecioV", obj.PrecioVenta);
                        cmd.Parameters.AddWithValue("Descuento", obj.Descuento);
                        cmd.Parameters.AddWithValue("Cantidad", obj.Cantidad);

                        subtotal = Convert.ToDecimal(cmd.ExecuteScalar());
                    }

                }
            }

            catch (Exception ex)
            {
                subtotal = 0;
                Mensaje = ex.Message;
            }
            return subtotal;
        }

        public decimal ObtenerUtilidadVenta(DetalleVenta obj, out string Mensaje)
        {
            decimal subtotal = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("select dbo.ObtenerUtilidadVenta(@PrecioV, @Descuento, @Cantidad, @costoProducto)", oconexion))
                    {
                        cmd.Connection.Open();
                        cmd.Parameters.AddWithValue("PrecioV", obj.PrecioVenta);
                        cmd.Parameters.AddWithValue("Descuento", obj.Descuento);
                        cmd.Parameters.AddWithValue("Cantidad", obj.Cantidad);
                        cmd.Parameters.AddWithValue("costoProducto", obj.PrecioUnitario);

                        subtotal = Convert.ToDecimal(cmd.ExecuteScalar());
                    }

                }
            }

            catch (Exception ex)
            {
                subtotal = 0;
                Mensaje = ex.Message;
            }
            return subtotal;
        }
    }
}
