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
    class Spec_Fac : TableBase {
        private readonly SQLiteConnection _dbConnection;

        public Spec_Fac() : this(null) { }
        public Spec_Fac(DBConnector dbConnection) : this(dbConnection, 0, 0) { }

        public Spec_Fac(DBConnector dbConnection, int idSpec, int idFac) {
            _dbConnection = dbConnection?.DBConnection
                            ?? new DBConnector("Data Source=.\\history.db").DBConnection;
            IdSpec = idSpec;
            IdFac = idFac;
        }

        public int IdSpec { get; set; }

        public int IdFac { get; set; }
        public int Id { get; set; }

        public bool InsertByParams(int idSpec, int idFac) {
            var dbConnection = _dbConnection;
            if (!FindById(idSpec, new Specs(), dbConnection) || !FindById(idFac, new Facs(), dbConnection))
                return false;
            var sql = $"insert into {GetType().Name.ToLower()} (id_specs, id_fac) values ({idSpec}, {idFac})";
            var command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();

            return true;
        }

        public bool Insert() { return InsertByParams(IdSpec, IdFac); }

        public List<string> Read(string columnName) {
            var dbConnection = _dbConnection;
            var list = ReadByColumn(columnName, this, dbConnection);
            return list;
        }

        public bool EditByID(int id, Spec_Fac newElement) {
            var dbConnection = _dbConnection;
            if (!FindById(newElement.IdSpec, new Specs(), dbConnection) || !FindById(newElement.IdFac, new Facs(), dbConnection)) return false;
            var sql =
                $"update {GetType().Name.ToLower()} set id_specs = '{newElement.IdSpec}', id_facs = '{newElement.IdFac}' where id = '{id}'";
            var command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
            return true;
        }

        public bool DeleteByID(int id) {
            var dbConnection = _dbConnection;
            var resultDeleteById = DeleteById(id, this, dbConnection);
            return resultDeleteById;
        }
    }
}