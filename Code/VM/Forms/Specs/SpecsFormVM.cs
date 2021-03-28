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
using WpfBDLab2.Windows.Faculties;
using WpfBDLab2.Windows.Specs;

namespace WpfBDLab2.VM.Forms.Specs {
    class SpecsFormVM : BaseVM.BaseVM {
        private ICommand _addCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private string _readAllString;
        private int _id;
        private string _name;
        private string _letter;
        private int _code;

        public SpecsFormVM() {
            Specs = new DataBase.Tables.Specs(new DBConnector());
            readAllString();
            DbConnector = new DBConnector();
        }

        public DataBase.Tables.Specs Specs { get; set; }

        private void readAllString() {
            ReadAllString =
                ListConvertor.ConvertToString(new TableBase().GetColumnNames(Specs, new DBConnector().DBConnection),
                    " || "
                ) + "\n\n";
            ReadAllString +=
                ListConvertor.ConvertToString(
                    new TableBase().ReadAllRowsFromTable(Specs, new DBConnector().DBConnection, " || ")
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

        public string Letter {
            get => _letter;
            set {
                _letter = value;
                OnPropertyChanged(nameof(Letter));
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

        public ICommand AddCommand =>
            _addCommand ??= new RelayCommand.RelayCommand((o) => {
                    new SpecAddWindow(Id, new TableBase().FindByIdByColumn(Id, "name", Specs, DbConnector.DBConnection),
                        new TableBase().FindByIdByColumn(Id, "letter", Specs, DbConnector.DBConnection),
                        Convert.ToInt32(new TableBase().FindByIdByColumn(Id, "code", Specs, DbConnector.DBConnection)
                                        == ""
                            ? 0
                            : new TableBase().FindByIdByColumn(Id, "code", Specs, DbConnector.DBConnection)
                        )
                    ).ShowDialog();
                    readAllString();
                }
            );

        public ICommand EditCommand =>
            _editCommand ??= new RelayCommand.RelayCommand((o) => {
                    if (new TableBase().FindByIdByColumn(Id, "id", Specs, DbConnector.DBConnection) == "") {
                        MessageBox.Show("Нет такого Id!");
                    }
                    else {
                        new SpecEditWindow(Id,
                            new TableBase().FindByIdByColumn(Id, "name", Specs, DbConnector.DBConnection),
                            new TableBase().FindByIdByColumn(Id, "letter", Specs, DbConnector.DBConnection),
                            Convert.ToInt32(
                                new TableBase().FindByIdByColumn(Id, "code", Specs, DbConnector.DBConnection) == ""
                                    ? 0
                                    : new TableBase().FindByIdByColumn(Id, "code", Specs, DbConnector.DBConnection)
                            )
                        ).ShowDialog();
                        readAllString();
                    }
                }
            );

        public ICommand DeleteCommand =>
            _deleteCommand ??= new RelayCommand.RelayCommand((o) => {
                    MessageBox.Show(new TableBase().FindByIdByColumn(Id, "id", Specs, DbConnector.DBConnection) == ""
                        ? "Нет такого Id!"
                        : "Запись удалена!"
                    );
                    new TableBase().DeleteById(Id, new DataBase.Tables.Specs(), DbConnector.DBConnection);
                    readAllString();
                }
            );
    }
}