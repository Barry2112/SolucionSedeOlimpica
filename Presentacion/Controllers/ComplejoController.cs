using Presentacion.Datos;
using Presentacion.Models;
using Presentacion.Permisos;
using Presentacion.Request;
using Presentacion.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    [ValidarSesion]
    public class ComplejoController : Controller
    { 
        // GET: Complejo
        public ActionResult Listar()
        {
            List<Response_Complejo> ListaComplejos = ComplejoDto.Instancia.ListarComplejo();
            return View(ListaComplejos);
        }
         
        public ActionResult Guardar()
        {
            List<Presentacion.Models.T_Deporte> ListaComplejos = ComplejoDto.Instancia.ListarDeporteComplejo();
            return View(ListaComplejos);
        }

        [HttpPost]
        public ActionResult Guardar(Request_Complejo oComplejo)
        {
            var respuesta = ComplejoDto.Instancia.RegistrarComplejoUnideportivo(oComplejo);

            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Editar(int ID_Complejo)
        {
            Presentacion.Response.Response_ComplejoEdit oComplejo = ComplejoDto.Instancia.ObtenerComplejo(ID_Complejo);
            
            List<Presentacion.Models.T_Deporte> ListaComplejos = ComplejoDto.Instancia.ListarDeporteComplejo();
            oComplejo.ListaComplejos = ListaComplejos;

            return View(oComplejo);
        }

        [HttpPost]
        public ActionResult Editar(Response_ComplejoEdit oDeporte)
        {
            bool respuesta = ComplejoDto.Instancia.ModificarComplejoDeporte(oDeporte);

            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Eliminar(int ID_Complejo)
        {
            Presentacion.Response.Response_ComplejoEdit oComplejo = ComplejoDto.Instancia.ObtenerComplejo(ID_Complejo);

            List<Presentacion.Models.T_Deporte> ListaComplejos = ComplejoDto.Instancia.ListarDeporteComplejo();
            oComplejo.ListaComplejos = ListaComplejos;
            return View(oComplejo);
        }

        [HttpPost]
        public ActionResult Eliminar(T_Complejo oComplejo)
        {
            bool respuesta = ComplejoDto.Instancia.EliminarComplejoDeporte(oComplejo.ID_Complejo);

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