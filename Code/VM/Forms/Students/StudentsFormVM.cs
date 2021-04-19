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
using WpfBDLab2.Windows.Students;
using WpfBDLab2.Windows.Teachers;

namespace WpfBDLab2.VM.Forms.Students {
    class StudentsFormVM : BaseVM.BaseVM {
        private ICommand _addCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private string _readAllString;
        private int _id;
        private string _name;
        private int _idSpecFac;
        private int _year;

        public StudentsFormVM() {
            Students = new DataBase.Tables.Students(new DBConnector());
            readAllString();
            DbConnector = new DBConnector();
        }

        public DataBase.Tables.Students Students { get; set; }

        private void readAllString() {
            ReadAllString =
                ListConvertor.ConvertToString(new TableBase().GetColumnNames(Students, new DBConnector().DBConnection),
                    " || "
                ) + "\n\n";
            ReadAllString +=
                ListConvertor.ConvertToString(
                    new TableBase().ReadAllRowsFromTable(Students, new DBConnector().DBConnection, " || ")
                );
        }

        public int Id {
            get => _id;
            set {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name {
            get => _name;
            set {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int IdSpecFac {
            get => _idSpecFac;
            set {
                _idSpecFac = value;
                OnPropertyChanged(nameof(IdSpecFac));
            }
        }

        public int Year {
            get => _year;
            set {
                _year = value;
                OnPropertyChanged(nameof(Year));
            }
        }

        public string ReadAllString {
            get => _readAllString;
            set {
                _readAllString = value;
                OnPropertyChanged(nameof(ReadAllString));
            }
        }

        public ICommand AddCommand =>
            _addCommand ??= new RelayCommand.RelayCommand((o) => {
                    new StudentAddWindow(Id,
                        new TableBase().FindByIdByColumn(Id, "name", Students, DbConnector.DBConnection),
                        Convert.ToInt32(
                            new TableBase().FindByIdByColumn(Id, "id_spec_fac", Students, DbConnector.DBConnection)
                            == ""
                                ? 0
                                : new TableBase().FindByIdByColumn(Id, "id_spec_fac", Students, DbConnector.DBConnection
                                )
                        ),
                        Convert.ToInt32(
                            new TableBase().FindByIdByColumn(Id, "year", Students, DbConnector.DBConnection) == ""
                                ? 0
                                : new TableBase().FindByIdByColumn(Id, "year", Students, DbConnector.DBConnection)
                        )
                    ).ShowDialog();
                    readAllString();
                }
            );

        public ICommand EditCommand =>
            _editCommand ??= new RelayCommand.RelayCommand((o) => {
                    if (new TableBase().FindByIdByColumn(Id, "id", Students, DbConnector.DBConnection) == "") {
                        MessageBox.Show("Нет такого Id!");
                    }
                    else {
                        new StudentEditWindow(Id,
                            new TableBase().FindByIdByColumn(Id, "name", Students, DbConnector.DBConnection),
                            Convert.ToInt32(
                                new TableBase().FindByIdByColumn(Id, "id_spec_fac", Students, DbConnector.DBConnection)
                                == ""
                                    ? 0
                                    : new TableBase().FindByIdByColumn(Id, "id_spec_fac", Students, DbConnector.DBConnection
                                    )
                            ),
                            Convert.ToInt32(
                                new TableBase().FindByIdByColumn(Id, "year", Students, DbConnector.DBConnection) == ""
                                    ? 0
                                    : new TableBase().FindByIdByColumn(Id, "year", Students, DbConnector.DBConnection)
                            )
                        ).ShowDialog();
                    readAllString();
                    }
                }
            );

        public ICommand DeleteCommand =>
            _deleteCommand ??= new RelayCommand.RelayCommand((o) => {
                    MessageBox.Show(new TableBase().FindByIdByColumn(Id, "id", Students, DbConnector.DBConnection) == ""
                        ? "Нет такого Id!"
                        : "Запись удалена!"
                    );
                    new TableBase().DeleteById(Id, new DataBase.Tables.Students(), DbConnector.DBConnection);
                    readAllString();
                }
            );
    }
}