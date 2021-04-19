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
using WpfBDLab2.Windows.Cities;
using WpfBDLab2.Windows.PublHouses;

namespace WpfBDLab2.VM.Forms.PublHouses {
    class PublHousesFormVM : BaseVM.BaseVM {
        private ICommand _addCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private string _readAllString;
        private int _id;

        public PublHousesFormVM() {
            PublHouses = new DataBase.Tables.Publ_Houses(new DBConnector());
            readAllString();
            DbConnector = new DBConnector();
        }

        public DataBase.Tables.Publ_Houses PublHouses { get; set; }

        private void readAllString() {
            ReadAllString =
                ListConvertor.ConvertToString(
                    new TableBase().GetColumnNames(PublHouses, new DBConnector().DBConnection), " || "
                ) + "\n\n";
            ReadAllString +=
                ListConvertor.ConvertToString(
                    new TableBase().ReadAllRowsFromTable(PublHouses, new DBConnector().DBConnection, " || ")
                );
        }

        public int Id {
            get => _id;
            set {
                _id = value;
                OnPropertyChanged(nameof(Id));
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
                    new PublHouseAddWindow(Id,
                        new TableBase().FindByIdByColumn(Id, "house_name", PublHouses, DbConnector.DBConnection)
                    ).ShowDialog();
                    readAllString();
                }
            );

        public ICommand EditCommand =>
            _editCommand ??= new RelayCommand.RelayCommand((o) => {
                    if (new TableBase().FindByIdByColumn(Id, "id", PublHouses, DbConnector.DBConnection) == "") {
                        MessageBox.Show("Нет такого Id!");
                    }
                    else {
                        new PublHouseEditWindow(Id,
                            new TableBase().FindByIdByColumn(Id, "house_name", PublHouses, DbConnector.DBConnection)
                        ).ShowDialog();
                        readAllString();
                    }
                }
            );

        public ICommand DeleteCommand =>
            _deleteCommand ??= new RelayCommand.RelayCommand((o) => {
                    MessageBox.Show(new TableBase().FindByIdByColumn(Id, "id", PublHouses, DbConnector.DBConnection)
                                    == ""
                        ? "Нет такого Id!"
                        : "Запись удалена!"
                    );
                    new TableBase().DeleteById(Id, new DataBase.Tables.Publ_Houses(), DbConnector.DBConnection);
                    readAllString();
                }
            );
    }
}