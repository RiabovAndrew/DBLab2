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
    class Students : TableBase {
        private readonly SQLiteConnection _dbConnection;

        public Students() : this(null) { }
        public Students(DBConnector dbConnection) : this(dbConnection, "", 0, 0) { }

        public Students(DBConnector dbConnection, string name, int idSpecFac, int year) {
            _dbConnection = dbConnection?.DBConnection
                            ?? new DBConnector("Data Source=.\\history.db").DBConnection;
            Name = name;
            IdSpecFac = idSpecFac;
            Year = year;
        }

        public string Name { get; set; }
        public int IdSpecFac { get; set; }
        public int Year { get; set; }

        public int Id { get; set; }

        public bool InsertByParams(string name, int idSpecFac, int year) {
            var dbConnection = _dbConnection;
            if (!FindById(idSpecFac, new Spec_Fac(), dbConnection)) return false;
            var sql =
                $"insert into {GetType().Name.ToLower()} (name, id_spec_fac, year) values ('{name}', {idSpecFac}, {year})";
            var command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();

            return true;
        }

        public void Insert() { InsertByParams(Name, IdSpecFac, Year); }

        public List<string> Read(string columnName) {
            var dbConnection = _dbConnection;
            var list = ReadByColumn(columnName, this, dbConnection);
            return list;
        }

        public void EditByID(int id, Students newElement) {
            var dbConnection = _dbConnection;
            var sql =
                $"update {GetType().Name.ToLower()} set name = '{newElement.Name}', id_spec_fac = '{newElement.IdSpecFac}',"
                + $"year = '{newElement.Year} where id = '{id}'";
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