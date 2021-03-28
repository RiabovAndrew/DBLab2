using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfBDLab2.DataBase.DbConnector;
using WpfBDLab2.DataBase.Father;
using WpfBDLab2.DataConvertor;
using WpfBDLab2.Windows.Books;
using WpfBDLab2.Windows.Cards;

namespace WpfBDLab2.VM.Forms.Cards {
    class CardsFormVM : BaseVM.BaseVM {
        private ICommand _addStudentCommand;
        private ICommand _addTeacherCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private string _readAllString;
        private int _id;
        private int _idStudent;
        private int _idTeacher;
        private string _dateGiven;
        private int _code;

        public CardsFormVM() {
            Cards = new DataBase.Tables.Cards(new DBConnector());
            readAllString();
            DbConnector = new DBConnector();
        }

        public DataBase.Tables.Cards Cards { get; set; }

        private void readAllString() {
            ReadAllString =
                ListConvertor.ConvertToString(new TableBase().GetColumnNames(Cards, new DBConnector().DBConnection),
                    " || "
                ) + "\n\n";
            ReadAllString +=
                ListConvertor.ConvertToString(
                    new TableBase().ReadAllRowsFromTable(Cards, new DBConnector().DBConnection, "\t")
                );
        }

        public int Id {
            get => _id;
            set {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int IdStudent {
            get => _idStudent;
            set {
                _idStudent = value;
                OnPropertyChanged(nameof(IdStudent));
            }
        }

        public int IdTeacher {
            get => _idTeacher;
            set {
                _idTeacher = value;
                OnPropertyChanged(nameof(IdTeacher));
            }
        }

        public string DateGiven {
            get => _dateGiven;
            set {
                _dateGiven = value;
                OnPropertyChanged(nameof(DateGiven));
            }
        }

        public int Code {
            get => _code;
            set {
                _code = value;
                OnPropertyChanged(nameof(Code));
            }
        }

        public string ReadAllString {
            get => _readAllString;
            set {
                _readAllString = value;
                OnPropertyChanged(nameof(ReadAllString));
            }
        }

        public ICommand AddStudentCommand =>
            _addStudentCommand ??= new RelayCommand.RelayCommand((o) => {
                    if (new TableBase().FindByIdByColumn(Id, "id_teacher", Cards, DbConnector.DBConnection) != ""
                        && new TableBase().FindByIdByColumn(Id, "id_teacher", Cards, DbConnector.DBConnection) == "0") {
                        new CardAddStudentWindow(Id,
                            Convert.ToInt32(
                                new TableBase().FindByIdByColumn(Id, "id_student", Cards, DbConnector.DBConnection)
                                == ""
                                    ? 0
                                    : new TableBase().FindByIdByColumn(Id, "id_student", Cards, DbConnector.DBConnection
                                    )
                            ),
                            Convert.ToInt32(
                                new TableBase().FindByIdByColumn(Id, "id_teacher", Cards, DbConnector.DBConnection)
                                == ""
                                    ? 0
                                    : new TableBase().FindByIdByColumn(Id, "id_teacher", Cards, DbConnector.DBConnection
                                    )
                            ), new TableBase().FindByIdByColumn(Id, "date_given", Cards, DbConnector.DBConnection),
                            Convert.ToInt32(
                                new TableBase().FindByIdByColumn(Id, "code", Cards, DbConnector.DBConnection) == ""
                                    ? 0
                                    : new TableBase().FindByIdByColumn(Id, "code", Cards, DbConnector.DBConnection)
                            )
                        ).ShowDialog();
                    }
                    else {
                        MessageBox.Show("Нельзя добавить новую запись на основе этой!");
                    }

                    readAllString();
                }
            );

        public ICommand AddTeacherCommand =>
            _addTeacherCommand ??= new RelayCommand.RelayCommand((o) => {
                    if (new TableBase().FindByIdByColumn(Id, "id_student", Cards, DbConnector.DBConnection) != ""
                        && new TableBase().FindByIdByColumn(Id, "id_student", Cards, DbConnector.DBConnection) == "0") {
                        new CardAddTeacherWindow(Id,
                            Convert.ToInt32(
                                new TableBase().FindByIdByColumn(Id, "id_student", Cards, DbConnector.DBConnection)
                                == ""
                                    ? 0
                                    : new TableBase().FindByIdByColumn(Id, "id_student", Cards, DbConnector.DBConnection
                                    )
                            ),
                            Convert.ToInt32(
                                new TableBase().FindByIdByColumn(Id, "id_teacher", Cards, DbConnector.DBConnection)
                                == ""
                                    ? 0
                                    : new TableBase().FindByIdByColumn(Id, "id_teacher", Cards, DbConnector.DBConnection
                                    )
                            ), new TableBase().FindByIdByColumn(Id, "date_given", Cards, DbConnector.DBConnection),
                            Convert.ToInt32(
                                new TableBase().FindByIdByColumn(Id, "code", Cards, DbConnector.DBConnection) == ""
                                    ? 0
                                    : new TableBase().FindByIdByColumn(Id, "code", Cards, DbConnector.DBConnection)
                            )
                        ).ShowDialog();
                        readAllString();
                    }
                    else {
                        MessageBox.Show("Нельзя добавить новую запись на основе этой!");
                    }
                }
            );

        public ICommand EditCommand =>
            _editCommand ??= new RelayCommand.RelayCommand((o) => {
                    if (new TableBase().FindByIdByColumn(Id, "id", Cards, DbConnector.DBConnection) == "") {
                        MessageBox.Show("Нет такого Id!");
                    }
                    else {
                        if (new TableBase().FindByIdByColumn(Id, "id_teacher", Cards, DbConnector.DBConnection) != ""
                            && new TableBase().FindByIdByColumn(Id, "id_teacher", Cards, DbConnector.DBConnection)
                            == "0") {
                            new CardEditStudentWindow(Id,
                                Convert.ToInt32(
                                    new TableBase().FindByIdByColumn(Id, "id_student", Cards, DbConnector.DBConnection)
                                    == ""
                                        ? 0
                                        : new TableBase().FindByIdByColumn(Id, "id_student", Cards,
                                            DbConnector.DBConnection
                                        )
                                ),
                                Convert.ToInt32(
                                    new TableBase().FindByIdByColumn(Id, "id_teacher", Cards, DbConnector.DBConnection)
                                    == ""
                                        ? 0
                                        : new TableBase().FindByIdByColumn(Id, "id_teacher", Cards,
                                            DbConnector.DBConnection
                                        )
                                ), new TableBase().FindByIdByColumn(Id, "date_given", Cards, DbConnector.DBConnection),
                                Convert.ToInt32(
                                    new TableBase().FindByIdByColumn(Id, "code", Cards, DbConnector.DBConnection) == ""
                                        ? 0
                                        : new TableBase().FindByIdByColumn(Id, "code", Cards, DbConnector.DBConnection)
                                )
                            ).ShowDialog();
                        }
                        else {
                            new CardEditTeacherWindow(Id,
                                Convert.ToInt32(
                                    new TableBase().FindByIdByColumn(Id, "id_student", Cards, DbConnector.DBConnection)
                                    == ""
                                        ? 0
                                        : new TableBase().FindByIdByColumn(Id, "id_student", Cards,
                                            DbConnector.DBConnection
                                        )
                                ),
                                Convert.ToInt32(
                                    new TableBase().FindByIdByColumn(Id, "id_teacher", Cards, DbConnector.DBConnection)
                                    == ""
                                        ? 0
                                        : new TableBase().FindByIdByColumn(Id, "id_teacher", Cards,
                                            DbConnector.DBConnection
                                        )
                                ), new TableBase().FindByIdByColumn(Id, "date_given", Cards, DbConnector.DBConnection),
                                Convert.ToInt32(
                                    new TableBase().FindByIdByColumn(Id, "code", Cards, DbConnector.DBConnection) == ""
                                        ? 0
                                        : new TableBase().FindByIdByColumn(Id, "code", Cards, DbConnector.DBConnection)
                                )
                            ).ShowDialog();
                        }

                        readAllString();
                    }
                }
            );

        public ICommand DeleteCommand =>
            _deleteCommand ??= new RelayCommand.RelayCommand((o) => {
                    MessageBox.Show(new TableBase().FindByIdByColumn(Id, "id", Cards, DbConnector.DBConnection) == ""
                        ? "Нет такого Id!"
                        : "Запись удалена!"
                    );
                    new TableBase().DeleteById(Id, new DataBase.Tables.Cards(), DbConnector.DBConnection);
                    readAllString();
                }
            );
    }
}