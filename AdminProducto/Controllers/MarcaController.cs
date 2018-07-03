using AdminProducto.Lib;
using AdminProducto.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace AdminProducto.Controllers
{
    public class MarcaController : ApiController
    {
        // GET: api/Marca
        public JsonResult<List<Marca>> GetAll()
        {
            List<Marca> Resultado = new List<Marca>();

            Marca marcas = new Marca();

            Resultado = marcas.Get(null).ToList();

            return Json(Resultado);
        }

        // GET: api/Marca/5
        public JsonResult<Marca> Get(int? marcaID)
        {
            Marca Resultado = new Marca();

            Marca marca = new Marca();

            Resultado = marca.Get(marcaID).FirstOrDefault();

            return Json(Resultado);
        }

        // POST: api/Marca
        public JsonResult<string> Post()
        {
            Marca marca = new Marca();

            var marcaID = HttpContext.Current.Request.Params["marcaID"];

            //if (HttpContext.Current.Request.Files.AllKeys.Length > 0)
            //{
            // Se obtienen los datos que vienen del formulario mediante la API
            marca.marcaID = Convert.ToInt32(HttpContext.Current.Request.Params["marcaID"]);            
            marca.marca_nombre = HttpContext.Current.Request.Params["marca"];
            var httpPostedFile = HttpContext.Current.Request.Files["img"];

            try
            {
                string img = "";

                if (httpPostedFile != null)
                {
                    img = subirIMG(httpPostedFile, marca.marca_nombre);

                    if (marca.marcaID > 0)
                    {
                        marca.Put(marca.marcaID, marca.marca_nombre, img);
                    }
                    else
                    {
                        marca.Set(marca.marca_nombre, img);
                    }
                }
                else
                {
                    img = marca.Get(marca.marcaID).FirstOrDefault().img;

                    marca.Put(marca.marcaID, marca.marca_nombre, img);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //}

            return Json(marca.marca_nombre + ": guardado correctamente.");
        }

        public string subirIMG(HttpPostedFile file, string name)
        {
            ImageResult imageResult = new ImageResult();

            if (file != null)
            {
                // width + height will force size, care for distortion
                //Exmaple: 
                //ImageUpload imageUpload = new ImageUpload { Width = 350, Height = 280 };

                // Se cambia el alto y el ancho quedara proporcionadamente
                //Ejemplo: ImageUpload imageUpload = new ImageUpload { Height= 600 };

                // Se cambia en ancho y el alto quedara proporcionadamente 
                ImageUpload imageUpload = new ImageUpload { Width = 350 };

                // rename, resize, and upload 
                //return object that contains {bool Success,string ErrorMessage,string ImageName}
                imageResult = imageUpload.RenameUploadFile(file, name);
            }

            return imageResult.ImageName;
        }

        // PUT: api/Marca/5
        //public void Put(int marcaID = 0)
        //{
        //    Marca marca = new Marca();

        //    if (HttpContext.Current.Request.Files.AllKeys.Any())
        //    {
        //        // Se obtienen los datos que vienen del formulario mediante la API


        //        marcaID = Convert.ToInt32(HttpContext.Current.Request.Params["marcaID"]);
        //        marca.marca_nombre = HttpContext.Current.Request.Params["marca"];
        //        var httpPostedFile = HttpContext.Current.Request.Files["img"];

        //        if (httpPostedFile != null && )
        //        {
        //            if (httpPostedFile.ContentLength > 0)
        //            {
        //                // width + height will force size, care for distortion
        //                //Exmaple: 
        //                //ImageUpload imageUpload = new ImageUpload { Width = 350, Height = 280 };

        //                // Se cambia el alto y el ancho quedara proporcionadamente
        //                //Ejemplo: ImageUpload imageUpload = new ImageUpload { Height= 600 };

        //                // Se cambia en ancho y el alto quedara proporcionadamente 
        //                ImageUpload imageUpload = new ImageUpload { Width = 350 };

        //                // rename, resize, and upload 
        //                //return object that contains {bool Success,string ErrorMessage,string ImageName}
        //                ImageResult imageResult = imageUpload.RenameUploadFile(httpPostedFile, marca.marca_nombre);

        //                if (imageResult.Success)
        //                {
        //                    //TODO: write the filename to the db
        //                    string img = imageResult.ImageName;

        //                    marca.Put(marcaID, marca.marca_nombre, img);
        //                }
        //                else
        //                {
        //                    //TODO: show view error
        //                    // use imageResult.ErrorMessage to show the error
        //                    //ViewBag.Error = imageResult.ErrorMessage;
        //                }
        //            }
        //        }
        //    }
        //}

        [Route("api/Marca/Delete")]
        // DELETE: api/Marca/5
        [EnableCors(origins: "*", headers: "http://localhost:4200", methods: "*")]
        public void Delete(int marcaID)
        {
            Marca marca = new Marca();

            marca.Delete(marcaID);
        }
    }
}
