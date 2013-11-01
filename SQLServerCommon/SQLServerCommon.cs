﻿using System;
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

        public static void Insert(string iTableName, string iConnectionString, string[] iColumns, IDictionary<string, string> iParameters)
        {
            SqlConnection sqlConn = new SqlConnection(iConnectionString);

            string sqlCommand = String.Format("INSERT INTO {0} (", iTableName);
            foreach (string column in iColumns)
            {
                sqlCommand += string.Format("{0},",column);
            }
            sqlCommand = sqlCommand.Remove(sqlCommand.Length - 1, 1);
            sqlCommand += ")";

            sqlCommand += " VALUES (";
            foreach (var pair in iParameters)
            {
                sqlCommand += String.Format("{0},", pair.Key);
            }
            sqlCommand = sqlCommand.Remove(sqlCommand.Length - 1, 1);
            sqlCommand += ");";

            SqlCommand myCommand = new SqlCommand(sqlCommand, sqlConn);

            foreach (var parameter in iParameters)
            {
                myCommand.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }

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

        public static DataTable ExecuteQuery(string iSQLCommand, string iConnectionString)
        {
            DataTable results = new DataTable();

            using (SqlConnection conn = new SqlConnection(iConnectionString))
            using (SqlCommand command = new SqlCommand(iSQLCommand, conn))
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                dataAdapter.Fill(results);

            return results;
        }
    }
}
