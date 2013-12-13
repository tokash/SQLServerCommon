using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SQLServerCommonTester
{
    class Program
    {
        //private static readonly string connStringInitial =  "Server=TOKASHYO-PC\\SQLEXPRESS;Integrated security=SSPI;database=master";
        //private static readonly string connString = "Server=TOKASHYO-PC\\SQLEXPRESS;Integrated security=SSPI;database=drugs";

        //private static readonly string sqlCommandCreateDB = "CREATE DATABASE Drugs ON PRIMARY " +
        //        "(NAME = Drugs, " +
        //        "FILENAME = 'D:\\Drugs.mdf', " +
        //        "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
        //        "LOG ON (NAME = Drugs_LOG, " +
        //        "FILENAME = 'D:\\Drugs.ldf', " +
        //        "SIZE = 1MB, " +
        //        "MAXSIZE = 100MB, " +
        //        "FILEGROWTH = 10%)";

        //private static readonly string drugsTableSchema = "CREATE TABLE Drugs (DrugID int IDENTITY(1,1), Name varchar(255) NOT NULL, PRIMARY KEY (DrugID))";
        //private static readonly string SideEffectsTableSchema = "CREATE TABLE SideEffects (SideEffectID int IDENTITY(1,1), Name varchar(255) NOT NULL, Description varchar(255), PRIMARY KEY (SideEffectID))";
        //private static readonly string ActiveIngredientsTableSchema = "CREATE TABLE ActiveIngredients (ActiveIngredientID int IDENTITY(1,1), Name varchar(255) NOT NULL, PRIMARY KEY (ActiveIngredientID))";
        //private static readonly string InactiveIngredientsTableSchema = "CREATE TABLE InactiveIngredients (InactiveIngredientID int IDENTITY(1,1), Name varchar(255) NOT NULL, PRIMARY KEY (InactiveIngredientID))";
        //private static readonly string Drugs_ActiveIngredientsTableSchema = "CREATE TABLE Drugs_ActiveIngredients (DrugID int FOREIGN KEY REFERENCES Drugs(DrugID), ActiveIngredientID int FOREIGN KEY REFERENCES ActiveIngredients(ActiveIngredientID))";
        //private static readonly string Drugs_InactiveIngredientsTableSchema = "CREATE TABLE Drugs_InactiveIngredients (DrugID int FOREIGN KEY REFERENCES Drugs(DrugID), InactiveIngredientID int FOREIGN KEY REFERENCES InactiveIngredients(InactiveIngredientID))";
        //private static readonly string Drugs_SideEffectsTableSchema = "CREATE TABLE Drugs_SideEffects (DrugID int FOREIGN KEY REFERENCES Drugs(DrugID), SideEffectID int FOREIGN KEY REFERENCES SideEffects(SideEffectID))";

        private static readonly string connStringInitial = "Server=TOKASHYO-PC\\SQLEXPRESS;User Id=sa;Password=tokash30;database=master";
        private static readonly string connString = "Server=TOKASHYO-PC\\SQLEXPRESS;User Id=sa;Password=tokash30;database=SiteDB";
        private static string dbName = "SiteDB";

        private static readonly string sqlCommandCreateDB = "CREATE DATABASE " + dbName + " ON PRIMARY " +
                "(NAME = " + dbName + ", " +
                "FILENAME = 'D:\\" + dbName + ".mdf', " +
                "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
                "LOG ON (NAME = " + dbName + "_LOG, " +
                "FILENAME = 'D:\\" + dbName + ".ldf', " +
                "SIZE = 1MB, " +
                "MAXSIZE = 100MB, " +
                "FILEGROWTH = 10%)";

        private static readonly string siteStateTableSchema = "CREATE TABLE SiteState (UserID varchar(30) NOT NULL , Page int NOT NULL, IsVisited BIT NOT NULL, PRIMARY KEY (UserID))";
        private static readonly string[] siteStateTableColumns = { "UserID", "Page", "IsVisited" };

        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();

            if (SQLServerCommon.SQLServerCommon.IsTableExists(connString, dbName, "SiteState"))
            {
                Console.WriteLine(string.Format("Table {0} exists", "SiteState"));
            }
            else
            {
                Console.WriteLine(string.Format("Table {0} doesn't exists", "SiteState"));
            }

            //Dictionary<string, string> parameters = new Dictionary<string, string>();

            //parameters.Add(String.Format("@{0}", siteStateTableColumns[0]), "'abajdsdk12'");
            //parameters.Add(String.Format("@{0}", siteStateTableColumns[1]), "2");
            //parameters.Add(String.Format("@{0}", siteStateTableColumns[2]), "0");

            //SQLServerCommon.SQLServerCommon.Update("SiteState", connString, siteStateTableColumns, parameters);

            ////Create DB
            //if (!SQLServerCommon.SQLServerCommon.IsDatabaseExists(connStringInitial, "Drugs"))
            //{
            //    SQLServerCommon.SQLServerCommon.ExecuteNonQuery(sqlCommandCreateDB, connStringInitial);

            //    //Create tables upon DB creation
            //    SQLServerCommon.SQLServerCommon.ExecuteNonQuery(drugsTableSchema, connString);
            //    SQLServerCommon.SQLServerCommon.ExecuteNonQuery(SideEffectsTableSchema, connString);
            //    SQLServerCommon.SQLServerCommon.ExecuteNonQuery(ActiveIngredientsTableSchema, connString);
            //    SQLServerCommon.SQLServerCommon.ExecuteNonQuery(InactiveIngredientsTableSchema, connString);
            //    SQLServerCommon.SQLServerCommon.ExecuteNonQuery(Drugs_ActiveIngredientsTableSchema, connString);
            //    SQLServerCommon.SQLServerCommon.ExecuteNonQuery(Drugs_InactiveIngredientsTableSchema, connString);
            //    SQLServerCommon.SQLServerCommon.ExecuteNonQuery(Drugs_SideEffectsTableSchema, connString);
            //}
            //stopWatch.Stop();

            Console.WriteLine(String.Format("Time Elapsed for creating DB & tables: {0}", stopWatch.Elapsed));
        }
    }
}
