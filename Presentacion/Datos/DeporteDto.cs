using Presentacion.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Presentacion.Datos
{
    public class DeporteDto
    {
        public static DeporteDto _instancia = null;

        private DeporteDto() { }

        public static DeporteDto Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new DeporteDto();
                }
                return _instancia;
            }
        }

        static string cn = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        

        public List<T_Deporte> ClientesDeporte()
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
                            Nombres_Deporte = dr["Nombre_Deporte"].ToString(),
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

        public T_Deporte ObtenerDeporte(int ID_Deporte)
        {
            T_Deporte result = new T_Deporte();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(cn))
                {
                    oConexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Obtener_Deporte", oConexion);
                    cmd.Parameters.AddWithValue("@ID_Deporte", ID_Deporte);
                    cmd.CommandType = CommandType.StoredProcedure;
                     
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result.ID_Deporte = Convert.ToInt32(dr["ID_Deporte"]);
                            result.Nombres_Deporte = dr["Nombre_Deporte"].ToString();
                            result.Descripcion_Deporte = dr["Descripcion_Deporte"].ToString();
                            result.Equipo = dr["Equipo"].ToString();
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return new T_Deporte();
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
                    cmd.Parameters.AddWithValue("@ID_Deporte", T_Deporte.ID_Deporte);
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

        public bool EliminarDeporte(int ID_Deporte)
        {
            bool result = true;
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_Eliminar_Deporte", oConexion);
                    cmd.Parameters.AddWithValue("@ID_Deporte", ID_Deporte);
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