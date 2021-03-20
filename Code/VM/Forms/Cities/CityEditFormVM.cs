using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfBDLab2.VM.Forms.Cities {
    class CityEditFormVM : BaseVM.BaseVM {
        private int _id;
        private string _name;
        private ICommand _cancelCommand;
        private ICommand _editCommand;

        public CityEditFormVM(int id, string name) {
            Id = id;
            Name = name;
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

        public ICommand CancelCommand =>
            _cancelCommand ??= new RelayCommand.RelayCommand((o) => {
                    var window = o as Window;
                    window?.Close();
                }
            );

        public ICommand EditCommand =>
            _editCommand ??= new RelayCommand.RelayCommand((o) => {
                    new DataBase.Tables.Cities(DbConnector).EditByID(Id, new DataBase.Tables.Cities(DbConnector, Name));
                    var ms = MessageBox.Show("Запись была обновлена!");
                    var window = o as Window;
                    window?.Close();
                }
            );
    }
}