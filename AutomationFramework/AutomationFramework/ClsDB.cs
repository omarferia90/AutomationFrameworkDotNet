using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework
{
    public class ClsDB
    {
        #pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        private OracleConnection conn;
        #pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        private static string strConnection;

        /// <summary>
        /// Returns the string connection for based on the parameters provided
        /// </summary>
        /// <param name="strHost"></param>
        /// <param name="strPort"></param>
        /// <param name="strService"></param>
        /// <param name="strUser"></param>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        public string GetConnectionString(string strHost, string strPort, string strService, string strUser, string strPassword)
        {
            strConnection = "Data Source=" +
                            "(DESCRIPTION =" + "" +
                                "(ADDRESS = " +
                                    "(PROTOCOL = TCP)" +
                                    "(HOST = " + strHost + ")" +
                                    "(PORT = " + strPort + "))" +
                            "(CONNECT_DATA = " +
                                "(SERVER = DEDICATED)" +
                                "(SERVICE_NAME = " + strService + ")));" +
                            "Password=" + strPassword + ";User ID=" + strUser + "";
            return strConnection;
        }

        /// <summary>
        /// Opens a connection for DB
        /// </summary>
        /// <param name="pstrConnection"></param>
        public void fnOpenConnection(string pstrConnection)
        {
            try
            {
                #pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                conn = new OracleConnection(pstrConnection);
                #pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("The connection cannot be opened: Error -> " + e.Message);
            }
        }

        /// <summary>
        /// Close a connection DB
        /// </summary>
        public void fnCloseConnection()
        {
            try
            {
                conn.Dispose();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("The connection cannot be closed: Error -> " + e.Message);
            }
        }

        /// <summary>
        /// Executes a query and  close the connection
        /// </summary>
        /// <param name="pstrQuery"></param>
        public void fnExecuteQuery(string pstrQuery)
        {
            try
            {
                #pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                OracleCommand cmd = new OracleCommand(pstrQuery, conn);
                #pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                cmd.ExecuteNonQuery();
                fnCloseConnection();
            }
            catch (Exception e)
            {
                fnCloseConnection();
                Console.WriteLine("The query cannot be executed: Error -> " + e.Message);
            }
        }

        /// <summary>
        /// Returns a data reader with teh result of a query
        /// </summary>
        /// <param name="pstrQuery"></param>
        /// <returns></returns>
        public DataTable fnDataReader(string pstrQuery)
        {
            try
            {
                #pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                OracleCommand cmd = new OracleCommand(pstrQuery, conn);
                #pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                DbDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                fnCloseConnection();
                return dt;
            }
            catch (Exception e)
            {
                fnCloseConnection();
                Console.WriteLine("The query cannot be executed: Error -> " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Returns a datatable with the results of a query
        /// </summary>
        /// <param name="pstrQuery"></param>
        /// <returns></returns>
        public DataTable fnDataSet(string pstrQuery)
        {
            try
            {
                #pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                OracleDataAdapter adapter = new OracleDataAdapter(pstrQuery, conn);
                #pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                #pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                OracleCommandBuilder builder = new OracleCommandBuilder(adapter);
                #pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                DataTable datatable = new DataTable();
                datatable = dataset.Tables[0];
                fnCloseConnection();
                return datatable;
            }
            catch (Exception e)
            {
                fnCloseConnection();
                Console.WriteLine("The data table cannot be created: Error -> " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Returns only one value of a query
        /// </summary>
        /// <param name="pstrQuery"></param>
        /// <returns></returns>
        public String fnGetSingleValue(string pstrQuery)
        {
            string strValue;
            try
            {
                #pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                using (var cmd = new OracleCommand(pstrQuery, conn))
                {
                    strValue = cmd.ExecuteScalar().ToString();
                    fnCloseConnection();
                }
                #pragma warning restore CS0618 // El tipo o el miembro están obsoletos
            }
            catch (Exception e)
            {
                fnCloseConnection();
                Console.WriteLine("The data table cannot be created: Error -> " + e.Message);
                strValue = "";
            }

            return strValue;
        }

    }
}
