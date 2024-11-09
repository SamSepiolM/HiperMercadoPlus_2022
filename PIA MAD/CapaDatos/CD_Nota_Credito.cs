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
    public class CD_Nota_Credito
    {
        public List<NotaCredito> Listar()
        {
            List<NotaCredito> lista = new List<NotaCredito>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    //query.AppendLine("SELECT ND.Num_Recibo, ND.Prod_regresado, ND.Cantidad, ND.Subtotal, ND.Total, ND.PrecioUni, ND.DescuentoP, NC.Fecha, ND.IdProDev, C.Id, C.Num_caja FROM NotaCredito NC");
                    //query.AppendLine("INNER JOIN NotaCreditoDetalle ND ON ND.Id_Num_Dev= NC.Num_Devolucion");
                    //query.AppendLine("INNER JOIN Caja C ON NC.IdCaja= C.Id INNER JOIN Producto P ON ND.IdProDev=P.Codigo_producto");

                    //SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    //cmd.CommandType = CommandType.Text;

                    SqlCommand cmd = new SqlCommand("SP_ListarNota", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new NotaCredito()
                            {
                                Num_Recibo = Convert.ToInt32(dr["Num_Recibo"]),
                                Prod_regresado = dr["Prod_regresado"].ToString(),
                                Cantidad = Convert.ToDecimal(dr["Cantidad"]),
                                Subtotal = decimal.Parse(dr["Subtotal"].ToString()),
                                Total= decimal.Parse(dr["Total"].ToString()),
                                Fecha= dr["Fecha"].ToString(),
                                PrecioUni = Convert.ToDecimal(dr["PrecioUni"]),
                                DescuentoP = Convert.ToDecimal(dr["DescuentoP"]),
                                oProducto = new Producto() { Codigo_producto = Convert.ToInt32(dr["IdProDev"]) },
                                //oEmpleado = new Empleado() { Nombre = dr["Nombre"].ToString(), ApePaterno = dr["ApePaterno"].ToString(), ApeMaterno = dr["ApeMaterno"].ToString() },
                                oCaja=new Caja() { Id= Convert.ToInt32(dr["Id"]), Num_caja=dr["Num_caja"].ToString() },

                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<NotaCredito>();
                }
            }
            return lista;
        }
        public List<NotaCredito> BusquedaAvanzada(string fecha, string caja)
        {
            List<NotaCredito> lista = new List<NotaCredito>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    //query.AppendLine("SELECT Num_Devolucion, Fecha, Id, Num_caja FROM BusquedaAvanzadaNota");
                    //query.AppendLine("SELECT NC.Num_Devolucion, NC.Fecha, C.Id, C.Num_caja FROM NotaCredito NC");
                    //query.AppendLine("INNER JOIN Caja C ON NC.IdCaja= C.Id");
                    //query.AppendLine("WHERE Num_caja=" + caja);
                    SqlCommand cmd = new SqlCommand("SP_NotaBusquedaAvanzada", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("caja", caja);

                    //SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    //cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new NotaCredito()
                            {
                                Num_Devolucion = Convert.ToInt32(dr["Num_Devolucion"]),
                                Fecha = dr["Fecha"].ToString(),
                                //oEmpleado = new Empleado() { Nombre = dr["Nombre"].ToString(), ApePaterno = dr["ApePaterno"].ToString(), ApeMaterno = dr["ApeMaterno"].ToString() },
                                oCaja = new Caja() { Id = Convert.ToInt32(dr["Id"]), Num_caja = dr["Num_caja"].ToString() },

                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<NotaCredito>();
                }
            }
            return lista;
        }
        public List<NotaCredito> BusquedaFolio(string folio)
        {
            List<NotaCredito> lista = new List<NotaCredito>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    //query.AppendLine("SELECT Num_Devolucion, IdTicketV, Num_Recibo, Prod_regresado, Cantidad, Subtotal, Total, PrecioUni, DescuentoP, Fecha, IdProDev, Id, Num_caja FROM BusquedaFolioNotaD");
                    //query.AppendLine("SELECT NC.Num_Devolucion, NC.IdTicketV, ND.Num_Recibo, ND.Prod_regresado, ND.Cantidad, ND.Subtotal, ND.Total, ND.PrecioUni, ND.DescuentoP, NC.Fecha, ND.IdProDev, C.Id, C.Num_caja FROM NotaCredito NC");
                    //query.AppendLine("INNER JOIN NotaCreditoDetalle ND ON ND.Id_Num_Dev= NC.Num_Devolucion");
                    //query.AppendLine("INNER JOIN Caja C ON NC.IdCaja= C.Id INNER JOIN Producto P ON ND.IdProDev=P.Codigo_producto");
                    //query.AppendLine("WHERE Num_Devolucion=" + folio);


                    //SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    //cmd.CommandType = CommandType.Text;

                    SqlCommand cmd = new SqlCommand("SP_NotaBusquedaFolio", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("folio", folio);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new NotaCredito()
                            {
                                Num_Devolucion= Convert.ToInt32(dr["Num_Devolucion"]),
                                Num_Recibo = Convert.ToInt32(dr["Num_Recibo"]),
                                Prod_regresado = dr["Prod_regresado"].ToString(),
                                Cantidad = Convert.ToDecimal(dr["Cantidad"]),
                                Subtotal = Convert.ToDecimal(dr["Subtotal"]),
                                Total = Convert.ToDecimal(dr["Total"]),
                                Fecha = dr["Fecha"].ToString(),
                                PrecioUni= Convert.ToDecimal(dr["PrecioUni"]),
                                DescuentoP = Convert.ToDecimal(dr["DescuentoP"]),
                                oProducto = new Producto() { Codigo_producto = Convert.ToInt32(dr["IdProDev"]) },
                                //oEmpleado = new Empleado() { Nombre = dr["Nombre"].ToString(), ApePaterno = dr["ApePaterno"].ToString(), ApeMaterno = dr["ApeMaterno"].ToString() },
                                oCaja = new Caja() { Id = Convert.ToInt32(dr["Id"]), Num_caja = dr["Num_caja"].ToString() },
                                oVenta=new Venta() { IdVenta= Convert.ToInt32(dr["IdTicketV"])}
                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<NotaCredito>();
                }
            }
            return lista;
        }
    }
}
