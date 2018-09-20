using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AdminProducto.Models
{
    public class Producto
    {
        public int? productoID { get; set; }
        public string serie { get; set; }
        public decimal precio_compra { get; set; }
        public decimal precio_venta { get; set; }

        public int marcaID { get; set; }
        public string marca_nombre { get; set; }

        public int modeloID { get; set; }
        public string modelo_nombre { get; set; }

        public int categoriaID { get; set; }
        public string categoria_nombre { get; set; }

        public int estadoID { get; set; }
        public string estado { get; set; }

        public int total_registros { get; set; }
        public int page { get; set; }
        public int index { get; set; }

        conexion conexion = new conexion();

        DataAccess data = new DataAccess();

        public List<Producto> Get2(int? productoID, string serie = null)
        {
            List<Producto> Resultado = new List<Producto>();


            //var con = cn.conectarData("Proc_Producto_Consulta", productoID, serie).AsEnumerable();

            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@productoID",productoID),
                new SqlParameter("@serie",serie)
            };

            try
            {
                Resultado = (from c in conexion.ConectarDatos("Proc_Producto_Consulta", param.ToArray()).AsEnumerable()
                             select new Producto
                             {
                                 productoID = c.Field<int>("productoID"),
                                 serie = c.Field<string>("serie"),
                                 precio_compra = c.Field<decimal>("precio_compra"),
                                 precio_venta = c.Field<decimal>("precio_venta"),
                                 marca_nombre = c.Field<string>("marca_nombre"),
                                 modelo_nombre = c.Field<string>("modelo_nombre"),
                                 categoria_nombre = c.Field<string>("categoria_nombre"),
                                 estado = c.Field<string>("estado"),
                             }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Resultado;
        }

        public Producto Post2(Producto prod)
        {
            Producto producto = new Producto();
            try
            {
                data.GetDataTable("Proc_Producto_Inserta",
                    prod.serie,
                    prod.precio_compra,
                    prod.precio_venta,
                    prod.marcaID,
                    prod.modeloID,
                    prod.categoriaID,
                    prod.estadoID);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return producto;
        }


        public IEnumerable<Producto> Get(int? productoID, string serie)
        {
            List<Producto> Resultado = new List<Producto>();

            DataTable dt = new DataTable();

            SqlConnection cn = new SqlConnection(conexion.conectar);
            cn.Open();

            SqlCommand cmd = cn.CreateCommand();

            cmd.CommandText = "Proc_Producto_Consulta";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@productoID", productoID);
            cmd.Parameters.AddWithValue("@serie", serie);
            cmd.Parameters.AddWithValue("@page", 1);
            cmd.Parameters.AddWithValue("@index", 10);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cn.Close();

            try
            {
                Resultado = (from c in dt.AsEnumerable()
                             select new Producto
                             {
                                 productoID = c.Field<int>("productoID"),
                                 serie = c.Field<string>("serie"),
                                 precio_compra = c.Field<decimal>("precio_compra"),
                                 precio_venta = c.Field<decimal>("precio_venta"),
                                 marca_nombre = c.Field<string>("marca_nombre"),
                                 modelo_nombre = c.Field<string>("modelo_nombre"),
                                 categoria_nombre = c.Field<string>("categoria_nombre"),
                                 estado = c.Field<string>("estado")
                             }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Resultado;
        }
    }
}