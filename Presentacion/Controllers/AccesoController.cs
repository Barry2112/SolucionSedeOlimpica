using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Presentacion.Models; 

namespace Presentacion.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        ///static string cadena = "Data Source=MG-L0DSALAZARP\\MSSQLSERVER01; Initial Catalog=BD_SEDE_OLIMPICA; Integrated Security = true";

        static string cadena = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString; 

        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(T_Usuario usuario)
        {
            bool registrado;
            string mensaje;

            if (usuario.Contrasena == usuario.ConfirmarContrasena)
            {
                usuario.Contrasena = Encriptartexto(usuario.Contrasena);
            }
            else
            {
                ViewData["Mensaje"] = "La contraseña ingresada no coincide";
                return View();

            }

            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_RegistrarUsuario", conn);

                    cmd.Parameters.AddWithValue("@Nombre_Usuario", usuario.Nombres);
                    cmd.Parameters.AddWithValue("@Apellido_Usuario", usuario.Apellidos);
                    cmd.Parameters.AddWithValue("@DNI_Usuario", usuario.DNI);
                    cmd.Parameters.AddWithValue("@Correo_Usuario", usuario.Correo);
                    cmd.Parameters.AddWithValue("@Contrasena_Usuario", usuario.Contrasena);
                    cmd.Parameters.Add("@Inserted", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    registrado = Convert.ToBoolean(cmd.Parameters["@Inserted"].Value);
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }

                ViewData["Mensaje"] = mensaje;
            }
            catch (Exception ex)
            {
                throw ex;
            }
             
            if (registrado)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                return View();
            }
        }

        public static string Encriptartexto(string texto)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding encoding = Encoding.UTF8;
                byte[] obj = hash.ComputeHash(encoding.GetBytes(texto));

                foreach (byte o in obj)
                {
                    sb.Append(o.ToString("x2"));
                }
                return sb.ToString();
            }

        }

        [HttpPost]
        public ActionResult Login(T_Usuario usuario)
        {
            usuario.Contrasena = Encriptartexto(usuario.Contrasena);

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("@Correo_Usuario", usuario.Correo);
                cmd.Parameters.AddWithValue("@Contrasena_Usuario", usuario.Contrasena);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                usuario.ID_Usuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }

            if (usuario.ID_Usuario != 0)
            {
                Session["usuario"] = usuario;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "usuario no encontrado";
                return View();
            }
        }
    }
}