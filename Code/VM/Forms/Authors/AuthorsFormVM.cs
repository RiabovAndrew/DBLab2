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
using WpfBDLab2.Windows.Cities;

namespace WpfBDLab2.VM.Forms.Authors {
    class AuthorsFormVM : BaseVM.BaseVM {
        private ICommand _addCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private string _readAllString;
        private int _id;
        private int _year;

        public AuthorsFormVM() {
            Authors = new DataBase.Tables.Authors(new DBConnector());
            readAllString();
            DbConnector = new DBConnector();
        }

        public DataBase.Tables.Authors Authors { get; set; }

        private void readAllString() {
            ReadAllString =
                ListConvertor.ConvertToString(new TableBase().GetColumnNames(Authors, new DBConnector().DBConnection),
                    " || "
                ) + "\n\n";
            ReadAllString +=
                ListConvertor.ConvertToString(
                    new TableBase().ReadAllRowsFromTable(Authors, new DBConnector().DBConnection, " || ")
                );
        }

        public int Id {
            get => _id;
            set {
                _id = value;
                OnPropertyChanged(nameof(Id));
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
                    new AuthorAddWindow(Id,
                        new TableBase().FindByIdByColumn(Id, "name", Authors, DbConnector.DBConnection),
                        Convert.ToInt32(
                            new TableBase().FindByIdByColumn(Id, "year_birth", Authors, DbConnector.DBConnection) == ""
                                ? 0
                                : new TableBase().FindByIdByColumn(Id, "year_birth", Authors, DbConnector.DBConnection)
                        )
                    ).ShowDialog();
                    readAllString();
                }
            );

        public ICommand EditCommand =>
            _editCommand ??= new RelayCommand.RelayCommand((o) => {
                    if (new TableBase().FindByIdByColumn(Id, "id", Authors, DbConnector.DBConnection) == "") {
                        MessageBox.Show("Нет такого Id!");
                    }
                    else {
                        new AuthorEditWindow(Id,
                            new TableBase().FindByIdByColumn(Id, "name", Authors, DbConnector.DBConnection),
                            Convert.ToInt32(new TableBase().FindByIdByColumn(Id, "year_birth", Authors,
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
                    MessageBox.Show(new TableBase().FindByIdByColumn(Id, "id", Authors, DbConnector.DBConnection) == ""
                        ? "Нет такого Id!"
                        : "Запись удалена!"
                    );
                    new TableBase().DeleteById(Id, new DataBase.Tables.Authors(), DbConnector.DBConnection);
                    readAllString();
                }
            );
    }
}