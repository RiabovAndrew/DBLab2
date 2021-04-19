using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfBDLab2.VM.Forms.Cards {
    class CardEditFormVM : BaseVM.BaseVM {
        private int _id;
        private int _idStudent;
        private int _idTeacher;
        private string _dateGiven;
        private int _code;
        private ICommand _cancelCommand;
        private ICommand _editCommand;

        public CardEditFormVM(int id, int idStudent, int idTeacher, string dateGiven, int code) {
            Id = id;
            IdStudent = idStudent;
            IdTeacher = idTeacher;
            DateGiven = dateGiven;
            Code = code;
        }

        public int Id {
            get => _id;
            set {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int IdStudent {
            get => _idStudent;
            set {
                _idStudent = value;
                OnPropertyChanged(nameof(IdStudent));
            }
        }

        public int IdTeacher {
            get => _idTeacher;
            set {
                _idTeacher = value;
                OnPropertyChanged(nameof(IdTeacher));
            }
        }

        public string DateGiven {
            get => _dateGiven;
            set {
                _dateGiven = value;
                OnPropertyChanged(nameof(DateGiven));
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
                    if (IdTeacher == 0) {
                        MessageBox.Show(new DataBase.Tables.Cards(DbConnector).EditStudentById(Id,
                                new DataBase.Tables.Cards(DbConnector, DateGiven, Code, IdStudent, IdTeacher)
                            )
                                ? "Запись была обновлена!"
                                : "Внешние ключи заданы неверно!"
                        );
                    }
                    else {
                        MessageBox.Show(new DataBase.Tables.Cards(DbConnector).EditTeacherById(Id,
                                new DataBase.Tables.Cards(DbConnector, DateGiven, Code, IdStudent, IdTeacher)
                            )
                                ? "Запись была обновлена!"
                                : "Внешние ключи заданы неверно!"
                        );
                    }

                    var window = o as Window;
                    window?.Close();
                }
            );
    }
}