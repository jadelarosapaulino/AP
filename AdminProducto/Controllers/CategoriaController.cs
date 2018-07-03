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
    public class CategoriaController : ApiController
    {
        public JsonResult<List<Categoria>> Get(int? categoriaID)
        {
            List<Categoria> Resultado = new List<Categoria>();
            Categoria categoria = new Categoria();

            Resultado = categoria.GetCategoria(categoriaID).ToList();

            return Json(Resultado);
        }
      
        public void Post([FromBody]Categoria cat)
        {
            Categoria categoria = new Categoria();

            categoria.SetCategoria(cat);
        }
    }
}
