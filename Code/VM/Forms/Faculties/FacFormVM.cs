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
using WpfBDLab2.Windows.Authors;
using WpfBDLab2.Windows.Faculties;

namespace WpfBDLab2.VM.Forms.Faculties {
    class FacFormVM : BaseVM.BaseVM {
        private ICommand _addCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private string _readAllString;
        private int _id;
        private string _name;
        private string _letter;

        public FacFormVM() {
            Facs = new DataBase.Tables.Facs(new DBConnector());
            readAllString();
            DbConnector = new DBConnector();
        }

        public DataBase.Tables.Facs Facs { get; set; }

        private void readAllString() {
            ReadAllString =
                ListConvertor.ConvertToString(new TableBase().GetColumnNames(Facs, new DBConnector().DBConnection),
                    " || "
                ) + "\n\n";
            ReadAllString +=
                ListConvertor.ConvertToString(
                    new TableBase().ReadAllRowsFromTable(Facs, new DBConnector().DBConnection, " || ")
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

        public string ReadAllString {
            get => _readAllString;
            set {
                _readAllString = value;
                OnPropertyChanged(nameof(ReadAllString));
            }
        }

        public ICommand AddCommand =>
            _addCommand ??= new RelayCommand.RelayCommand((o) => {
                    new FacAddWindow(Id, new TableBase().FindByIdByColumn(Id, "name", Facs, DbConnector.DBConnection),
                        new TableBase().FindByIdByColumn(Id, "letter", Facs, DbConnector.DBConnection)
                    ).ShowDialog();
                    readAllString();
                }
            );

        public ICommand EditCommand =>
            _editCommand ??= new RelayCommand.RelayCommand((o) => {
                    if (new TableBase().FindByIdByColumn(Id, "id", Facs, DbConnector.DBConnection) == "") {
                        MessageBox.Show("Нет такого Id!");
                    }
                    else {
                        new FacEditWindow(Id,
                            new TableBase().FindByIdByColumn(Id, "name", Facs, DbConnector.DBConnection),
                            new TableBase().FindByIdByColumn(Id, "letter", Facs, DbConnector.DBConnection)
                        ).ShowDialog();
                        readAllString();
                    }
                }
            );

        public ICommand DeleteCommand =>
            _deleteCommand ??= new RelayCommand.RelayCommand((o) => {
                    MessageBox.Show(new TableBase().FindByIdByColumn(Id, "id", Facs, DbConnector.DBConnection) == ""
                        ? "Нет такого Id!"
                        : "Запись удалена!"
                    );
                    new TableBase().DeleteById(Id, new DataBase.Tables.Facs(), DbConnector.DBConnection);
                    readAllString();
                }
            );
    }
}