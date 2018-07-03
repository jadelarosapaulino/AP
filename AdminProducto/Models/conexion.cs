using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AdminProducto.Models
{
    public class conexion
    {
        public string conectar = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
    }
}