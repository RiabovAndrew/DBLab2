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
    class Specs : TableBase
    {
        private readonly SQLiteConnection _dbConnection;

        public Specs() : this(null) { }
        public Specs(DBConnector dbConnection) : this(dbConnection, "1", "", 0) { }

        public Specs(DBConnector dbConnection, string name, string letter, int code) {
            _dbConnection = dbConnection?.DBConnection
                            ?? new DBConnector("Data Source=.\\history.db").DBConnection;
            Name = name;
            Letter = letter;
            Code = code;
        }

        public string Name { get; set; }
        public string Letter { get; set; }

        public int Code { get; set; }
        public int Id { get; set; }

        public void InsertByParams(string name, string letter, int code) {
            var dbConnection = _dbConnection;
            var sql =
                $"insert into {GetType().Name.ToLower()} (name, letter, code) values ('{name}', '{letter}', {code})";
            var command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
        }

        public void Insert() { InsertByParams(Name, Letter, Code); }

        public List<string> Read(string columnName) {
            var dbConnection = _dbConnection;
            var list = ReadByColumn(columnName, this, dbConnection);
            return list;
        }

        public void EditByID(int id, Specs newElement) {
            var dbConnection = _dbConnection;
            var sql =
                $"update {GetType().Name.ToLower()} set name = '{newElement.Name}', letter = '{newElement.Letter}',"
                + $" code = '{newElement.Code}' where id = '{id}'";
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