using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Runtime.CompilerServices;

namespace WindowsUpdateAgent
{
    class UpdateDB
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        public UpdateDB()
        {

        }
        //public void CreateDB()
        //{
        //    string cs = @"Data Source=Updates.db;Version=3;New=False;Compress=True;";

        //    var con = new SQLiteConnection(cs);
        //    con.Open();

        //    var cmd = new SQLiteCommand(con);

        //    cmd.CommandText = "DROP TABLE IF EXISTS updates";
        //    cmd.ExecuteNonQuery();

        //    cmd.CommandText = @"CREATE TABLE updates(guid STRING PRIMARY KEY,
        //            aproval bool)";
        //    cmd.ExecuteNonQuery();
        //}
        //private void setDummy()
        //{
        //    ExecuteQuery("INSERT INTO updates(guid,approval) VALUES (1,TRUE)");
        //    ExecuteQuery("INSERT INTO updates(guid,approval) VALUES (2,FALSE)");
        //    ExecuteQuery("INSERT INTO updates(guid,approval) VALUES (3,TRUE)");
        //}

        private void SetConnection()
        {
            sql_con = new SQLiteConnection
                ("Data Source=Updates.db;Version=3;New=False;Compress=True;");
        }
        private void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }
 
        public void CreateDB()
        {
            ExecuteQuery("DROP TABLE IF EXISTS updates");
            ExecuteQuery("CREATE TABLE updates(guid STRING PRIMARY KEY,approval bool)");
            //setDummy(); //// testing
        }
        public bool QueryApproval(string UpdateGuid)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "SELECT approval FROM updates WHERE guid = '" + UpdateGuid +"'";
            DB = new SQLiteDataAdapter(CommandText, sql_con);

            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];

            sql_con.Close();
            //Grid.DataSource = DT;
            try
            {
                if (DS.Tables[0].Rows[0]["approval"].ToString() == "True")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }
        }



    }
}
