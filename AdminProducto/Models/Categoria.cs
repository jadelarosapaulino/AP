using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http.Results;

namespace AdminProducto.Models
{
    public class Categoria
    {
        public int? categoriaID { get; set; }
        public string categoria_nombre { get; set; }

        conexion conexion = new conexion();

        // Realiza filtra categoria o traer listado de ellas
        #region Filtrar Categoria
        public IEnumerable<Categoria> GetCategoria(int? categoriaID)
        {
            List<Categoria> Resultado = new List<Categoria>();

            DataTable dt = new DataTable();

            SqlConnection cn = new SqlConnection(conexion.conectar);
            cn.Open();

            SqlCommand cmd = cn.CreateCommand();

            cmd.CommandText = "Proc_Categoria_Consulta";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@categoriaID", categoriaID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cn.Close();

            try
            {
                Resultado = (from c in dt.AsEnumerable()
                             select new Categoria
                             {
                                 categoria_nombre = c.Field<string>("categoria_nombre"),
                                 categoriaID = c.Field<int>("categoriaID")
                             }).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Resultado;
        }
        #endregion

        // Inserta una nueva categoria
        #region Registrar Categoria
        public void SetCategoria(Categoria categoria)
        {
            DataTable dt = new DataTable();

            SqlConnection cn = new SqlConnection(conexion.conectar);
            cn.Open();

            SqlCommand cmd = cn.CreateCommand();

            try
            {
                cmd.CommandText = "Proc_Categoria_Inserta";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@categoria_nombre", categoria.categoria_nombre);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cn.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Actualizar Categoria
        public void UpdateCategoria(Categoria categoria)
        {
            DataTable dt = new DataTable();

            SqlConnection cn = new SqlConnection(conexion.conectar);
            cn.Open();

            SqlCommand cmd = cn.CreateCommand();

            try
            {
                cmd.CommandText = "Proc_Categoria_Actualiza";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@categoriaID", categoria.categoriaID);
                cmd.Parameters.AddWithValue("@categoria_nombre", categoria.categoria_nombre);

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