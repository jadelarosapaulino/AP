using AdminProducto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace AdminProducto.Controllers
{
    public class ColorController : ApiController
    {
        // GET: api/Color
        public JsonResult<List<Color>> Get(int? colorID)
        {
            List<Color> Resultado = new List<Color>();

            Color colores = new Color();

            Resultado = colores.GetColor(colorID).ToList();

            return Json(Resultado);
        }

        // POST: api/Color
        public JsonResult<string> Post([FromBody]Color color)
        {
            string msg = "";
            Color colores = new Color();

            try
            {
                colores.SetColor(color);
                msg = "La categoria " + color.color + " se registro correctamente.";
            }
            catch
            {
                msg = "Error al guardar " + color.color + ", verifique e intente nuevamente.";
            }

            return Json(msg);
        }

        // PUT: api/Color/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Color/5
        public void Delete(int id)
        {
        }
    }
}
