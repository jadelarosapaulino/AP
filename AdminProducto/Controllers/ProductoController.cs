using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using AdminProducto.App_Data;

namespace AdminProducto.Controllers
{
    public class ProductoController : ApiController
    {
        private ProductosEntities db = new ProductosEntities();

        // GET: api/Producto
        public JsonResult<List<Proc_Producto_Consultar_Todo_Result>> GetProducto()
        {
            var resultado = db.Proc_Producto_Consultar_Todo().ToList();
            return Json(resultado.ToList());
        }

        // GET: api/Producto/5
        [ResponseType(typeof(Producto))]
        public JsonResult<List<Proc_Producto_Consulta_Result>> GetProducto(int? productoID, string serie)
      {
            //Producto producto = new Producto(); //db.Producto.Find(productoID, serie);

            var resultado = db.Proc_Producto_Consulta(productoID, serie).ToList();

            return Json(resultado.ToList());
        }

        // PUT: api/Producto/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProducto(int id, Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != producto.productoID)
            {
                return BadRequest();
            }

            db.Entry(producto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Producto
        [ResponseType(typeof(Producto))]
        public IHttpActionResult PostProducto(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Producto.Add(producto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = producto.productoID }, producto);
        }

        // DELETE: api/Producto/5
        [ResponseType(typeof(Producto))]
        public IHttpActionResult DeleteProducto(int id)
        {
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return NotFound();
            }

            db.Producto.Remove(producto);
            db.SaveChanges();

            return Ok(producto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductoExists(int id)
        {
            return db.Producto.Count(e => e.productoID == id) > 0;
        }
    }
}