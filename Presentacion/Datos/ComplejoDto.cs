using Presentacion.Models;
using Presentacion.Request;
using Presentacion.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Presentacion.Datos
{
    public class ComplejoDto
    {
        public static ComplejoDto _instancia = null;

        private ComplejoDto() { }

        public static ComplejoDto Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new ComplejoDto();
                }
                return _instancia;
            }
        }

        static string cn = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        public List<Response_Complejo> ListarComplejo()
        {
            List<Response_Complejo> result = new List<Response_Complejo>();
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SP_Get_Complejo", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        result.Add(new Response_Complejo()
                        {
                            ID_Complejo = Convert.ToInt32(dr["ID_Complejo"].ToString()),
                            Nombre_Sede_Olimpica = dr["Nombre_Sede_Olimpica"].ToString(),
                            Descripcion_Tipo_Complejo = dr["Descripcion_Tipo_Complejo"].ToString(),
                            Nombre_Deporte = dr["Nombre_Deporte"].ToString(), 
                            Nombre_Complejo = dr["Nombre_Complejo"].ToString(),
                            Localizacion = dr["Localizacion"].ToString(),
                            Jefe_Organizacion = dr["Jefe_Organizacion"].ToString(),
                            Area_Total = Convert.ToDouble(dr["Area_Total"]),
                            Presupuesto = Convert.ToDouble(dr["Presupuesto"].ToString())
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

        public List<T_Deporte> ListarDeporteComplejo()
        {
            List<T_Deporte> result = new List<T_Deporte>();
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SP_Get_Deporte_Complejo", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        result.Add(new T_Deporte()
                        {
                            ID_Deporte = Convert.ToInt32(dr["ID_Deporte"]),
                            Nombres_Deporte = dr["Nombre_Deporte"].ToString()
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

        public bool RegistrarComplejoUnideportivo(Request_Complejo T_Deporte)
        {
            bool result = true;
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_Registrar_Complejo_Unideportivo", oConexion);

                    cmd.Parameters.AddWithValue("@ID_Sede_Olimpica", T_Deporte.ID_SEDE);
                    cmd.Parameters.AddWithValue("@ID_Deporte", T_Deporte.ID_Deporte);
                    cmd.Parameters.AddWithValue("@Nombre_Complejo", T_Deporte.Nombre_Complejo);
                    cmd.Parameters.AddWithValue("@Localizacion", T_Deporte.Localizacion);
                    cmd.Parameters.AddWithValue("@Jefe_Organizacion", T_Deporte.Jefe_Organizacion);
                    cmd.Parameters.AddWithValue("@Presupuesto", T_Deporte.Presupuesto);  
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

        public Response_ComplejoEdit ObtenerComplejo(int ID_Complejo)
        {
            Response_ComplejoEdit result = new Response_ComplejoEdit();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(cn))
                {
                    oConexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Obtener_Complejo", oConexion);
                    cmd.Parameters.AddWithValue("@ID_Complejo", ID_Complejo);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result.ID_Complejo = Convert.ToInt32(dr["ID_Complejo"]);
                            result.ID_Deporte = Convert.ToInt32(dr["ID_Deporte"]);
                            result.Nombre_Complejo = dr["Nombre_Complejo"].ToString();
                            result.Localizacion = dr["Localizacion"].ToString();
                            result.Jefe_Organizacion = dr["Jefe_Organizacion"].ToString();
                            result.Presupuesto = Convert.ToDouble(dr["Presupuesto"]);
                            result.Nombre_Deporte = dr["Nombre_Deporte"].ToString();
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return new Response_ComplejoEdit();
            }
        }

        public bool ModificarComplejoDeporte(Response_ComplejoEdit T_ComplejoDeporte)
        {
            bool result = true;
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_Actualizar_Complejo_Deporte", oConexion); 
                    cmd.Parameters.AddWithValue("@ID_Complejo", T_ComplejoDeporte.ID_Complejo);
                    cmd.Parameters.AddWithValue("@ID_Deporte", T_ComplejoDeporte.ID_Deporte);
                    cmd.Parameters.AddWithValue("@Nombre_Complejo", T_ComplejoDeporte.Nombre_Complejo);
                    cmd.Parameters.AddWithValue("@Localizacion", T_ComplejoDeporte.Localizacion);
                    cmd.Parameters.AddWithValue("@Jefe_Organizacion", T_ComplejoDeporte.Jefe_Organizacion);
                    cmd.Parameters.AddWithValue("@Presupuesto", T_ComplejoDeporte.Presupuesto);  
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

        public bool EliminarComplejoDeporte(int ID_Complejo)
        {
            bool result = true;
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_Eliminar_Complejo_Deporte", oConexion);
                    cmd.Parameters.AddWithValue("@ID_Complejo", ID_Complejo); 
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
