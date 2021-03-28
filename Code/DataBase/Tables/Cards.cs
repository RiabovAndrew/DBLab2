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
    class Cards : TableBase {
        private readonly SQLiteConnection _dbConnection;

        public Cards() : this(null) { }
        public Cards(DBConnector dbConnection) : this(dbConnection, "", 0, 0, 0) { }

        public Cards(DBConnector dbConnection, string dateGiven, int code, int idStudent, int idTeacher) {
            _dbConnection = dbConnection?.DBConnection
                            ?? new DBConnector("Data Source=.\\history.db").DBConnection;
            DateGiven = dateGiven;
            Code = code;
            IdStudent = idStudent;
            IdTeacher = idTeacher;
        }

        public string DateGiven { get; set; }
        public int Code { get; set; }
        public int IdStudent { get; set; }
        public int IdTeacher { get; set; }

        public int Id { get; set; }

        public bool InsertByStudentParams(string dateGiven, int code, int idStudent) {
            var dbConnection = _dbConnection;
            if (!FindById(idStudent, new Students(), dbConnection)) return false;
            var sql =
                $"insert into {GetType().Name.ToLower()} (id_student, id_teacher, date_given, code) values ({idStudent}, 'null',"
                + $" '{dateGiven}', {code})";
            var command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();

            return true;
        }

        public bool InsertStudent() { return InsertByStudentParams(DateGiven, Code, IdStudent); }

        public bool InsertByTeacherParams(string dateGiven, int code, int idTeacher) {
            var dbConnection = _dbConnection;
            if (!FindById(idTeacher, new Teachers(), dbConnection)) return false;
            var sql =
                $"insert into {GetType().Name.ToLower()} (id_student, id_teacher, date_given, code) values ('null',"
                + $" {idTeacher}, '{dateGiven}', {code})";
            var command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();

            return true;
        }

        public bool InsertTeacher() { return InsertByTeacherParams(DateGiven, Code, IdTeacher); }

        public List<string> Read(string columnName) {
            var dbConnection = _dbConnection;
            var list = ReadByColumn(columnName, this, dbConnection);
            return list;
        }

        public void EditByID(int id, Cards newElement) {
            var dbConnection = _dbConnection;
            var sql =
                $"update {GetType().Name.ToLower()} set id_student = '{newElement.IdStudent}', id_teacher = '{newElement.IdTeacher}', "
                + $"date_given = '{newElement.DateGiven}', code = '{newElement.Code}' where id = '{id}'";
            var command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
        }

        public bool EditStudentById(int id, Cards newElement) {
            var dbConnection = _dbConnection;
            if (!FindById(newElement.IdStudent, new Students(), _dbConnection)) return false;
                var sql =
                $"update {GetType().Name.ToLower()} set id_student = '{newElement.IdStudent}', id_teacher = '{newElement.IdTeacher}', "
                + $"date_given = '{newElement.DateGiven}', code = '{newElement.Code}' where id = '{id}'";
            var command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
            return true;
        }

        public bool EditTeacherById(int id, Cards newElement)
        {
            var dbConnection = _dbConnection;
            if (!FindById(newElement.IdTeacher, new Teachers(), _dbConnection)) return false;
            var sql =
                $"update {GetType().Name.ToLower()} set id_student = '{newElement.IdStudent}', id_teacher = '{newElement.IdTeacher}', "
                + $"date_given = '{newElement.DateGiven}', code = '{newElement.Code}' where id = '{id}'";
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