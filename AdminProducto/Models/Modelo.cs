using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AdminProducto.Models
{
    public class Modelo
    {
        public int? modeloID { get; set; }
        public string modelo_nombre { get; set; }
        public int? marcaID { get; set; }
        public string marca_nombre { get; set; }

        conexion conexion = new conexion();

        // Realiza filtra Marca o traer listado de ellas
        #region Filtrar Modelo
        public IEnumerable<Modelo> Get(int? modeloID = null, int? marcaID = null)
        {
            List<Modelo> Resultado = new List<Modelo>();

            DataTable dt = new DataTable();

            SqlConnection cn = new SqlConnection(conexion.conectar);
            cn.Open();

            SqlCommand cmd = cn.CreateCommand();

            cmd.CommandText = "Proc_Modelo_Consulta";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@modeloID", modeloID);
            cmd.Parameters.AddWithValue("@marcaID", marcaID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cn.Close();

            try
            {
                Resultado = (from c in dt.AsEnumerable()
                             select new Modelo
                             {
                                 modeloID = c.Field<int>("modeloID"),
                                 modelo_nombre = c.Field<string>("modelo_nombre"),
                                 marca_nombre = c.Field<string>("marca_nombre"),
                                 marcaID = c.Field<int>("marcaID"),
                             }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Resultado;
        }
        #endregion

        // Inserta una nueva Marca
        #region Registrar Modelo
        public void Set(Modelo mod)
        {
            DataTable dt = new DataTable();

            SqlConnection cn = new SqlConnection(conexion.conectar);
            cn.Open();

            SqlCommand cmd = cn.CreateCommand();

            try
            {
                cmd.CommandText = "Proc_Modelo_Inserta";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@modelo_nombre", mod.modelo_nombre);
                cmd.Parameters.AddWithValue("@marcaID", mod.marcaID);

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