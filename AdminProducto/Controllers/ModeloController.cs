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
    public class ModeloController : ApiController
    {
        // GET: api/Modelo
        [Route("api/Modelo/GetAll")]
        public JsonResult<List<Modelo>> GetAll(int? modeloID, int? marcaID)
        {
            List<Modelo> Resultado = new List<Modelo>();

            Modelo modelo = new Modelo();

            Resultado = modelo.Get(modeloID, marcaID).ToList();

            return Json(Resultado);
        }

        // GET: api/Modelo
        [Route("api/Modelo/Get")]
        public JsonResult<List<Modelo>> Get(int? modeloID, int? marcaID)
        {
            List<Modelo> Resultado = new List<Modelo>();

            Modelo modelo = new Modelo();

            Resultado = modelo.Get(modeloID, marcaID).ToList();

            return Json(Resultado);
        }

        // GET: api/Modelo/5
        public string Get(int id)
        {       
            return "value";
        }

        // POST: api/Modelo
        [HttpPost]
        public void Post([FromBody]Modelo mod)
            {
            Modelo modelo = new Modelo();
            modelo.Set(mod);
        }

        // PUT: api/Modelo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Modelo/5
        public void Delete(int id)
        {
        }
    }
}
