using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Interface.Interfaces;
using Presentacion.Models;

namespace Infrastructure
{
    public class DeporteRepository : IDeporte
    { 
        string cn = Conexion.Conexion.CN;
          
        public List<T_Deporte> ObtenerClientes()
        {
            List<T_Deporte> result = new List<T_Deporte>();
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SP_Get_Deporte", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        result.Add(new T_Deporte()
                        {
                            ID_Deporte = Convert.ToInt32(dr["ID_Deporte"].ToString()),
                            Nombres_Deporte = dr["Nombres_Deporte"].ToString(),
                            Descripcion_Deporte = dr["Descripcion_Deporte"].ToString(),
                            Equipo = dr["Equipo"].ToString(),  
                        });
                    }
                    dr.Close();

                    return result;
                }
                catch (Exception ex)
                {
                    result = null;
                    return result;
                }
            }
        }

        public bool RegistrarDeporte(T_Deporte T_Deporte)
        {
            bool result = true;
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_Registrar_Deporte", oConexion);
                    cmd.Parameters.AddWithValue("@NombreDeporte", T_Deporte.Nombres_Deporte);
                    cmd.Parameters.AddWithValue("@DescripcionDeporte", T_Deporte.Descripcion_Deporte);
                    cmd.Parameters.AddWithValue("@EquipoDeporte", T_Deporte.Equipo);  
                    cmd.Parameters.Add("@Inserted", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open(); 
                    cmd.ExecuteNonQuery();

                    result = Convert.ToBoolean(cmd.Parameters["@Inserted"].Value); 
                }
                catch (Exception ex)
                {
                    result = false;
                } 
            } 
            return result;
        }

        public bool ModificarDeporte(T_Deporte T_Deporte)
        {
            bool result = true;
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_Actualizar_Deporte", oConexion);
                    cmd.Parameters.AddWithValue("@NombreDeporte", T_Deporte.Nombres_Deporte);
                    cmd.Parameters.AddWithValue("@DescripcionDeporte", T_Deporte.Descripcion_Deporte);
                    cmd.Parameters.AddWithValue("@EquipoDeporte", T_Deporte.Equipo);
                    cmd.Parameters.Add("@Updated", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open(); 
                    cmd.ExecuteNonQuery();

                    result = Convert.ToBoolean(cmd.Parameters["@Updated"].Value);
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }
            return result;
        }

        public bool EliminarDeporte(T_Deporte T_Deporte)
        {
            bool result = true;
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_Eliminar_Deporte", oConexion);
                    cmd.Parameters.AddWithValue("@ID_Deporte", T_Deporte.ID_Deporte); 
                    cmd.Parameters.Add("@Deleted", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open(); 
                    cmd.ExecuteNonQuery();

                    result = Convert.ToBoolean(cmd.Parameters["@Deleted"].Value);
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }
            return result;
        }
    }
}
