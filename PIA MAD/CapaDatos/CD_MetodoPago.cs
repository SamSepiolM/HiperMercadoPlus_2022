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
    public class CD_MetodoPago
    {
        public List<MetodoPago> Listar()
        {
            List<MetodoPago> lista = new List<MetodoPago>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    //query.AppendLine("SELECT IdMetodo, NombreMetodo FROM MetodoPago");
                    //SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    //cmd.CommandType = CommandType.Text;


                    SqlCommand cmd = new SqlCommand("SP_ListarMetodo", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;


                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new MetodoPago()
                            {
                                IdMetodo = Convert.ToInt32(dr["IdMetodo"]),
                                NombreMetodo = dr["NombreMetodo"].ToString(),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<MetodoPago>();
                }
            }
            return lista;
        }

        public List<MetodoPago> ListarFolio(string folio)
        {
            List<MetodoPago> lista = new List<MetodoPago>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT IdMetodo, NombreMetodo, MontoPago, IdVenta FROM ListarFolioMetodoPago");
                    //query.AppendLine("SELECT MP.IdMetodo, MP.NombreMetodo, DMP.MontoPago FROM DetalleMetodoPago DMP");
                    //query.AppendLine("INNER JOIN MetodoPago MP ON DMP.IdMetodo = MP.IdMetodo");
                    //query.AppendLine("INNER JOIN VENTA V ON V.IdVenta = DMP.IdVentaM");
                    query.AppendLine("WHERE IdVenta=" + folio);
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new MetodoPago()
                            {
                                IdMetodo = Convert.ToInt32(dr["IdMetodo"]),
                                NombreMetodo = dr["NombreMetodo"].ToString(),
                                MontoPago= Convert.ToDecimal(dr["MontoPago"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<MetodoPago>();
                }
            }
            return lista;
        }

        public int Registrar(MetodoPago obj, out string Mensaje)
        {
            int idProductogenerado = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_RegMetodoPago", oconexion);
                   
                    cmd.Parameters.AddWithValue("IdMetodo", obj.IdMetodo);
                    cmd.Parameters.AddWithValue("MontoPago", obj.MontoPago);
                    cmd.Parameters.AddWithValue("IdVentaM", obj.IdVentaM);


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

        public void ActualizarMetodo(MetodoPago obj, out string Mensaje)
        {
            int idProductogenerado = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ModifMetodoPago", oconexion);

                    cmd.Parameters.AddWithValue("IdVentaM", obj.IdVentaM);


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
            return ;
        }
    }
}