using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AdminProducto.Models
{
    public class Color
    {
        public int? colorID { get; set; }
        public string color { get; set; }
        public string codigo { get; set; }

        conexion cd = new conexion();

        conexion conexion = new conexion();

        // Realiza filtra color o traer listado de ellas
        #region Filtrar Color
        public IEnumerable<Color> GetColor(int? colorID)
        {
            List<Color> Resultado = new List<Color>();

            DataTable dt = new DataTable();

            SqlConnection cn = new SqlConnection(conexion.conectar);
            cn.Open();

            SqlCommand cmd = cn.CreateCommand();

            cmd.CommandText = "Proc_Color_Consulta";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@colorID", colorID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cn.Close();

            try
            {
                Resultado = (from c in dt.AsEnumerable()
                             select new Color
                             {
                                 colorID = c.Field<int>("colorID"),
                                 color = c.Field<string>("color"),
                                 codigo = c.Field<string>("codigo")
                             }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Resultado;
        }
        #endregion

        // Inserta una nueva color
        #region Registrar Color
        public void SetColor(Color color)
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@color", color.color),
                new SqlParameter("@codigo", color.codigo)
            };

            try
            {
                cd.ConectarDatos("Proc_Color_Inserta", param.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}