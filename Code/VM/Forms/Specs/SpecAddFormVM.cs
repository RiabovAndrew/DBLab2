using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfBDLab2.VM.Forms.Specs {
    class SpecAddFormVM : BaseVM.BaseVM {
        private int _id;
        private string _name;
        private string _letter;
        private int _code;
        private ICommand _cancelCommand;
        private ICommand _addCommand;

        public SpecAddFormVM(int id, string name, string letter, int code) {
            Id = id;
            Name = name;
            Letter = letter;
            Code = code;
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

        public ICommand CancelCommand =>
            _cancelCommand ??= new RelayCommand.RelayCommand((o) => {
                    var window = o as Window;
                    window?.Close();
                }
            );

        public ICommand AddCommand =>
            _addCommand ??= new RelayCommand.RelayCommand((o) => {
                    new DataBase.Tables.Specs(DbConnector, Name, Letter, Code).Insert();
                    var ms = MessageBox.Show("Новая запись была добавлена!");
                    var window = o as Window;
                    window?.Close();
                }
            );
    }
}