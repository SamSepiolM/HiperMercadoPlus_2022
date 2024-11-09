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
    public class CD_Empleado
    {
        public List<Empleado>Listar() {
            List<Empleado> lista = new List<Empleado>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena)) {
                
                try {
                    StringBuilder query = new StringBuilder();
                    //query.AppendLine("SELECT E.IdEmpleado, E.Nombre, E.ApePaterno, E.ApeMaterno, E.Telefono, E.Correo, E.CURP, E.Contrasenia, E.NumNomina, E.Domicilio, E.Estado, P.IdPuesto, P.Nombre_Puesto FROM Empleado E");
                    //query.AppendLine("inner join Puesto P on P.IdPuesto = E.IdPuesto");

                    //SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    //cmd.CommandType = CommandType.Text;

                    SqlCommand cmd = new SqlCommand("SP_ListarEmpleado", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        while (dr.Read()) {
                            lista.Add(new Empleado()
                            {
                                IdEmpleado = Convert.ToInt32(dr["IdEmpleado"]),
                                Nombre = dr["Nombre"].ToString(),
                                ApePaterno = dr["ApePaterno"].ToString(),
                                ApeMaterno = dr["ApeMaterno"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                CURP = dr["CURP"].ToString(),
                                Contrasenia = dr["Contrasenia"].ToString(),
                                NumNomina = dr["NumNomina"].ToString(),
                                Domicilio = dr["Domicilio"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                oPuesto = new Puesto() { IdPuesto = Convert.ToInt32(dr["IdPuesto"]), Nombre_Puesto = dr["Nombre_Puesto"].ToString()}
                            });
                        }
                    }
                }

                catch (Exception ex){

                    lista = new List<Empleado>();
                }
            }
            return lista;
        }

        public List<Empleado>Login()
        {
            List<Empleado> lista = new List<Empleado>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {

                try
                {
                    StringBuilder query = new StringBuilder();
                    //query.AppendLine("SELECT E.IdEmpleado, E.Nombre, E.ApePaterno, E.ApeMaterno, E.Telefono, E.Correo, E.CURP, E.Contrasenia, E.NumNomina, E.Domicilio, E.Estado, P.IdPuesto, P.Nombre_Puesto FROM Empleado E");
                    //query.AppendLine("inner join Puesto P on P.IdPuesto = E.IdPuesto");
                    //query.AppendLine("where Estado=1");

                    // SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    //cmd.CommandType = CommandType.Text;

                    SqlCommand cmd = new SqlCommand("SP_Login", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Empleado()
                            {
                                IdEmpleado = Convert.ToInt32(dr["IdEmpleado"]),
                                Nombre = dr["Nombre"].ToString(),
                                ApePaterno = dr["ApePaterno"].ToString(),
                                ApeMaterno = dr["ApeMaterno"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                CURP = dr["CURP"].ToString(),
                                Contrasenia = dr["Contrasenia"].ToString(),
                                NumNomina = dr["NumNomina"].ToString(),
                                Domicilio = dr["Domicilio"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                oPuesto = new Puesto() { IdPuesto = Convert.ToInt32(dr["IdPuesto"]), Nombre_Puesto = dr["Nombre_Puesto"].ToString() }
                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<Empleado>();
                }
            }
            return lista;
        }

        public List<Empleado> ListarCajeros()
        {
            List<Empleado> lista = new List<Empleado>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {

                try
                {
                    StringBuilder query = new StringBuilder();
                    //query.AppendLine("SELECT E.IdEmpleado, E.Nombre, E.ApePaterno, E.ApeMaterno, E.Telefono, E.Correo, E.CURP, E.Contrasenia, E.NumNomina, E.Domicilio, E.Estado, P.IdPuesto, P.Nombre_Puesto FROM Empleado E");
                    //query.AppendLine("inner join Puesto P on P.IdPuesto = E.IdPuesto");
                    //query.AppendLine("WHERE P.IdPuesto=0");

                    //SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    //cmd.CommandType = CommandType.Text;

                    SqlCommand cmd = new SqlCommand("SP_ListarCajero", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Empleado()
                            {
                                IdEmpleado = Convert.ToInt32(dr["IdEmpleado"]),
                                Nombre = dr["Nombre"].ToString(),
                                ApePaterno = dr["ApePaterno"].ToString(),
                                ApeMaterno = dr["ApeMaterno"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                CURP = dr["CURP"].ToString(),
                                Contrasenia = dr["Contrasenia"].ToString(),
                                NumNomina = dr["NumNomina"].ToString(),
                                Domicilio = dr["Domicilio"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                oPuesto = new Puesto() { IdPuesto = Convert.ToInt32(dr["IdPuesto"]), Nombre_Puesto = dr["Nombre_Puesto"].ToString() }
                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<Empleado>();
                }
            }
            return lista;
        }

        public int Registrar(Empleado obj, out string Mensaje) {
            int idempleadogenerado = 0;
            Mensaje = String.Empty;
            try{
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_RegEmpleado", oconexion);
                    cmd.Parameters.AddWithValue("Contrasenia", obj.Contrasenia);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("ApePaterno", obj.ApePaterno);
                    cmd.Parameters.AddWithValue("ApeMaterno", obj.ApeMaterno);
                    cmd.Parameters.AddWithValue("CURP", obj.CURP);
                    cmd.Parameters.AddWithValue("NumNomina", obj.NumNomina);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Domicilio", obj.Domicilio);
                    cmd.Parameters.AddWithValue("IdPuesto", obj.oPuesto.IdPuesto);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);

                    cmd.Parameters.Add("IdEmpleadoResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    
                    cmd.ExecuteNonQuery();
                    idempleadogenerado = Convert.ToInt32(cmd.Parameters["IdEmpleadoResultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }

            catch (Exception ex){
                idempleadogenerado = 0;
                Mensaje = ex.Message;
            }
            return idempleadogenerado;
        }

        public bool Editar(Empleado obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EditEmpleado", oconexion);
                    cmd.Parameters.AddWithValue("IdEmpleado", obj.IdEmpleado);
                    cmd.Parameters.AddWithValue("Contrasenia", obj.Contrasenia);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("ApePaterno", obj.ApePaterno);
                    cmd.Parameters.AddWithValue("ApeMaterno", obj.ApeMaterno);
                    cmd.Parameters.AddWithValue("CURP", obj.CURP);
                    cmd.Parameters.AddWithValue("NumNomina", obj.NumNomina);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Domicilio", obj.Domicilio);
                    cmd.Parameters.AddWithValue("IdPuesto", obj.oPuesto.IdPuesto);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);

                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
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

        public bool Eliminar(Empleado obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ElimEmpleado", oconexion);
                    cmd.Parameters.AddWithValue("IdEmpleado", obj.IdEmpleado);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
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
