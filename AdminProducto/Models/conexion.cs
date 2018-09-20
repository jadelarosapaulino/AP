using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AdminProducto.Models
{
    public class conexion
    {
        public string conectar = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

        public DataTable ConectarDatos(string sp, object[] param)
        {
            SqlConnection cn = new SqlConnection(conectar);
            DataTable dt = new DataTable();
            cn.Open();

            try
            {
                SqlCommand comm = new SqlCommand()
                {
                    CommandText = sp,
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 30
                };

                comm.Connection = cn;
                comm.Parameters.AddRange(param.ToArray());
                SqlDataAdapter da = new SqlDataAdapter(comm);

                da.Fill(dt);
                cn.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}