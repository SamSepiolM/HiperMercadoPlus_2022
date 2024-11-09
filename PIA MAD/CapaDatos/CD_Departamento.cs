using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Departamento
    {
        public List<Departamento> Listar()
        {
            List<Departamento> lista = new List<Departamento>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    //query.AppendLine("SELECT Clave, Nombre, Descuento, DescActivo, Devolucion FROM Departamento");

                    //SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    //cmd.CommandType = CommandType.Text;

                    SqlCommand cmd = new SqlCommand("SP_ListarDepartamento", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Departamento()
                            {
                                Clave = Convert.ToInt32(dr["Clave"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descuento = Convert.ToInt32(dr["Descuento"]),
                                DescActivo = Convert.ToBoolean(dr["DescActivo"]),
                                Devolucion = Convert.ToBoolean(dr["Devolucion"])
                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<Departamento>();
                }
            }
            return lista;
        }

      
      public int Registrar(Departamento obj, out string Mensaje)
        {
            int idDepartamentogenerado = 0;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_RegDepto", oconexion);

                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descuento", obj.Descuento);
                    cmd.Parameters.AddWithValue("DescActivo", obj.DescActivo);
                    cmd.Parameters.AddWithValue("Devolucion", obj.Devolucion);

                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    idDepartamentogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }

            catch (Exception ex)
            {
                idDepartamentogenerado = 0;
                Mensaje = ex.Message;
            }
            return idDepartamentogenerado;
        }

      public bool Editar(Departamento obj, out string Mensaje)
      {
          bool respuesta = false;
          Mensaje = String.Empty;
          try
          {
              using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
              {

                  SqlCommand cmd = new SqlCommand("SP_EditDepto", oconexion);
                  cmd.Parameters.AddWithValue("Clave", obj.Clave);
                  cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                  cmd.Parameters.AddWithValue("Descuento", obj.Descuento);
                  cmd.Parameters.AddWithValue("DescActivo", obj.DescActivo);
                  cmd.Parameters.AddWithValue("Devolucion", obj.Devolucion);

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

      public bool Eliminar(Departamento obj, out string Mensaje)
      {
          bool respuesta = false;
          Mensaje = String.Empty;
          try
          {
              using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
              {

                  SqlCommand cmd = new SqlCommand("SP_ElimDepto", oconexion);
                  cmd.Parameters.AddWithValue("Clave", obj.Clave);
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
