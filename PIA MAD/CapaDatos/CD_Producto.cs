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
    public class CD_Producto
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    //uery.AppendLine("SELECT Codigo_producto, P.Nombre, Descripcion,Unidad_medida, Costo, Precio_unitario, Existencia, Unidad_vendida, Merma, Stock, Punto_reorden, D.Clave, D.Nombre[Nombre_Depto], P.DescuentoProducto, P.FechaInicio, P.FechaFin, E.Nombre+ ' ' + E.ApePaterno + ' '+ E.ApeMaterno AS NombreMod, P.FechaModificacion FROM Producto P");
                    //query.AppendLine("INNER JOIN Departamento D ON D.Clave = P.Depto");
                    //query.AppendLine("INNER JOIN Empleado E ON E.IdEmpleado=P.IdAdminMod");

                    //SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    //cmd.CommandType = CommandType.Text;

                    SqlCommand cmd = new SqlCommand("SP_ListarProducto", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                Codigo_producto = Convert.ToInt32(dr["Codigo_producto"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Unidad_medida = Convert.ToBoolean(dr["Unidad_medida"]),
                                Costo = Convert.ToDecimal(dr["Costo"].ToString()),
                                Precio_unitario = Convert.ToDecimal(dr["Precio_unitario"].ToString()),
                                Existencia = Convert.ToDecimal(dr["Existencia"].ToString()),
                                Vendido = Convert.ToDecimal(dr["Unidad_vendida"].ToString()),
                                Merma = Convert.ToDecimal(dr["Merma"].ToString()),
                                Stock = Convert.ToBoolean(dr["Stock"]),
                                Punto_reorden = Convert.ToDecimal(dr["Punto_reorden"].ToString()),
                                oDepartamento = new Departamento() { Clave = Convert.ToInt32(dr["Clave"]), Nombre = dr["Nombre_Depto"].ToString() },
                                Descuento = Convert.ToDecimal(dr["DescuentoProducto"].ToString()),
                                FechaInicio = DateTime.Parse(dr["FechaInicio"].ToString()),
                                FechaFin = DateTime.Parse(dr["FechaFin"].ToString()),
                                FechaModificacion= DateTime.Parse(dr["FechaModificacion"].ToString()),
                                NombreAdminMod= dr["NombreMod"].ToString()
                                //FechaRegistro=
                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<Producto>();
                }
            }
            return lista;
        }

        public List<Producto> BuscarNombre()
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine(" SELECT Codigo_producto, P.Nombre, Descripcion,Unidad_medida, Costo, Precio_unitario, Existencia, Unidad_vendida, Merma, Stock, Punto_reorden, D.Clave, D.Nombre[Nombre_Depto] FROM Producto P");
                    query.AppendLine("INNER JOIN Departamento D ON D.Clave = P.Depto");
                    query.AppendLine("WHERE Stock=1");


                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                Codigo_producto = Convert.ToInt32(dr["Codigo_producto"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Unidad_medida = Convert.ToBoolean(dr["Unidad_medida"]),
                                Costo = Convert.ToDecimal(dr["Costo"].ToString()),
                                Precio_unitario = Convert.ToDecimal(dr["Precio_unitario"].ToString()),
                                Existencia = Convert.ToDecimal(dr["Existencia"].ToString()),
                                Vendido = Convert.ToDecimal(dr["Unidad_vendida"].ToString()),
                                Merma = Convert.ToDecimal(dr["Merma"].ToString()),
                                Stock = Convert.ToBoolean(dr["Stock"]),
                                Punto_reorden = Convert.ToDecimal(dr["Punto_reorden"].ToString()),
                                oDepartamento = new Departamento() { Clave = Convert.ToInt32(dr["Clave"]), Nombre = dr["Nombre_Depto"].ToString() },

                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<Producto>();
                }
            }
            return lista;
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            int idProductogenerado = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_RegProd", oconexion);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Unidad_medida", obj.Unidad_medida);
                    cmd.Parameters.AddWithValue("Costo", obj.Costo);
                    cmd.Parameters.AddWithValue("Precio_unitario", obj.Precio_unitario);
                    cmd.Parameters.AddWithValue("Existencia", obj.Existencia);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("Punto_reorden", obj.Punto_reorden);
                    cmd.Parameters.AddWithValue("Depto", obj.oDepartamento.Clave);
                    cmd.Parameters.AddWithValue("IdAdminMod", obj.IdAdmin);
                    cmd.Parameters.AddWithValue("DescuentoProducto", obj.Descuento);
                    cmd.Parameters.AddWithValue("FechaRegistro", obj.FechaRegistro);
                    cmd.Parameters.AddWithValue("FechaModificacion", obj.FechaModificacion);
                    cmd.Parameters.AddWithValue("FechaInicio", obj.FechaInicio);
                    cmd.Parameters.AddWithValue("FechaFin", obj.FechaFin);

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
        
        public bool Editar(Producto obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ModifProd", oconexion);

                    cmd.Parameters.AddWithValue("Codigo_producto", obj.Codigo_producto);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Unidad_medida", obj.Unidad_medida);
                    cmd.Parameters.AddWithValue("Costo", obj.Costo);
                    cmd.Parameters.AddWithValue("Precio_unitario", obj.Precio_unitario);
                    cmd.Parameters.AddWithValue("Existencia", obj.Existencia);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("Punto_reorden", obj.Punto_reorden);
                    cmd.Parameters.AddWithValue("Depto", obj.oDepartamento.Clave);
                    cmd.Parameters.AddWithValue("IdAdminMod", obj.IdAdmin);
                    cmd.Parameters.AddWithValue("DescuentoProducto", obj.Descuento);
                    cmd.Parameters.AddWithValue("FechaModificacion", obj.FechaModificacion);
                    cmd.Parameters.AddWithValue("FechaInicio", obj.FechaInicio);
                    cmd.Parameters.AddWithValue("FechaFin", obj.FechaFin);

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
      
        public bool Eliminar(Producto obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ElimProd", oconexion);
                    cmd.Parameters.AddWithValue("Codigo_producto", obj.Codigo_producto);
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
    }
}
