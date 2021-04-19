using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBDLab2.DataBase.DbConnector;
using WpfBDLab2.DataBase.Father;

namespace WpfBDLab2.DataBase.Tables
{
    class Cities : TableBase
    {
        private readonly SQLiteConnection _dbConnection;

        public Cities() : this(null) { }
        public Cities(DBConnector dbConnection) : this(dbConnection, "1") { }

        public Cities(DBConnector dbConnection, string name) {
            _dbConnection = dbConnection?.DBConnection
                            ?? new DBConnector("Data Source=.\\history.db").DBConnection;
            Name = name;
        }

        public string Name { get; set; }
        public int Id { get; set; }

        public void InsertByParams(string name) {
            var dbConnection = _dbConnection;
            var sql = $"insert into {GetType().Name.ToLower()} (city_name) values ('{name}')";
            var command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
        }

        public void Insert() { InsertByParams(Name); }

        public List<string> Read(string columnName) {
            var dbConnection = _dbConnection;
            var list = ReadByColumn(columnName, this, dbConnection);
            return list;
        }

        public void EditByID(int id, Cities newElement) {
            var dbConnection = _dbConnection;
            var sql = $"update {GetType().Name.ToLower()} set city_name = '{newElement.Name}' where id = '{id}'";
            var command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
        }

        public bool DeleteByID(int id) {
            var dbConnection = _dbConnection;
            var resultDeleteById = DeleteById(id, this, dbConnection);
            return resultDeleteById;
        }
    }
}