using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AdminProducto.Models
{
    public class DataAccess
    {
        private String Server = ConfigurationManager.AppSettings["Server"];
        private String DataBase = ConfigurationManager.AppSettings["DataBase"];
        private String DbUser = ConfigurationManager.AppSettings["DbUser"];
        private String DbUserPass = ConfigurationManager.AppSettings["DbUserPass"];
        private String _conexion = ConfigurationManager.ConnectionStrings["Connection"].ToString();
        private SqlConnection conn = new SqlConnection();
        private String _exMensaje = "Error de acceso a datos";

        /// <summary>
        /// Clase para acceder a las operaciones con la Base de Datos. <para>
        /// Para utilizar una conexión por defecto, cree un ConectionString en el proyecto principal con el nombre "Connection". </para>
        /// Utilice este Constructor solo si ha creado el ConnectionString llamado "Connection" en el archivo de configuración Principal.
        /// </summary>
        public DataAccess() { }

        /// <summary>
        /// Establece la cadena de conexión
        /// </summary>
        public String Conexion
        {
            get { return _conexion; }
            set { _conexion = value; }
        }

        /// <summary>
        /// Clase para acceder a las operaciones con la Base de Datos. <para>
        /// Utilice este Constructor solo si va a proporcionar la "Base de datos". </para>
        /// Los demás parámetros deben estar especificados en el archivo de configuración de Principal:
        /// <para>->    key="Server" value="server_name", </para>
        ///       ->    key="DbUser" value="user_name", 
        /// <para>->    key="DbUserPass" value="user_password".</para>
        /// </summary>
        /// <param name="db">Base de datos a utilizar en la conexión.</param>
        public DataAccess(String db)
        {
            Conexion = ("Data Source=" + Server + ";Initial Catalog=" + db + "; User ID" + DbUser + "; Password=" + DbUserPass);
        }

        /// <summary>
        /// Clase para acceder a las operaciones con la Base de Datos. <para>
        /// Utilice este Constructor solo si va a proporcionar el "Servidor" y la "Base de datos".</para>
        /// Los demás parámetros deben estar especificados en el archivo de configuración de Principal:
        /// <para>->    key="DbUser" value="user_name", </para>
        ///       ->    key="DbUserPass" value="user_password".
        /// </summary>
        /// <param name="server">Servidor a utilizar para la conexión.</param>
        /// <param name="db">Base de datos a utilizar en la conexión.</param>
        public DataAccess(String server, String db)
        {
            Conexion = ("Data Source=" + server + ";Initial Catalog=" + db + "; User ID" + DbUser + "; Password=" + DbUserPass);
        }

        /// <summary>
        /// Clase para acceder a las operaciones con la Base de Datos. <para>
        /// Utilice este Constructor solo si va a proporcionar el "Servidor", "Base de datos", "Usuario" y "Password".</para>
        /// </summary>
        /// <param name="server">Servidor a utilizar para la conexión.</param>
        /// <param name="db">Base de datos a utilizar en la conexión.</param>
        /// <param name="user">Usuario a utilizar en la conexión.</param>
        /// <param name="password">Password a utilizar en la conexión.</param>
        public DataAccess(String server, String db, String user, String password)
        {
            string conectar = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
            //Conexion = ("Server=" + server + ";Database=" + db + ";User Id=" + user + ";Password=" + password);
        }

        /// <summary>
        /// Convierte un objeto (clase) a tipo Dictionary&lt;string, object&gt;
        /// </summary>
        /// <param name="clase">Objeto a convertir</param>
        /// <returns>Dictionary&lt;string, object&gt;</returns>
        public Dictionary<string, object> SetearClase(Object clase)
        {
            return clase.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetValue(this, null));
        }

        /// <summary>
        /// Asigna al SqlParameterCollection (parametros) los valores de object (clase)
        /// </summary>
        /// <param name="Parameters">Colección de parámetros a asignarle los valores</param>
        /// <param name="Class">Objeto con los valores a asignar a la colección de parámetros</param>
        private void SetearParametros(SqlParameterCollection Parameters, Object Class, object[] parametros)
        {
            List<SqlParameter> cmdParams = Parameters.Cast<SqlParameter>().Where(
                    x => x.Direction == ParameterDirection.Input || x.Direction == ParameterDirection.InputOutput).ToList();

            Dictionary<string, object> dic = null;

            if (Class != null)
                dic = Class.GetType().GetProperties().ToDictionary(prop => prop.Name, prop => prop.GetValue(Class, null));

            int i = 0;
            int i2 = 0;

            foreach (SqlParameter p in cmdParams)
            {
                if (dic != null)
                {
                    if (dic.Where(x => x.Key == p.ParameterName.Remove(0, 1)).Count() > 0)
                        p.Value = dic.FirstOrDefault(x => x.Key == p.ParameterName.Remove(0, 1)).Value;
                    else if (parametros != null)
                    {
                        if (i2 < parametros.Count())
                        {
                            cmdParams[i].Value = parametros[i2];
                            i2++;
                        }
                    }
                }
                else
                {
                    if (parametros != null)
                    {
                        if (i2 < parametros.Count())
                        {
                            cmdParams[i].Value = parametros[i2];
                            i2++;
                        }
                    }
                }

                i++;
            }

        }

        /// <summary>
        /// Método para configurar el objeto SqlCommand
        /// </summary>
        /// <param name="procedure">Procedimiento Almacenado que utilizará el objecto SqlCommand</param>
        /// <returns>SqlCommand</returns>
        private SqlCommand SetCommand(string procedure)
        {
            conn = new SqlConnection(Conexion);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = procedure;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            SqlCommandBuilder.DeriveParameters(cmd);

            return cmd;
        }

        /// <summary>
        /// Realiza una consulta en la base de datos y devuelve un DataTable con los datos solicitados.
        /// </summary>
        /// <param name="procedure">Nombre del Store Procedure</param>
        /// <param name="Class">Objeto (clase) con los valores</param>
        /// <param name="parametros">Valores de los parámetros</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(String procedure, Object Class, params object[] parametros)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlCommand command = SetCommand(procedure);
                SetearParametros(command.Parameters, Class, parametros);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception(_exMensaje, ex);
            }
            finally
            { conn.Close(); }

            return dt;
        }

        /// <summary>
        /// Realiza una consulta en la base de datos y devuelve un DataSet con los datos solicitados.
        /// </summary>
        /// <param name="procedure">Nombre del Store Procedure</param>
        /// <param name="Class">Objeto (clase) con los valores</param>
        /// <param name="parametros">Valores de los parámetros</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(String procedure, Object Class, params object[] parametros)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlCommand command = SetCommand(procedure);
                SetearParametros(command.Parameters, Class, parametros);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw new Exception(_exMensaje, ex);
            }
            finally
            { conn.Close(); }

            return ds;
        }

        /// <summary>
        /// Ejecuta un ExecuteReader y devuelve un DataReader.
        /// </summary>
        /// <param name="procedure">Nombre del Store Procedure</param>
        /// <param name="Class">Objeto (clase) con los valores</param>
        /// <param name="parametros">Valores de los parámetros</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader GetDataReader(String procedure, Object Class, params object[] parametros)
        {
            SqlDataReader dr;

            try
            {
                SqlCommand command = SetCommand(procedure);
                SetearParametros(command.Parameters, Class, parametros);
                dr = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new Exception(_exMensaje, ex);
            }
            finally
            { conn.Close(); }

            return dr;
        }

        /// <summary>
        /// Ejecuta un ExceuteNonQuery y devuelve el SqlCommand.
        /// </summary>
        /// <param name="procedure">Nombre del Store Procedure</param>
        /// <param name="Class">Objeto (clase) con los valores</param>
        /// <param name="parametros">Valores de los parámetros</param>
        /// <returns>SqlCommand</returns>
        public SqlCommand ExecDataCommand(String procedure, Object Class, params object[] parametros)
        {
            SqlCommand command;

            try
            {
                command = SetCommand(procedure);
                SetearParametros(command.Parameters, Class, parametros);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(_exMensaje, ex);
            }
            finally
            { conn.Close(); }

            return command;
        }

    }
}