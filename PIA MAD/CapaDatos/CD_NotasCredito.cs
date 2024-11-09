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
    public class CD_NotasCredito
    {
        public List<NotasCredito> Listar()
        {
            List<NotasCredito> lista = new List<NotasCredito>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    //query.AppendLine("SELECT ND.Num_Recibo, ND.Prod_regresado, ND.Cantidad, ND.Subtotal, ND.Total, ND.PrecioUni, ND.DescuentoP, ND.Unidad_medida, NC.Fecha, ND.IdProDev, C.Id, C.Num_caja FROM NotaCredito NC");
                    //query.AppendLine("INNER JOIN NotaCreditoDetalle ND ON ND.Id_Num_Dev= NC.Num_Devolucion");
                    //query.AppendLine("INNER JOIN Caja C ON NC.IdCaja= C.Id INNER JOIN Producto P ON ND.IdProDev=P.Codigo_producto");

                    //SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    //cmd.CommandType = CommandType.Text;

                    SqlCommand cmd = new SqlCommand("SP_ListarDetalleNota", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new NotasCredito()
                            {
                                IdDetalleVenta = Convert.ToInt32(dr["IdDetalleVenta"]),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                Cantidad = Convert.ToDecimal(dr["Cantidad"]),
                                Subtotal = Convert.ToDecimal(dr["Subtotal"]),
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"]),
                                Fecha = dr["Fecha"].ToString(),
                                PrecioUnitario = Convert.ToDecimal(dr["PrecioUnitario"]),
                                Descuento = Convert.ToDecimal(dr["Descuento"]),
                                unidad_medida=Convert.ToBoolean(dr["Unidad_medida"]),
                                oProducto = new Producto() { Codigo_producto = Convert.ToInt32(dr["IdProDev"]) },
                                //oEmpleado = new Empleado() { Nombre = dr["Nombre"].ToString(), ApePaterno = dr["ApePaterno"].ToString(), ApeMaterno = dr["ApeMaterno"].ToString() },
                                oCaja = new Caja() { Id = Convert.ToInt32(dr["Id"]), Num_caja = dr["Num_caja"].ToString() },

                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<NotasCredito>();
                }
            }
            return lista;
        }
        public List<NotasCredito> BusquedaFolio(string folio)
        {
            List<NotasCredito> lista = new List<NotasCredito>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    //query.AppendLine("SELECT IdVenta, Fecha, NombreProducto, Cantidad, IdProducto, Devuelto, Id, Num_caja FROM BusquedaFolioNota");
                    //query.AppendLine("SELECT V.IdVenta, V.Fecha, DV.NombreProducto, DV.Cantidad, DV.IdProducto, DV.Devuelto, C.Id, C.Num_caja FROM Venta V");
                    //query.AppendLine("INNER JOIN DetalleVenta DV ON V.IdVenta=DV.IdVenta");
                    //query.AppendLine("INNER JOIN CAJA C ON V.IdCaja=C.Id INNER JOIN Producto P ON P.Codigo_producto=DV.IdProducto");
                    //query.AppendLine("WHERE IdVenta=" + folio );


                    //SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    //cmd.CommandType = CommandType.Text;

                    SqlCommand cmd = new SqlCommand("SP_DetalleNotaBusquedaFolio", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("folio", folio);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new NotasCredito()
                            {
                                IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                Cantidad = Convert.ToDecimal(dr["Cantidad"]),
                                Fecha = dr["Fecha"].ToString(),
                                oProducto = new Producto() { Codigo_producto = Convert.ToInt32(dr["IdProducto"]) },
                                //oEmpleado = new Empleado() { Nombre = dr["Nombre"].ToString(), ApePaterno = dr["ApePaterno"].ToString(), ApeMaterno = dr["ApeMaterno"].ToString() },
                                oCaja = new Caja() { Id = Convert.ToInt32(dr["Id"]), Num_caja = dr["Num_caja"].ToString() },

                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<NotasCredito>();
                }
            }
            return lista;
        }
        public List<NotasCredito> BusquedaProducto(string folio, string idproducto)
        {
            List<NotasCredito> lista = new List<NotasCredito>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    //query.AppendLine("SELECT V.IdVenta, V.Fecha, DV.IdDetalleVenta, DV.IdProducto, DV.NombreProducto, DV.Cantidad, DV.Subtotal, DV.PrecioVenta, DV.PrecioUnitario, DV.Descuento, DV.Unidad_medida, D.Devolucion, C.Id, C.Num_caja FROM Venta V");
                    //query.AppendLine("INNER JOIN DetalleVenta DV ON V.IdVenta=DV.IdVenta");
                    //query.AppendLine("INNER JOIN Caja C ON V.IdCaja=C.Id INNER JOIN Producto P ON P.Codigo_producto=DV.IdProducto");
                    //query.AppendLine("INNER JOIN Departamento D ON P.Depto=D.Clave");
                    //query.AppendLine("WHERE V.IdVenta=" + folio + " AND DV.IdProducto="+ idproducto);

                    //SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    //cmd.CommandType = CommandType.Text;

                    SqlCommand cmd = new SqlCommand("SP_DetalleNotaBusquedaProducto", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("folio", folio);
                    cmd.Parameters.AddWithValue("idproducto", idproducto);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new NotasCredito()
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
                                unidad_medida = Convert.ToBoolean(dr["Unidad_medida"]),
                                Devolucion=Convert.ToBoolean(dr["Devolucion"]),
                                oProducto = new Producto() { Codigo_producto = Convert.ToInt32(dr["IdProducto"]) },
                                //oEmpleado = new Empleado() { Nombre = dr["Nombre"].ToString(), ApePaterno = dr["ApePaterno"].ToString(), ApeMaterno = dr["ApeMaterno"].ToString() },
                                oCaja = new Caja() { Id = Convert.ToInt32(dr["Id"]), Num_caja = dr["Num_caja"].ToString() },

                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<NotasCredito>();
                }
            }
            return lista;
        }

        public List<NotasCredito> BusquedaCantidadExisteNota(string IdTicketV, string IdProDev)
        {
            List<NotasCredito> lista = new List<NotasCredito>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    //query.AppendLine("SELECT IdTicketV, IdProDev, Cantidad FROM BusquedaCantidadExisteNota");
                    //query.AppendLine("WHERE IdTicketV=" + IdTicketV + "AND IdProDev=" + IdProDev);


                    //SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    //cmd.CommandType = CommandType.Text;
                    SqlCommand cmd = new SqlCommand("SP_DetalleNotaBusquedaProducto", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("IdTicketV", IdTicketV);
                    cmd.Parameters.AddWithValue("IdProDev", IdProDev);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new NotasCredito()
                            {
                                IdVenta = Convert.ToInt32(dr["IdTicketV"]),
                                Cantidad = Convert.ToDecimal(dr["Cantidad"]),
                                oProducto = new Producto() { Codigo_producto = Convert.ToInt32(dr["IdProDev"]) },
                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<NotasCredito>();
                }
            }
            return lista;
        }

        public int Registrar(NotasCredito obj, out string Mensaje)
        {
            int idProductogenerado = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_RegNota", oconexion);
                    
                    cmd.Parameters.AddWithValue("IdAdmin", obj.oEmpleado.IdEmpleado);
                    cmd.Parameters.AddWithValue("IdCaja", obj.oCaja.Id);
                    cmd.Parameters.AddWithValue("IdTicketV", obj.oVenta.IdVenta);
                    cmd.Parameters.AddWithValue("MontoCambio", obj.MontoCambio);
                    cmd.Parameters.AddWithValue("Fecha", obj.FechaRegistro);
                    //cmd.Parameters.AddWithValue("Fecha", obj.Fecha);

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
        public int RegistrarDetalle(NotasCredito obj, out string Mensaje)
        {
            int idProductogenerado = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_RegNotaDetalle", oconexion);

                    cmd.Parameters.AddWithValue("Id_Num_Dev", obj.oVenta.IdVenta);
                    cmd.Parameters.AddWithValue("IdProDev", obj.oProducto.Codigo_producto);
                    cmd.Parameters.AddWithValue("Prod_regresado", obj.NombreProducto);
                    cmd.Parameters.AddWithValue("Cantidad", obj.Cantidad);
                    cmd.Parameters.AddWithValue("Subtotal", obj.Subtotal);
                    cmd.Parameters.AddWithValue("Total", obj.PrecioVenta);
                    cmd.Parameters.AddWithValue("DescuentoP", obj.Descuento);
                    cmd.Parameters.AddWithValue("PrecioUni", obj.PrecioUnitario);
                    cmd.Parameters.AddWithValue("Unidad_medida", obj.unidad_medida);
                    cmd.Parameters.AddWithValue("Merma", obj.Merma);
                    //cmd.Parameters.AddWithValue("Fecha", obj.Fecha);

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

    }
}
