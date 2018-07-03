using AdminProducto.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AdminProducto.Models
{
    public class Marca
    {
        public int marcaID { get; set; }
        public string marca_nombre { get; set; }
        public string img { get; set; }
        public int cantidad { get; set; }


        conexion conexion = new conexion();

        // Realiza filtra Marca o traer listado de ellas
        #region Filtrar Marca
        public IEnumerable<Marca> Get(int? marcaID)
        {
            List<Marca> Resultado = new List<Marca>();

            DataTable dt = new DataTable();

            SqlConnection cn = new SqlConnection(conexion.conectar);
            cn.Open();

            SqlCommand cmd = cn.CreateCommand();

            cmd.CommandText = "Proc_Marca_Consulta";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@marcaID", marcaID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cn.Close();

            try
            {
                Resultado = (from c in dt.AsEnumerable()
                             select new Marca
                             {
                                 marcaID = c.Field<int>("marcaID"),
                                 marca_nombre = c.Field<string>("marca_nombre"),
                                 img = c.Field<string>("img"),
                                 cantidad = c.Field<int>("cantidad")
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
        #region Registrar Marca
        public void Set(string marca_nombre, string img)
        {
            DataTable dt = new DataTable();

            SqlConnection cn = new SqlConnection(conexion.conectar);
            cn.Open();

            SqlCommand cmd = cn.CreateCommand();

            try
            {
                cmd.CommandText = "Proc_Marca_Inserta";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@marca_nombre", marca_nombre);
                cmd.Parameters.AddWithValue("@img", img);

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

        // Actualizar resgistro
        #region Actualizar Marca
        public void Put(int marcaID, string marca_nombre, string img)
        {
            DataTable dt = new DataTable();

            SqlConnection cn = new SqlConnection(conexion.conectar);
            cn.Open();

            SqlCommand cmd = cn.CreateCommand();

            try
            {
                cmd.CommandText = "Proc_Marca_Actualiza";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@marcaID", marcaID);
                cmd.Parameters.AddWithValue("@marca_nombre", marca_nombre);
                cmd.Parameters.AddWithValue("@img", img);

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

        // Inserta una nueva Marca
        #region Eliminar Marca
        public void Delete(int marcaID)
        {
            ImageUpload imageUpload = new ImageUpload();

            DataTable dt = new DataTable();

            SqlConnection cn = new SqlConnection(conexion.conectar);
            cn.Open();

            SqlCommand cmd = cn.CreateCommand();

            try
            {
                Marca marca = Get(marcaID).FirstOrDefault();
                imageUpload.eliminarImagen(marca.img);

                cmd.CommandText = "Proc_Marca_Eliminar";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@marcaID", marcaID);
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