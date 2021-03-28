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
    class History : TableBase {
        private readonly SQLiteConnection _dbConnection;

        public History() : this(null) { }
        public History(DBConnector dbConnection) : this(dbConnection, 0, 0, "", "", "") { }

        public History(DBConnector dbConnection, int idCard, int idBook, string dateGiven, string dateGivenUntill,
                       string worker) {
            _dbConnection = dbConnection?.DBConnection
                            ?? new DBConnector("Data Source=.\\history.db").DBConnection;
            IdCard = idCard;
            IdBook = idBook;
            DateGiven = dateGiven;
            DateGivenUntill = dateGivenUntill;
            Worker = worker;
        }

        public int IdCard { get; set; }
        public int IdBook { get; set; }
        public string DateGiven { get; set; }
        public string DateGivenUntill { get; set; }
        public string Worker { get; set; }

        public int Id { get; set; }

        public bool InsertByParams(int idCard, int idBook, string dateGiven, string dateGivenUntill, string worker) {
            var dbConnection = _dbConnection;
            if (!FindById(idCard, new Cards(), dbConnection) || !FindById(idBook, new Books(), dbConnection))
                return false;
            var sql =
                $"insert into {GetType().Name.ToLower()} (id_card, id_book, date_given, date_given_untill, worker)"
                + $" values ('{idCard}', {idBook}, '{dateGiven}', '{dateGivenUntill}', '{worker}')";
            var command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();

            return true;
        }

        public bool Insert() { return InsertByParams(IdCard, IdBook, DateGiven, DateGivenUntill, Worker); }

        public List<string> Read(string columnName) {
            var dbConnection = _dbConnection;
            var list = ReadByColumn(columnName, this, dbConnection);
            return list;
        }

        public List<string> ReadColumnNames() {
            var dbConnection = _dbConnection;
            var list = GetColumnNames(this, dbConnection);

            return list;
        }

        public bool EditByID(int id, History newElement) {
            var dbConnection = _dbConnection;
            if (!FindById(newElement.IdCard, new Cards(), dbConnection) || !FindById(newElement.IdBook, new Books(), dbConnection))
                return false;
            var sql =
                $"update {GetType().Name.ToLower()} set id_card = '{newElement.IdCard}', id_book = '{newElement.IdBook}',"
                + $" date_given = '{newElement.DateGiven}', date_given_untill = '{newElement.DateGivenUntill}', worker = '{newElement.Worker}' where id = '{id}'";
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