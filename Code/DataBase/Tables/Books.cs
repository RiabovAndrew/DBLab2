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
    class Books : TableBase {
        private readonly SQLiteConnection _dbConnection;

        public Books() : this(null) { }
        public Books(DBConnector dbConnection) : this(dbConnection, 0, 0, 0, 1, 1) { }

        public Books(DBConnector dbConnection, int authorId, int cityId, int publHouseId, int year, int code) {
            _dbConnection = dbConnection?.DBConnection
                            ?? new DBConnector("Data Source=.\\history.db").DBConnection;
            Year = year;
            AuthorId = authorId;
            CityId = cityId;
            PublHouseId = publHouseId;
            Code = code;
        }

        public int AuthorId { get; set; }
        public int CityId { get; set; }
        public int PublHouseId { get; set; }
        public int Code { get; set; }
        public int Year { get; set; }
        public int Id { get; set; }

        public bool InsertByParams(int authorId, int cityId, int publHouseId, int year, int code) {
            var dbConnection = _dbConnection;
            if (!FindById(authorId, new Authors(), dbConnection) || !FindById(cityId, new Cities(), dbConnection)
                                                                 || !FindById(publHouseId, new Publ_Houses(),
                                                                     dbConnection
                                                                 )) return false;
            var sql =
                $"insert into {GetType().Name.ToLower()} (id_author, id_city, id_publ_house, year, code) values ({authorId}, {cityId}, {publHouseId}, {year}, {code})";
            var command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();

            return true;
        }

        public bool Insert() { return InsertByParams(AuthorId, CityId, PublHouseId, Year, Code); }

        public List<string> Read(string columnName) {
            var dbConnection = _dbConnection;
            var list = ReadByColumn(columnName, this, dbConnection);
            return list;
        }

        public void EditByID(int id, Books newElement) {
            var dbConnection = _dbConnection;
            var sql =
                $"update {GetType().Name.ToLower()} set id_author = '{newElement.AuthorId}', id_city = '{newElement.CityId}'"
                + $", id_publ_house = '{newElement.PublHouseId}', year = '{newElement.Year}', code = '{newElement.Code}' where id = '{id}'";
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