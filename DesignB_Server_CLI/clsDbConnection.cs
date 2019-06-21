using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignB_Server_CLI
{
    class clsDbConnection
    {
        private static ConnectionStringSettings ConnectionStringSettings = ConfigurationManager.ConnectionStrings["DesignBDatabase"];
        private static DbProviderFactory ProviderFactory = DbProviderFactories.GetFactory(ConnectionStringSettings.ProviderName);
        private static string ConnectionStr = ConnectionStringSettings.ConnectionString;
        public static DataTable GetDataTable(string prSQL, Dictionary<string, Object> prPars) { using (DataTable lcDataTable = new DataTable("TheTable")) using (DbConnection lcDataConnection = ProviderFactory.CreateConnection()) using (DbCommand lcCommand = lcDataConnection.CreateCommand()) { lcDataConnection.ConnectionString = ConnectionStr; lcDataConnection.Open(); lcCommand.CommandText = prSQL; setPars(lcCommand, prPars); using (DbDataReader lcDataReader = lcCommand.ExecuteReader(CommandBehavior.CloseConnection)) lcDataTable.Load(lcDataReader); return lcDataTable; } }
        public static int Execute(string prSQL, Dictionary<string, Object> prPars) {
            using (DbConnection lcDataConnection = ProviderFactory.CreateConnection())
            using (DbCommand lcCommand = lcDataConnection.CreateCommand()) {
                lcDataConnection.ConnectionString = ConnectionStr;
                lcDataConnection.Open(); lcCommand.CommandText = prSQL;
                setPars(lcCommand, prPars);
                return lcCommand.ExecuteNonQuery();
            }
        }
        private static void setPars(DbCommand prCommand, Dictionary<string, Object> prPars)
        { // For most DBMS using @Name1, @Name2, @Name3 etc. 
            if (prPars != null) foreach (KeyValuePair<string, Object> lcItem in prPars) { DbParameter lcPar = ProviderFactory.CreateParameter(); lcPar.Value = lcItem.Value == null ? DBNull.Value : lcItem.Value; lcPar.ParameterName = '@' + lcItem.Key; prCommand.Parameters.Add(lcPar); } }

        public static List<string> CheckTables()
        {
            //Check to see if there is any tables
            DataTable lcResult = GetDataTable("use dbdesignb; Show Tables;", null);
            List<String> lcBrands = new List<String>();
            foreach (DataRow dr in lcResult.Rows)
                lcBrands.Add((string)dr[0]);
            return lcBrands;
        }

        public static int CreateDB()
        {
            //get the connection string from app.config and split it into an array
            string[] conArray = ConnectionStr.Split(';');
            string connStr ="";
            //create a new string without the database, so it can access one level up
            foreach(string i in conArray)
            {
                if(i != "Database=dbdesignb")
                {
                    connStr = connStr+ i + ";";
                }
            }
            using (DbConnection lcDataConnection = ProviderFactory.CreateConnection())
            using (DbCommand lcCommand = lcDataConnection.CreateCommand())
            {
                lcDataConnection.ConnectionString = connStr;
                lcDataConnection.Open();
                //Get the SQL creation file
                string lcSQLFile = "..\\..\\..\\DesignB-Database-mySQL\\Creation Database Script.sql";
                string[] prSQL = File.ReadAllLines(lcSQLFile);
                string lcSQL ="";
                foreach (string line in prSQL)
                {
                    //Convert file into a one long string to be sent to the database
                    lcSQL += line;
                }
                lcCommand.CommandText = lcSQL;     
                return lcCommand.ExecuteNonQuery();
            }
        }
    }
    }


