using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfBDLab2.VM.Forms.Books {
    class BookEditFormVM : BaseVM.BaseVM {
        private int _id;
        private int _idAuthor;
        private int _idCity;
        private int _idPublHouse;
        private int _year;
        private int _code;
        private ICommand _cancelCommand;
        private ICommand _editCommand;

        public BookEditFormVM(int id, int idAuthor, int idCity, int idPublHouse, int year, int code) {
            Id = id;
            IdAuthor = idAuthor;
            IdCity = idCity;
            IdPublHouse = idPublHouse;
            Year = year;
            Code = code;
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

        public ICommand CancelCommand =>
            _cancelCommand ??= new RelayCommand.RelayCommand((o) => {
                    var window = o as Window;
                    window?.Close();
                }
            );

        public ICommand EditCommand =>
            _editCommand ??= new RelayCommand.RelayCommand((o) => {
                    MessageBox.Show(new DataBase.Tables.Books(DbConnector).EditByID(Id,
                            new DataBase.Tables.Books(DbConnector, IdAuthor, IdCity, IdPublHouse, Year, Code)
                        )
                            ? "Запись была обновлена!"
                            : "Внешние ключи заданы неверно!"
                    );
                    var window = o as Window;
                    window?.Close();
                }
            );
    }
}