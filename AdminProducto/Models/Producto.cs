using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminProducto.Models
{
    public class Producto
    {
        public int productoID { get; set; }
        public string serie { get; set; }
        public decimal precio_compra { get; set; }
        public decimal precio_venta { get; set; }
        public DateTime fecha_venta { get; set; }
        public int cantidad { get; set; }
        public decimal precio_envio { get; set; }
        public int marcaID { get; set; }
        public int modeloID { get; set; }
        public int categoriaID { get; set; }
        public int estadoID { get; set; }
        public int compraID { get; set; }
        public int colorID { get; set; }
    }
}