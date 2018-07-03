using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AdminProducto.Models
{
    public class Factura_Compra
    {
        public int? compraID { get; set; }
        public DateTime compra_fecha { get; set; }
        public string tracking { get; set; }
        public int articulos_cantidad { get; set; }
        public int compra_tipoID { get; set; }
        public int vendedorID { get; set; }
        public string compra_tipo { get; set; }
        public string vendedor { get; set; }

        conexion conexion = new conexion();

        // Realiza filtra Factura_Compra o traer listado de ellas
        #region Filtrar Factura_Compra
        public IEnumerable<Factura_Compra> Get(int? compraID, string tracking)
        {
            List<Factura_Compra> Resultado = new List<Factura_Compra>();

            DataTable dt = new DataTable();

            SqlConnection cn = new SqlConnection(conexion.conectar);
            cn.Open();

            SqlCommand cmd = cn.CreateCommand();

            cmd.CommandText = "Proc_Factura_Compra_Consulta";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@compraID", compraID);
            cmd.Parameters.AddWithValue("@tracking", tracking);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cn.Close();

            try
            {
                Resultado = (from c in dt.AsEnumerable()
                             select new Factura_Compra
                             {
                                 compraID = c.Field<int>("compraID"),
                                 compra_fecha = c.Field<DateTime>("compra_fecha"),
                                 tracking = c.Field<string>("tracking"),
                                 articulos_cantidad = c.Field<int>("articulos_cantidad"),
                                 compra_tipo = c.Field<string>("compra_tipo"),
                                 vendedor = c.Field<string>("vendedor")
                             }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Resultado;
        }
        #endregion

        // Inserta una nueva Factura_Compra
        #region Registrar Factura_Compra
        public void Set(Factura_Compra fC)
        {
            DataTable dt = new DataTable();

            SqlConnection cn = new SqlConnection(conexion.conectar);
            cn.Open();

            SqlCommand cmd = cn.CreateCommand();

            try
            {
                cmd.CommandText = "Pro_Factura_Compra_Inserta";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@compra_fecha", fC.compra_fecha);
                cmd.Parameters.AddWithValue("@tracking", fC.tracking);
                cmd.Parameters.AddWithValue("@articulos_cantidad", fC.articulos_cantidad);
                cmd.Parameters.AddWithValue("@compra_tipoID", fC.compra_tipoID);
                cmd.Parameters.AddWithValue("@vendedorID", fC.vendedorID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}