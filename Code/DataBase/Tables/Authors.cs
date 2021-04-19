using System.Collections.Generic;
using System.Data.SQLite;
using WpfBDLab2.DataBase.DbConnector;
using WpfBDLab2.DataBase.Father;

namespace WpfBDLab2.DataBase.Tables {
    internal class Authors : TableBase {
        private readonly SQLiteConnection _dbConnection;

        public Authors() : this(null) { }

        public Authors(DBConnector dbConnection) : this(dbConnection, "1", 0) { }

        public Authors(DBConnector dbConnection, string name, int year) {
            _dbConnection = dbConnection?.DBConnection
                            ?? new DBConnector("Data Source=.\\history.db").DBConnection;
            Name = name;
            Year = year;
        }

        public string Name { get; set; }
        public int Year { get; set; }
        public int Id { get; set; }

        public void InsertByParams(string name, int year) {
            var dbConnection = _dbConnection;
            var sql = $"insert into {GetType().Name.ToLower()} (name, year_birth) values ('{name}', {year})";
            var command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
        }

        public void Insert() { InsertByParams(Name, Year); }

        public List<string> Read(string columnName) {
            var dbConnection = _dbConnection;
            var list = ReadByColumn(columnName, this, dbConnection);
            return list;
        }

        public void EditByID(int id, Authors author) {
            var dbConnection = _dbConnection;
            var sql =
                $"update {GetType().Name.ToLower()} set name = '{author.Name}', year_birth = '{author.Year}' where id = '{id}'";
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