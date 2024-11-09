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
    public class CD_Autorizacion
    {
        public List<Autorizacion> Listar(int idempleado)
        {
            List<Autorizacion> lista = new List<Autorizacion>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();


                    //query.AppendLine("SELECT a.IdPuesto,a.NombreMenu FROM Autorizacion a");
                    //query.AppendLine("inner join Puesto p on p.IdPuesto = a.IdPuesto");
                    //query.AppendLine("inner join Empleado e on e.IdPuesto = p.IdPuesto");
                    //query.AppendLine("WHERE e.IdEmpleado = @idempleado");

                    //SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);

                   

                    //cmd.CommandType = CommandType.Text;
                    SqlCommand cmd = new SqlCommand("SP_ListarAutorizacion", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idempleado", idempleado);
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Autorizacion()
                            {
                                oPuesto = new Puesto() {IdPuesto = Convert.ToInt32(dr["IdPuesto"])} ,
                                NombreMenu = dr["NombreMenu"].ToString(),
                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<Autorizacion>();
                }
            }
            return lista;
        }
    }
}
