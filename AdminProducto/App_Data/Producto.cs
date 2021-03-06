//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdminProducto.App_Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            this.Color = new HashSet<Color>();
            this.Factura = new HashSet<Factura>();
        }
    
        public int productoID { get; set; }
        public string serie { get; set; }
        public decimal precio_compra { get; set; }
        public Nullable<decimal> precio_venta { get; set; }
        public Nullable<int> marcaID { get; set; }
        public Nullable<int> modeloID { get; set; }
        public Nullable<int> categoriaID { get; set; }
        public Nullable<int> estadoID { get; set; }
        public string activo { get; set; }
        public Nullable<System.DateTime> fecha_ingreso { get; set; }
        public Nullable<System.DateTime> ultima_actualizacion { get; set; }
    
        public virtual Categoria Categoria { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual Marca Marca { get; set; }
        public virtual Modelo Modelo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Color> Color { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Factura> Factura { get; set; }
    }
}
