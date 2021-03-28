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
using WpfBDLab2.Windows.SpecFac;
using WpfBDLab2.Windows.Teachers;

namespace WpfBDLab2.VM.Forms.Teachers {
    class TeachersFormVM : BaseVM.BaseVM {
        private ICommand _addCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private string _readAllString;
        private int _id;
        private string _name;
        private int _idFac;

        public TeachersFormVM() {
            Teachers = new DataBase.Tables.Teachers(new DBConnector());
            readAllString();
            DbConnector = new DBConnector();
        }

        public DataBase.Tables.Teachers Teachers { get; set; }

        private void readAllString() {
            ReadAllString =
                ListConvertor.ConvertToString(new TableBase().GetColumnNames(Teachers, new DBConnector().DBConnection),
                    " || "
                ) + "\n\n";
            ReadAllString +=
                ListConvertor.ConvertToString(
                    new TableBase().ReadAllRowsFromTable(Teachers, new DBConnector().DBConnection, " || ")
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

        public int IdFac {
            get => _idFac;
            set {
                _idFac = value;
                OnPropertyChanged(nameof(IdFac));
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
                    new TeacherAddWindow(Id,
                        new TableBase().FindByIdByColumn(Id, "name", Teachers, DbConnector.DBConnection),
                        Convert.ToInt32(
                            new TableBase().FindByIdByColumn(Id, "id_faculty", Teachers, DbConnector.DBConnection) == ""
                                ? 0
                                : new TableBase().FindByIdByColumn(Id, "id_faculty", Teachers, DbConnector.DBConnection)
                        )
                    ).ShowDialog();
                    readAllString();
                }
            );

        public ICommand EditCommand =>
            _editCommand ??= new RelayCommand.RelayCommand((o) => {
                    if (new TableBase().FindByIdByColumn(Id, "id", Teachers, DbConnector.DBConnection) == "") {
                        MessageBox.Show("Нет такого Id!");
                    }
                    else {
                        new TeacherEditWindow(Id,
                            new TableBase().FindByIdByColumn(Id, "name", Teachers, DbConnector.DBConnection),
                            Convert.ToInt32(
                                new TableBase().FindByIdByColumn(Id, "id_faculty", Teachers, DbConnector.DBConnection)
                                == ""
                                    ? 0
                                    : new TableBase().FindByIdByColumn(Id, "id_faculty", Teachers,
                                        DbConnector.DBConnection
                                    )
                            )
                        ).ShowDialog();
                        readAllString();
                    }
                }
            );

        public ICommand DeleteCommand =>
            _deleteCommand ??= new RelayCommand.RelayCommand((o) => {
                    MessageBox.Show(new TableBase().FindByIdByColumn(Id, "id", Teachers, DbConnector.DBConnection) == ""
                        ? "Нет такого Id!"
                        : "Запись удалена!"
                    );
                    new TableBase().DeleteById(Id, new DataBase.Tables.Teachers(), DbConnector.DBConnection);
                    readAllString();
                }
            );
    }
}