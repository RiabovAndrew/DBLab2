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
using WpfBDLab2.Windows.History;

namespace WpfBDLab2.VM.Forms.History {
    class HistoriesFormVM : BaseVM.BaseVM {
        private ICommand _addCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private string _readAllString;
        private int _id;
        private int _idCard;
        private int _idBook;
        private string _dateGiven;
        private string _dateGivenUntill;
        private string _worker;

        public HistoriesFormVM() {
            History = new DataBase.Tables.History(new DBConnector());
            readAllString();
            DbConnector = new DBConnector();
        }

        public DataBase.Tables.History History { get; set; }

        private void readAllString() {
            ReadAllString =
                ListConvertor.ConvertToString(new TableBase().GetColumnNames(History, new DBConnector().DBConnection),
                    " || "
                ) + "\n\n";
            ReadAllString +=
                ListConvertor.ConvertToString(
                    new TableBase().ReadAllRowsFromTable(History, new DBConnector().DBConnection, "\t")
                );
        }

        public int Id {
            get => _id;
            set {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int IdCard {
            get => _idCard;
            set {
                _idCard = value;
                OnPropertyChanged(nameof(IdCard));
            }
        }

        public int IdBook {
            get => _idBook;
            set {
                _idBook = value;
                OnPropertyChanged(nameof(IdBook));
            }
        }

        public string DateGiven {
            get => _dateGiven;
            set {
                _dateGiven = value;
                OnPropertyChanged(nameof(DateGiven));
            }
        }

        public string DateGivenUntill {
            get => _dateGivenUntill;
            set {
                _dateGivenUntill = value;
                OnPropertyChanged(nameof(DateGivenUntill));
            }
        }

        public string Worker {
            get => _worker;
            set {
                _worker = value;
                OnPropertyChanged(nameof(Worker));
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
                    new HistoryAddWindow(Id,
                        Convert.ToInt32(
                            new TableBase().FindByIdByColumn(Id, "id_card", History, DbConnector.DBConnection) == ""
                                ? 0
                                : new TableBase().FindByIdByColumn(Id, "id_card", History, DbConnector.DBConnection)
                        ),
                        Convert.ToInt32(
                            new TableBase().FindByIdByColumn(Id, "id_book", History, DbConnector.DBConnection) == ""
                                ? 0
                                : new TableBase().FindByIdByColumn(Id, "id_book", History, DbConnector.DBConnection)
                        ), new TableBase().FindByIdByColumn(Id, "date_given", History, DbConnector.DBConnection),
                        new TableBase().FindByIdByColumn(Id, "date_given_untill", History, DbConnector.DBConnection),
                        new TableBase().FindByIdByColumn(Id, "worker", History, DbConnector.DBConnection)
                    ).ShowDialog();
                    readAllString();
                }
            );

        public ICommand EditCommand =>
            _editCommand ??= new RelayCommand.RelayCommand((o) => {
                    if (new TableBase().FindByIdByColumn(Id, "id", History, DbConnector.DBConnection) == "") {
                        MessageBox.Show("Нет такого Id!");
                    }
                    else {
                        new HistoryEditWindiw(Id,
                            Convert.ToInt32(
                                new TableBase().FindByIdByColumn(Id, "id_card", History, DbConnector.DBConnection) == ""
                                    ? 0
                                    : new TableBase().FindByIdByColumn(Id, "id_card", History, DbConnector.DBConnection)
                            ),
                            Convert.ToInt32(
                                new TableBase().FindByIdByColumn(Id, "id_book", History, DbConnector.DBConnection) == ""
                                    ? 0
                                    : new TableBase().FindByIdByColumn(Id, "id_book", History, DbConnector.DBConnection)
                            ), new TableBase().FindByIdByColumn(Id, "date_given", History, DbConnector.DBConnection),
                            new TableBase().FindByIdByColumn(Id, "date_given_untill", History, DbConnector.DBConnection),
                            new TableBase().FindByIdByColumn(Id, "worker", History, DbConnector.DBConnection)
                        ).ShowDialog();
                    readAllString();
                    }
                }
            );

        public ICommand DeleteCommand =>
            _deleteCommand ??= new RelayCommand.RelayCommand((o) => {
                    MessageBox.Show(new TableBase().FindByIdByColumn(Id, "id", History, DbConnector.DBConnection) == ""
                        ? "Нет такого Id!"
                        : "Запись удалена!"
                    );
                    new TableBase().DeleteById(Id, new DataBase.Tables.History(), DbConnector.DBConnection);
                    readAllString();
                }
            );
    }
}