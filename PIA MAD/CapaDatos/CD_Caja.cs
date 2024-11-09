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
    public class CD_Caja
    {
        public List<Caja> Listar()
        {
            List<Caja> lista = new List<Caja>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    //StringBuilder query = new StringBuilder();
                    //query.AppendLine("SELECT Id, Num_caja, Estado FROM Caja");

                    //SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    //cmd.CommandType = CommandType.Text;

                    SqlCommand cmd = new SqlCommand("SP_ListarCajas", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Caja()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Num_caja = dr["Num_caja"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])

                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<Caja>();
                }
            }
            return lista;
        }

        
        public int Registrar(Caja obj, out string Mensaje)
        {
            int idCajagenerado = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_RegCaja", oconexion);

                    cmd.Parameters.AddWithValue("Num_caja", obj.Num_caja);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);

                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    idCajagenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }

            catch (Exception ex)
            {
                idCajagenerado = 0;
                Mensaje = ex.Message;
            }
            return idCajagenerado;
        }


      

        public bool Editar(Caja obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EditCaja", oconexion);
                    cmd.Parameters.AddWithValue("Id", obj.Id);
                    cmd.Parameters.AddWithValue("Num_caja", obj.Num_caja);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);

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

      
      public bool Eliminar(Caja obj, out string Mensaje)
      {
          bool respuesta = false;
          Mensaje = String.Empty;
          try
          {
              using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
              {

                  SqlCommand cmd = new SqlCommand("SP_ElimCaja", oconexion);
                  cmd.Parameters.AddWithValue("Id", obj.Id);
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
    }

}
