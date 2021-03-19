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
    class Teachers : TableBase {
        private readonly SQLiteConnection _dbConnection;

        public Teachers() : this(null) { }
        public Teachers(DBConnector dbConnection) : this(dbConnection, "", 0) { }

        public Teachers(DBConnector dbConnection, string name, int idFac) {
            _dbConnection = dbConnection?.DBConnection
                            ?? new DBConnector("Data Source=.\\history.db").DBConnection;
            Name = name;
            IdFac = idFac;
        }

        public string Name { get; set; }
        public int IdFac { get; set; }

        public int Id { get; set; }

        public bool InsertByParams(string name, int idFac) {
            var dbConnection = _dbConnection;
            if (!FindById(idFac, new Facs(), dbConnection)) return false;
            var sql = $"insert into {GetType().Name.ToLower()} (name, id_faculty) values ('{name}', {idFac})";
            var command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();

            return true;
        }

        public void Insert() { InsertByParams(Name, IdFac); }

        public List<string> Read(string columnName) {
            var dbConnection = _dbConnection;
            var list = ReadByColumn(columnName, this, dbConnection);
            return list;
        }

        public void EditByID(int id, Teachers newElement) {
            var dbConnection = _dbConnection;
            var sql =
                $"update {GetType().Name.ToLower()} set name = '{newElement.Name}', id_faculty = '{newElement.IdFac}' where id = '{id}'";
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