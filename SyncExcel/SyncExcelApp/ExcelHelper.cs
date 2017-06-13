using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace SyncExcelApp
{
    /// <summary>  
    /// Excel文件处理类  
    /// </summary>  
    public class ExcelHelper
    {
        private static string fileName = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\sample\中围下街.xls";

        private static OleDbConnection connection;
        /*
        public static OleDbConnection Connection
        {
            get
            {
                string connectionString = "";
                string fileType = System.IO.Path.GetExtension(fileName);
                if (string.IsNullOrEmpty(fileType)) return null;
                if (fileType == ".xls")
                {
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + fileName + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=2\"";
                }
                else
                {
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileName + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=2\"";
                }
                if (connection == null)
                {
                    connection = new OleDbConnection(connectionString);
                    connection.Open();
                }
                else if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                else if (connection.State == System.Data.ConnectionState.Broken)
                {
                    connection.Close();
                    connection.Open();
                }
                return connection;
            }
        }
        */

        public static void CloseConnection()
        {
            if (connection != null)
            {
                connection.Close();
                connection = null;
            }
        }

        public static void CreateConnection(string fileName)
        {
            string connectionString = "";
            string fileType = System.IO.Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(fileType))
                return;
            if (fileType == ".xls")
            {
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + fileName + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=2\"";
            }
            else
            {
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileName + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=2\"";
            }
            if (connection == null)
            {
                connection = new OleDbConnection(connectionString);
                //connection.Open();
            }
            else if (connection.State == System.Data.ConnectionState.Closed)
            {
                //connection.Open();
            }
            else if (connection.State == System.Data.ConnectionState.Broken)
            {
                connection.Close();
                //connection.Open();
            }
        }

        public static string GetExcelSheetName()
        {
            if (connection != null)
            {
                try
                {
                    connection.Open();
                    DataTable schemaTable = connection.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
                    return schemaTable.Rows[0][2].ToString().Trim();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            } else
            {
                return string.Empty;
            }
        }

        /// <summary>  
        /// 执行无参数的SQL语句  
        /// </summary>  
        /// <param name="sql">SQL语句</param>  
        /// <returns>返回受SQL语句影响的行数</returns>  
        public static int ExecuteCommand(string sql)
        {
            try
            {
                connection.Open();
                OleDbCommand cmd = new OleDbCommand(sql, connection);
                int result = cmd.ExecuteNonQuery();
                return result;
            }
            catch(OleDbException oledbex)
            {
                throw new Exception(oledbex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>  
        /// 执行有参数的SQL语句  
        /// </summary>  
        /// <param name="sql">SQL语句</param>  
        /// <param name="values">参数集合</param>  
        /// <returns>返回受SQL语句影响的行数</returns>  
        public static int ExecuteCommand(string sql, params OleDbParameter[] values)
        {
            try
            {
                connection.Open();
                OleDbCommand cmd = new OleDbCommand(sql, connection);
                cmd.Parameters.AddRange(values);
                int result = cmd.ExecuteNonQuery();
                return result;

            }
            catch (OleDbException oledbex)
            {
                throw new Exception(oledbex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>  
        /// 返回单个值无参数的SQL语句  
        /// </summary>  
        /// <param name="sql">SQL语句</param>  
        /// <returns>返回受SQL语句查询的行数</returns>  
        public static int GetScalar(string sql)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand(sql, connection);
                int result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (OleDbException oledbex)
            {
                throw new Exception(oledbex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>  
        /// 返回单个值有参数的SQL语句  
        /// </summary>  
        /// <param name="sql">SQL语句</param>  
        /// <param name="parameters">参数集合</param>  
        /// <returns>返回受SQL语句查询的行数</returns>  
        public static int GetScalar(string sql, params OleDbParameter[] parameters)
        {
            try
            {
                connection.Open();
                OleDbCommand cmd = new OleDbCommand(sql, connection);
                cmd.Parameters.AddRange(parameters);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                return result;
            }
            catch (OleDbException oledbex)
            {
                throw new Exception(oledbex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>  
        /// 执行查询无参数SQL语句  
        /// </summary>  
        /// <param name="sql">SQL语句</param>  
        /// <returns>返回数据集</returns>  
        public static DataSet GetReader(string sql)
        {
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                da.Fill(ds, "TempTable");
                return ds;
            }
            catch (OleDbException oledbex)
            {
                throw new Exception(oledbex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>  
        /// 执行查询有参数SQL语句  
        /// </summary>  
        /// <param name="sql">SQL语句</param>  
        /// <param name="parameters">参数集合</param>  
        /// <returns>返回数据集</returns>  
        public static DataSet GetReader(string sql, params OleDbParameter[] parameters)
        {
            try
            {
                connection.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(sql, connection);
                da.SelectCommand.Parameters.AddRange(parameters);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;
            }
            catch (OleDbException oledbex)
            {
                throw new Exception(oledbex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
