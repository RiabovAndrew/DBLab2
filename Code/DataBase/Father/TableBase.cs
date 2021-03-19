using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBDLab2.DataBase.Father {
    internal class TableBase {
        public bool FindById(int id, object table, SQLiteConnection openedDBConnection) {
            var dbConnection = openedDBConnection;
            var sql = $"select id from {table.GetType().Name.ToLower()} where id = {id}";
            var command = new SQLiteCommand(sql, dbConnection);
            var reader = command.ExecuteReader();
            var result = reader.Read();
            return result;
        }

        public string FindByIdByColumn(int id, string columnName, object table, SQLiteConnection openedDBConnection)
        {
            var dbConnection = openedDBConnection;
            var sql = $"select '{columnName}' from {table.GetType().Name.ToLower()} where id = {id}";
            var command = new SQLiteCommand(sql, dbConnection);
            var reader = command.ExecuteReader();
            var result = reader[columnName].ToString();
            return result;
        }

        public bool DeleteById(int id, object table, SQLiteConnection openedDBConnection) {
            var dbConnection = openedDBConnection;
            var sql = $"delete from {table.GetType().Name.ToLower()} where id = {id}";
            var command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
            return !FindById(id, table, dbConnection);
        }

        public List<string> ReadByColumn(string columnName, object table, SQLiteConnection openedDBConnection) {
            var dbConnection = openedDBConnection;
            var sql = $"select {columnName} from {table.GetType().Name.ToLower()}";
            var command = new SQLiteCommand(sql, dbConnection);
            var reader = command.ExecuteReader();
            var list = new List<string>();
            while (reader.Read()) list.Add(reader[columnName].ToString());

            return list;
        }

        public List<string> ReadAllRowsFromTable(object table, SQLiteConnection openedDBConnection) {
            var dbConnection = openedDBConnection;
            var sql = $"select * from {table.GetType().Name.ToLower()}";
            var command = new SQLiteCommand(sql, dbConnection);
            var reader = command.ExecuteReader();
            var list = new List<string>();

            while (reader.Read()) {
                var str = GetColumnNames(table, dbConnection)
                    .Aggregate("", (current, columnName) => current + reader[columnName] + "\t");

                list.Add(str);
            }

            return list;
        }

        public List<string> GetColumnNames(object table, SQLiteConnection openedDBConnection) {
            var dbConnection = openedDBConnection;
            var tableCurrent = new DataTable(table.GetType().Name.ToLower());
            var adp = new SQLiteDataAdapter(new SQLiteCommand($"PRAGMA table_info({table.GetType().Name.ToLower()});",
                    openedDBConnection
                )
            );
            adp.Fill(tableCurrent);
            var list = new List<string>();
            for (var i = 0; i < tableCurrent.Rows.Count; i++) {
                list.Add(tableCurrent.Rows[i]["name"].ToString());
            }

            return list;
        }
    }
}