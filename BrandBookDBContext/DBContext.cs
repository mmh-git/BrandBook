using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BrandBookDBContext
{
    public abstract class DBContext
    {
        //private property
        private SqlConnection fReaderConn;

        #region BrandBook DB Context

        // this fuction add parameter to SqlCommand object iterating through SqlParameter array object
        private void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        // this function prepare new SqlCommand object based on different arguments
        private void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                //connection.Open();
                int repeatcnt = 0;

                while (repeatcnt < 3)
                {
                    try
                    {
                        connection.Open();
                        repeatcnt = 3;
                    }
                    catch (SqlException ex)
                    {
                        //Elmah.ErrorSignal.FromCurrentContext();
                        int timeOutValue = 0;
                        if (repeatcnt < 2)
                            timeOutValue = IsRerunException(ex);
                        if (timeOutValue > 0)
                        {
                            System.Threading.Thread.Sleep(timeOutValue);
                            repeatcnt++;
                        }
                        else
                        {
                            throw;
                        }
                    }
                };
            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            command.Connection = connection;
            // ATTENTION!!! System.Configuration.ConfigurationManager.AppSettings should be used here to avoid stack overflow - this code called from SettingManager
            //command.CommandTimeout = Convert.ToInt32(SettingsManager.GetSetting("sqlTimeOut").ToString());
            command.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["sqlTimeOut"].ToString());

            // Set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            // If we were provided a transaction, assign it
            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            // Set the command type
            command.CommandType = commandType;

            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }

        // this function returns SqlDataReader object
        private SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText, int SmallTimeout, params SqlParameter[] commandParameters)
        {
            // Pass through the call to the private overload using a null transaction value and an externally owned connection
            if (connection == null) throw new ArgumentNullException("connection");

            bool mustCloseConnection = false;
            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);
                SqlDataReader dataReader = null;
                // Call ExecuteReader with the appropriate CommandBehavior
                if (SmallTimeout < 60)
                    cmd.CommandTimeout = SmallTimeout;
                int repeatcnt = 0;

                while (repeatcnt < 3)
                {
                    try
                    {
                        dataReader = cmd.ExecuteReader();
                        repeatcnt = 3;
                    }
                    catch (SqlException ex)
                    {
                        //Elmah.ErrorSignal.FromCurrentContext();
                        int timeOutValue = 0;
                        if (repeatcnt < 2)
                            timeOutValue = IsRerunException(ex);
                        if (SmallTimeout < 60)
                            timeOutValue = 0;
                        if (timeOutValue > 0)
                        {
                            System.Threading.Thread.Sleep(timeOutValue);
                            repeatcnt++;
                        }
                        else
                        {
                            throw;
                        }
                    }
                };
                return dataReader;
            }
            catch
            {
                if (mustCloseConnection)
                    if (connection != null)
                        connection.Close();
                throw;
            }
        }

        // this function returns DataSet object
        private DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

            // Create the DataAdapter & DataSet
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();

                // Fill the DataSet using default values for DataTable names, etc
                int repeatcnt = 0;

                while (repeatcnt < 3)
                {
                    try
                    {
                        da.Fill(ds);
                        repeatcnt = 3;
                    }
                    catch (SqlException ex)
                    {
                        //Elmah.ErrorSignal.FromCurrentContext();
                        int timeOutValue = 0;
                        if (repeatcnt < 2)
                            timeOutValue = IsRerunException(ex);
                        if (timeOutValue > 0)
                        {
                            System.Threading.Thread.Sleep(timeOutValue);
                            repeatcnt++;
                        }
                        else
                        {
                            throw;
                        }
                    }
                };

                // Detach the SqlParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                if (mustCloseConnection)
                    connection.Close();

                // Return the dataset
                return ds;
            }
        }

        // this function returns no result
        private int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

            // Finally, execute the command
            int retval = cmd.ExecuteNonQuery();

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return retval;
        }

        // this function return suggested timeout.
        private int IsRerunException(Exception ex)
        {
            int rslt = 0;
            string msg = ex.Message;
            if (msg.StartsWith("Timeout expired."))
            {
                rslt = 6000; // sql run timeout - min
            };
            if (msg.StartsWith("SQL Server does not exist or access denied"))
            {
                rslt = 15000;
            };
            if (msg.StartsWith("SHUTDOWN is in progress"))
            {
                rslt = 25000;
            };
            if (msg.StartsWith("General network error"))
            {
                rslt = 3000;
            };
            if ((rslt == 0) & (ex is SqlException))
            {
                SqlException sqlex = (SqlException)ex;
                for (int i = 0; i < sqlex.Errors.Count; i++)
                {
                    int errnum = sqlex.Errors[i].Number;
                    if ((errnum == 1205) | (errnum == 111205))
                    {
                        rslt = 300;
                    };
                }
            }
            return rslt;
        }

        // this function closes db connection if there invokes a data reader function
        private void CloseReaderConnection()
        {
            try
            {
                if (fReaderConn != null)
                {
                    fReaderConn.Close();
                    fReaderConn = null;
                }
            }
            catch (Exception e)
            {
                //Elmah.ErrorSignal.FromCurrentContext();
                EatException(e);
            };
        }

        #endregion NetTrack DB Context

        #region Execute DataReader

        public SqlDataReader ExecuteReader(string commandText)
        {
            CloseReaderConnection();
            //SqlConnection f_conn = new SqlConnection(ConfigurationManager.AppSettings["DefaultConnection"]);
            SqlConnection f_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlDataReader dr = ExecuteReader(f_conn, CommandType.StoredProcedure, commandText, 60, new SqlParameter("@sessionid", 0));//TODO-- DbSessionId
            fReaderConn = f_conn;
            return dr;
        }

        public SqlDataReader ExecuteReader(string commandText, SqlParameter[] commandParameters)
        {
            CloseReaderConnection();
            //SqlConnection f_conn = new SqlConnection(ConfigurationManager.AppSettings["DefaultConnection"]);
            SqlConnection f_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlDataReader dr = ExecuteReader(f_conn, CommandType.StoredProcedure, commandText, 60, commandParameters);
            fReaderConn = f_conn;
            return dr;
        }

        #endregion Execute DataReader

        #region Execute DataTable

        public DataTable ExecuteDataTable(string commandText, SqlParameter[] commandParameters)
        {
            DataTable dt = null;
            //SqlConnection f_conn = new SqlConnection(ConfigurationManager.AppSettings["DefaultConnection"]);
            SqlConnection f_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            DataSet ds = ExecuteDataset(f_conn, CommandType.StoredProcedure, commandText, commandParameters);
            f_conn.Close();

            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        #endregion Execute DataTable

        #region Execute DataSet

        public DataSet ExecuteDataSet(string commandText, SqlParameter[] commandParameters)
        {
            //SqlConnection f_conn = new SqlConnection(ConfigurationManager.AppSettings["DefaultConnection"]);
            SqlConnection f_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            DataSet ds = ExecuteDataset(f_conn, CommandType.StoredProcedure, commandText, commandParameters);
            f_conn.Close();

            return ds;
        }

        #endregion Execute DataSet

        #region Execute NoResult

        public int ExecuteNoResult(string commandText, SqlParameter[] commandParameters)
        {
            //SqlConnection f_conn = new SqlConnection(ConfigurationSettings.AppSettings["DefaultConnection"]);
            SqlConnection f_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            int rslt = ExecuteNonQuery(f_conn, CommandType.StoredProcedure, commandText, commandParameters);
            f_conn.Close();
            return rslt;
        }

        public int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlConnection f_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, f_conn, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

            // Finally, execute the command
            int retval = cmd.ExecuteNonQuery();

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            if (mustCloseConnection)
                f_conn.Close();
            return retval;
        }

        #endregion Execute NoResult

        #region Exception handling

        private int EatException(Exception ex)
        {
            return 0;
        }

        #endregion Exception handling
    }
}
