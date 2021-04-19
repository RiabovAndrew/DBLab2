using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBDLab2.DataBase.DbConnector {
    public class DBConnector {
        public SQLiteConnection DBConnection { get; set; }

        public DBConnector() {
            DBConnection = new SQLiteConnection("Data Source=.\\history.db");
            DBConnection.Open();
        }

        public DBConnector(string connectionString) {
            DBConnection = new SQLiteConnection(connectionString);
            DBConnection.Open();
        }
    }
}