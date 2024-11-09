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
    public class CD_Reporte_Venta
    {
        public List<ReporteVenta> Listar()
        {
            List<ReporteVenta> lista = new List<ReporteVenta>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    //query.AppendLine("SELECT Fecha, Cantidad, Subtotal, Descuento, PrecioVenta, PrecioUnitario, Utilidad, Precio_unitario, Codigo_producto, Clave, Nombre, Id, Num_caja FROM ReporteVentas");
                    //query.AppendLine("SELECT Convert(DATE, V.Fecha) AS Fecha, SUM(Cantidad) AS Cantidad, SUM(Subtotal) AS Subtotal, SUM(DV.Descuento) AS Descuento, SUM(PrecioVenta) AS PrecioVenta, PrecioUnitario, SUM(Utilidad) AS Utilidad, Precio_unitario, Codigo_producto, Clave, D.Nombre, C.Id, C.Num_caja FROM DetalleVenta DV");
                    //query.AppendLine("INNER JOIN Producto P ON DV.IdProducto=P.Codigo_producto INNER JOIN Departamento D ON D.Clave=P.Depto INNER JOIN Venta V ON V.IdVenta=DV.IdVenta INNER JOIN Caja C ON C.Id = V.IdCaja");
                    //query.AppendLine("GROUP BY Convert(DATE, V.Fecha), D.Nombre, P.Codigo_producto, DV.PrecioUnitario, P.Precio_unitario, D.Clave, C.Id, C.Num_caja");


                    //SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    //cmd.CommandType = CommandType.Text;

                    SqlCommand cmd = new SqlCommand("SP_ListarReporteVenta", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteVenta()
                            {
                                oDepartamento = new Departamento() { Clave = Convert.ToInt32(dr["Clave"]), Nombre = dr["Nombre"].ToString() },
                                oProducto=new Producto() { Codigo_producto= Convert.ToInt32(dr["Codigo_producto"]), Precio_unitario= Convert.ToInt32(dr["Precio_unitario"]) },
                                oCaja = new Caja() { Id = Convert.ToInt32(dr["Id"]), Num_caja = dr["Num_caja"].ToString() },

                                oDetalleVenta = new DetalleVenta()
                                {
                                    Fecha = dr["Fecha"].ToString(),
                                    Cantidad = Convert.ToDecimal(dr["Cantidad"]),
                                    Subtotal = Convert.ToDecimal(dr["Subtotal"]),
                                    Descuento = Convert.ToDecimal(dr["Descuento"]),
                                    PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"]),
                                    PrecioUnitario = Convert.ToDecimal(dr["PrecioUnitario"]),
                                    Utilidad = Convert.ToDecimal(dr["Utilidad"]),
                                    
                                }

                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<ReporteVenta>();
                }
            }
            return lista;
        }
    }
}
