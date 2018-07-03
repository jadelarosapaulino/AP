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
    public class Factura_CompraController : ApiController
    {

        // GET: api/Factura_Compra
        public JsonResult<List<Factura_Compra>> GetAll()
        {
            List<Factura_Compra> Resultado = new List<Factura_Compra>();

            Factura_Compra compras = new Factura_Compra();

            Resultado = compras.Get(null, null).ToList();

            return Json(Resultado);
        }
        // GET: api/Factura_Compra
        public JsonResult<List<Factura_Compra>> Get(int? compraID, string tracking)
        {
            List<Factura_Compra> Resultado = new List<Factura_Compra>();

            Factura_Compra compras = new Factura_Compra();

            Resultado = compras.Get(compraID, tracking).ToList();

            return Json(Resultado);
        }

        // POST: api/Factura_Compra
        public JsonResult<string> Post([FromBody]Factura_Compra factura_Compra)
        {
            string msg = null;
            Factura_Compra compras = new Factura_Compra();

            try
            {
                compras = compras.Get(null, factura_Compra.tracking).FirstOrDefault();

                if(compras == null)
                {
                    compras.Set(factura_Compra);
                    msg = "Factura registrada correctamente.";
                }
                else
                {
                    
                }
                
                if (msg == null)
                {
                    
                }
            }
            catch
            {
                msg = "Error al guardar " + factura_Compra.tracking + ", verifique e intente nuevamente.";
            }

            return Json(msg);
        }

        // PUT: api/Factura_Compra/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Factura_Compra/5
        public void Delete(int id)
        {
        }
    }
}
