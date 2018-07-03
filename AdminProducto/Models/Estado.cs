using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AdminProducto.Models
{
    public class Estado
    {
        public int estadoID { get; set; }
        public string estado { get; set; }

        conexion conexion = new conexion();

        // Realiza filtra estado o traer listado de ellos
        #region Filtrar Color
        public IEnumerable<Estado> GetEstado(int? estadoID)
        {
            List<Estado> Resultado = new List<Estado>();

            DataTable dt = new DataTable();

            SqlConnection cn = new SqlConnection(conexion.conectar);
            cn.Open();

            SqlCommand cmd = cn.CreateCommand();

            cmd.CommandText = "Proc_Estado_Consulta";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@estadoID", estadoID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cn.Close();

            try
            {
                Resultado = (from c in dt.AsEnumerable()
                             select new Estado
                             {
                                 estadoID = c.Field<int>("estadoID"),
                                 estado = c.Field<string>("estado")
                             }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Resultado;
        }
        #endregion

        // Inserta una nueva estado
        #region Registrar Color
        public void SetEstado(string estado)
        {
            DataTable dt = new DataTable();

            SqlConnection cn = new SqlConnection(conexion.conectar);
            cn.Open();

            SqlCommand cmd = cn.CreateCommand();

            try
            {
                cmd.CommandText = "Proc_Estado_Inserta";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@estado", estado);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void delete(int estadoID)
        {
            DataTable dt = new DataTable();

            SqlConnection cn = new SqlConnection(conexion.conectar);
            cn.Open();

            SqlCommand cmd = cn.CreateCommand();

            try
            {
                cmd.CommandText = "Proc_Estado_Eliminar";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@estadoID", estadoID);
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