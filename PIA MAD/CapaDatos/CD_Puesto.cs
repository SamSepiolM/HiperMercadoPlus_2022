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
    public class CD_Puesto
    {
        public List<Puesto> Listar()
        {
            List<Puesto> lista = new List<Puesto>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    //query.AppendLine("SELECT IdPuesto, Nombre_Puesto FROM Puesto");

                    //SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);

                    SqlCommand cmd = new SqlCommand("SP_ListarPuesto", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Puesto()
                            {
                                IdPuesto = Convert.ToInt32(dr["IdPuesto"]), 
                                Nombre_Puesto = dr["Nombre_Puesto"].ToString()
                               
                            });
                        }
                    }
                }

                catch (Exception ex)
                {

                    lista = new List<Puesto>();
                }
            }
            return lista;
        }
    }
}
