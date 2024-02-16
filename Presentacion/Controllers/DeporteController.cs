using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Presentacion.Datos;
using Presentacion.Models;
using Presentacion.Permisos;

namespace Presentacion.Controllers
{
    [ValidarSesion]
    public class DeporteController : Controller
    {
        // GET: Deporte
        public ActionResult Listar()
        { 
            List<Presentacion.Models.T_Deporte> ListaDeportes = DeporteDto.Instancia.ClientesDeporte();
            return View(ListaDeportes);
        }

        public ActionResult Guardar()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Guardar(T_Deporte oDeporte)
        { 
            var respuesta = DeporteDto.Instancia.RegistrarDeporte(oDeporte);

            if (respuesta) 
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }    
        }

        public ActionResult Editar(int ID_Deporte)
        {
            Presentacion.Models.T_Deporte oDeporte = DeporteDto.Instancia.ObtenerDeporte(ID_Deporte);
            return View(oDeporte);
        }

        [HttpPost]
        public ActionResult Editar(T_Deporte oDeporte)
        {
            bool respuesta = DeporteDto.Instancia.ModificarDeporte(oDeporte);
            
            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Eliminar(int ID_Deporte)
        {
            Presentacion.Models.T_Deporte oDeporte = DeporteDto.Instancia.ObtenerDeporte(ID_Deporte);
            return View(oDeporte);
        }

        [HttpPost]
        public ActionResult Eliminar(T_Deporte oDeporte)
        {
            bool respuesta = DeporteDto.Instancia.EliminarDeporte(oDeporte.ID_Deporte);

            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }
         
    }
}