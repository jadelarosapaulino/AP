using AdminProducto.App_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdminProducto.Controllers
{
    public class UsuarioController : ApiController
    {
        ProductosEntities db = new ProductosEntities();

        // GET: api/Usuario
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Usuario/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Usuario
        public void Post([FromBody]Usuario user)
        {
            try
            {
                if (user.img == null)
                {
                    user.img = "No_img.jpg";
                }

                if (user.role == null)
                {
                    user.role = "User";
                }

                db.Proc_Usuarios_Insertar(user.nombre, user.correo, user.password, user.img, user.role);

                token(user.password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT: api/Usuario/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Usuario/5
        public void Delete(int id)
        {
        }

        public string token(string token)
        {
            //var tok = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string tok = Convert.ToBase64String(time.Concat(key).ToArray());

            return tok;
        }
    }
}
