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
    public class CD_Reporte_Cajero
    {

        public List<ReporteCajero> Listar()
        {
            List<ReporteCajero> lista = new List<ReporteCajero>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();

                    //query.AppendLine("SELECT Convert(DATE, V.Fecha) AS Fecha, E.Nombre, E.ApePaterno, E.ApeMaterno, D.Nombre[Nombre_Depto], SUM(DV.Cantidad) AS Cantidad, SUM(DV.PrecioVenta) AS PrecioVenta, SUM(DV.Utilidad) AS Utilidad, SUM(DV.Subtotal) AS Subtotal, P.Codigo_producto, D.Clave FROM DetalleVenta DV");
                    //query.AppendLine("INNER JOIN Producto P ON DV.IdProducto=P.Codigo_producto INNER JOIN Departamento D ON D.Clave=P.Depto INNER JOIN Venta V ON V.IdVenta= DV.IdVenta INNER JOIN Empleado E ON E.IdEmpleado= V.IdCajero");
                    //query.AppendLine("GROUP BY Convert(DATE, V.Fecha), E.Nombre, E.ApePaterno, E.ApeMaterno, D.Nombre, P.Codigo_producto, D.Clave");

                    //SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    //cmd.CommandType = CommandType.Text; 

                    SqlCommand cmd = new SqlCommand("SP_ListarReporteCajero", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteCajero()
                            {
                                oDepartamento = new Departamento() { Clave = Convert.ToInt32(dr["Clave"]), Nombre = dr["Nombre_Depto"].ToString() },
                                oProducto = new Producto() { Codigo_producto = Convert.ToInt32(dr["Codigo_producto"])},
                                oDetalleVenta = new DetalleVenta()
                                {
                                    Fecha = dr["Fecha"].ToString(),
                                    Cantidad = Convert.ToDecimal(dr["Cantidad"]),
                                    Subtotal = Convert.ToDecimal(dr["Subtotal"]),
                                    PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"]),
                                    Utilidad = Convert.ToDecimal(dr["Utilidad"])
                                },
                                oCajero= new Empleado() { Nombre= dr["Nombre"].ToString(), ApePaterno= dr["ApePaterno"].ToString(), ApeMaterno= dr["ApeMaterno"].ToString() },


                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<ReporteCajero>();
                }
            }
            return lista;
        }
    }
}
