using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SQLServerCommon
{
    public class SQLServerCommon
    {
        public static bool IsDatabaseExists(string iConnectionString, string iDatabaseName)
        {
            string sqlCreateDBQuery;
            bool result = false;

            try
            {
                //iConnectionString
                //tmpConn = new SqlConnection("server=(local)\\SQLEXPRESS;Trusted_Connection=yes");

                var tmpConn = new SqlConnection(iConnectionString);
                sqlCreateDBQuery = string.Format("SELECT database_id FROM sys.databases WHERE Name = '{0}'", iDatabaseName);

                using (tmpConn)
                {
                    using (SqlCommand sqlCmd = new SqlCommand(sqlCreateDBQuery, tmpConn))
                    {
                        tmpConn.Open();
                        int databaseID = (int)sqlCmd.ExecuteScalar();    
                        tmpConn.Close();

                        result = (databaseID > 0);
                    }
                }
            } 
            catch (Exception ex)
            { 
                result = false;
            }

            return result;
        }

        public static void ExecuteNonQuery(string iSQLCommand, string iConnectionString)
        {
            SqlConnection sqlConn = new SqlConnection(iConnectionString);

            SqlCommand myCommand = new SqlCommand(iSQLCommand, sqlConn);

            try
            {
                sqlConn.Open();
                myCommand.ExecuteNonQuery();
            }   
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }
        }

        public static void ExecuteQuery()
        {

        }

        public static void CreateDatabase(string iDatabaseName, string iConnectionString)
        {
            String str;
            SqlConnection myConn = new SqlConnection ("Server=localhost;Integrated security=SSPI;database=master");

            str = "CREATE DATABASE MyDatabase ON PRIMARY " +
                "(NAME = MyDatabase_Data, " +
                "FILENAME = 'C:\\MyDatabaseData.mdf', " +
                "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
                "LOG ON (NAME = MyDatabase_Log, " +
                "FILENAME = 'C:\\MyDatabaseLog.ldf', " +
                "SIZE = 1MB, " +
                "MAXSIZE = 5MB, " +
                "FILEGROWTH = 10%)";
        }
    }
}
