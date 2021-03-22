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
using WpfBDLab2.Windows.Books;

namespace WpfBDLab2.VM.Forms.Books {
    class BooksFormVM : BaseVM.BaseVM {
        private ICommand _addCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private string _readAllString;
        private int _id;
        private int _idAuthor;
        private int _idCity;
        private int _idPublHouse;
        private int _year;
        private int _code;

        public BooksFormVM() {
            Books = new DataBase.Tables.Books(new DBConnector());
            readAllString();
            DbConnector = new DBConnector();
        }

        public DataBase.Tables.Books Books { get; set; }

        private void readAllString() {
            ReadAllString =
                ListConvertor.ConvertToString(new TableBase().GetColumnNames(Books, new DBConnector().DBConnection),
                    " || "
                ) + "\n\n";
            ReadAllString +=
                ListConvertor.ConvertToString(
                    new TableBase().ReadAllRowsFromTable(Books, new DBConnector().DBConnection, "\t")
                );
        }

        public int Id {
            get => _id;
            set {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int IdAuthor {
            get => _idAuthor;
            set {
                _idAuthor = value;
                OnPropertyChanged(nameof(IdAuthor));
            }
        }

        public int IdCity {
            get => _idCity;
            set {
                _idCity = value;
                OnPropertyChanged(nameof(IdCity));
            }
        }

        public int IdPublHouse {
            get => _idPublHouse;
            set {
                _idPublHouse = value;
                OnPropertyChanged(nameof(IdPublHouse));
            }
        }

        public int Year {
            get => _year;
            set {
                _year = value;
                OnPropertyChanged(nameof(Year));
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
                    new BookAddWindow(Id,
                        Convert.ToInt32(
                            new TableBase().FindByIdByColumn(Id, "id_author", Books, DbConnector.DBConnection) == ""
                                ? 0
                                : new TableBase().FindByIdByColumn(Id, "id_author", Books, DbConnector.DBConnection)
                        ),
                        Convert.ToInt32(
                            new TableBase().FindByIdByColumn(Id, "id_city", Books, DbConnector.DBConnection) == ""
                                ? 0
                                : new TableBase().FindByIdByColumn(Id, "id_city", Books, DbConnector.DBConnection)
                        ),
                        Convert.ToInt32(
                            new TableBase().FindByIdByColumn(Id, "id_publ_house", Books, DbConnector.DBConnection) == ""
                                ? 0
                                : new TableBase().FindByIdByColumn(Id, "id_publ_house", Books, DbConnector.DBConnection)
                        ),
                        Convert.ToInt32(new TableBase().FindByIdByColumn(Id, "year", Books, DbConnector.DBConnection)
                                        == ""
                            ? 0
                            : new TableBase().FindByIdByColumn(Id, "year", Books, DbConnector.DBConnection)
                        ),
                        Convert.ToInt32(new TableBase().FindByIdByColumn(Id, "code", Books, DbConnector.DBConnection)
                                        == ""
                            ? 0
                            : new TableBase().FindByIdByColumn(Id, "code", Books, DbConnector.DBConnection)
                        )
                    ).ShowDialog();
                    readAllString();
                }
            );

        public ICommand EditCommand =>
            _editCommand ??= new RelayCommand.RelayCommand((o) => {
                    if (new TableBase().FindByIdByColumn(Id, "id", Books, DbConnector.DBConnection) == "") {
                        MessageBox.Show("Нет такого Id!");
                    }
                    else {
                        new BookEditWindow(Id,
                        Convert.ToInt32(
                            new TableBase().FindByIdByColumn(Id, "id_author", Books, DbConnector.DBConnection) == ""
                                ? 0
                                : new TableBase().FindByIdByColumn(Id, "id_author", Books, DbConnector.DBConnection)
                        ),
                        Convert.ToInt32(
                            new TableBase().FindByIdByColumn(Id, "id_city", Books, DbConnector.DBConnection) == ""
                                ? 0
                                : new TableBase().FindByIdByColumn(Id, "id_city", Books, DbConnector.DBConnection)
                        ),
                        Convert.ToInt32(
                            new TableBase().FindByIdByColumn(Id, "id_publ_house", Books, DbConnector.DBConnection) == ""
                                ? 0
                                : new TableBase().FindByIdByColumn(Id, "id_publ_house", Books, DbConnector.DBConnection)
                        ),
                        Convert.ToInt32(new TableBase().FindByIdByColumn(Id, "year", Books, DbConnector.DBConnection)
                                        == ""
                            ? 0
                            : new TableBase().FindByIdByColumn(Id, "year", Books, DbConnector.DBConnection)
                        ),
                        Convert.ToInt32(new TableBase().FindByIdByColumn(Id, "code", Books, DbConnector.DBConnection)
                                        == ""
                            ? 0
                            : new TableBase().FindByIdByColumn(Id, "code", Books, DbConnector.DBConnection)
                        )
                    ).ShowDialog();
                    readAllString();
                    }
                }
            );

        public ICommand DeleteCommand =>
            _deleteCommand ??= new RelayCommand.RelayCommand((o) => {
                    MessageBox.Show(new TableBase().FindByIdByColumn(Id, "id", Books, DbConnector.DBConnection) == ""
                        ? "Нет такого Id!"
                        : "Запись удалена!"
                    );
                    new TableBase().DeleteById(Id, new DataBase.Tables.Books(), DbConnector.DBConnection);
                    readAllString();
                }
            );
    }
}