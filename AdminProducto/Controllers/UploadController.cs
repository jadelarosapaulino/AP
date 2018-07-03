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
using System.Web.Http.Results;
using System.Web.Mvc;

namespace AdminProducto.Controllers
{
    public class UploadController : ApiController
    {

        //public void Index()
        //{
        //    if (HttpContext.Current.Request.Files.AllKeys.Any())
        //    {
        //        // Get the uploaded image from the Files collection
        //        var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];

        //        var namepost = HttpContext.Current.Request.Params["name"];

        //        if (httpPostedFile != null)
        //        {
        //            namepost = namepost.Replace(" ", "");
        //            // Validate the uploaded image(optional)
        //            namepost = namepost + "." + httpPostedFile.ContentType.Replace("image/", "");
        //            // Get the complete file path
        //            var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), namepost);

        //            // Save the uploaded file to "UploadedFiles" folder
        //            httpPostedFile.SaveAs(fileSavePath);
        //        }
        //    }
        //}

        public JsonResult<string> Upload()
        {
            ImageResult imageResult = new ImageResult();

            string name = "";

            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Se obtienen los datos que vienen del formulario mediante la API
                var httpPostedFile = HttpContext.Current.Request.Files["image"];

                name = HttpContext.Current.Request.Params["marca"];

                name = name.Replace(" ", "");
                name = name + "." + httpPostedFile.ContentType.Replace("image/", "");

                if (httpPostedFile != null)
                {
                    if (httpPostedFile.ContentLength > 0)
                    {
                        // width + height will force size, care for distortion
                        //Exmaple: 
                        ImageUpload imageUpload = new ImageUpload { Width = 100, Height = 100 };

                        // Se cambia el alto y el ancho quedara proporcionadamente
                        //Ejemplo: ImageUpload imageUpload = new ImageUpload { Height= 600 };

                        // Se cambia en ancho y el alto quedara proporcionadamente 
                        //Ejemplo imageUpload = new ImageUpload { Width = 600 };

                        // rename, resize, and upload 
                        //return object that contains {bool Success,string ErrorMessage,string ImageName}
                        imageResult = imageUpload.RenameUploadFile(httpPostedFile, name);

                        if (imageResult.Success)
                        {
                            //TODO: write the filename to the db
                            Console.WriteLine(imageResult.ImageName);
                        }
                        else
                        {
                            //TODO: show view error
                            // use imageResult.ErrorMessage to show the error
                            //ViewBag.Error = imageResult.ErrorMessage;
                        }
                    }
                }
            }

            return Json("Datos guardados correctamente" + imageResult.ImageName);
        }
    }
}
