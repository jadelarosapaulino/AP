using AdminProducto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Cors;

namespace AdminProducto.Controllers
{
    public class EstadoController : ApiController
    {
        // GET: api/Estado
        [EnableCors(origins: "*", headers: "http://localhost:4200", methods: "*")]
        public JsonResult<List<Estado>> Get(int? estadoID)
        {
            List<Estado> Resultado = new List<Estado>();

            Estado estados = new Estado();

            Resultado = estados.GetEstado(estadoID).ToList();

            return Json(Resultado);
        }

        // GET: api/Estado/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Estado
        //[EnableCors(origins: "*", headers: "http://localhost:4200", methods: "*")]
        public void Post([FromBody]Estado estad)
        {
            Estado estados = new Estado();

            estados.SetEstado(estad.estado);
        }

        // PUT: api/Estado/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Estado/5
        public void Delete(int estadoID)
        {
            Estado estado = new Estado();

            estado.delete(estadoID);
        }
    }
}
