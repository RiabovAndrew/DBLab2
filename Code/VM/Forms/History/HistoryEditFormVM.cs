using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfBDLab2.VM.Forms.History {
    class HistoryEditFormVM : BaseVM.BaseVM {
        private int _id;
        private int _idCard;
        private int _idBook;
        private string _dateGiven;
        private string _dateGivenUntill;
        private string _worker;
        private ICommand _cancelCommand;
        private ICommand _editCommand;

        public HistoryEditFormVM(int id, int idCard, int idBook, string dateGiven, string dateGivenUntill,
                                 string worker) {
            Id = id;
            IdCard = idCard;
            IdBook = idBook;
            DateGiven = dateGiven;
            DateGivenUntill = dateGivenUntill;
            Worker = worker;
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

        public ICommand CancelCommand =>
            _cancelCommand ??= new RelayCommand.RelayCommand((o) => {
                    var window = o as Window;
                    window?.Close();
                }
            );

        public ICommand EditCommand =>
            _editCommand ??= new RelayCommand.RelayCommand((o) => {
                    MessageBox.Show(new DataBase.Tables.History(DbConnector).EditByID(Id,
                            new DataBase.Tables.History(DbConnector, IdCard, IdBook, DateGiven, DateGivenUntill, Worker)
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