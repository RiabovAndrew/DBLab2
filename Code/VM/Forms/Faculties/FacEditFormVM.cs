using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfBDLab2.VM.Forms.Faculties
{
    class FacEditFormVM : BaseVM.BaseVM
    {
        private int _id;
        private string _name;
        private string _letter;
        private ICommand _cancelCommand;
        private ICommand _editCommand;

        public FacEditFormVM(int id, string name, string letter)
        {
            Id = id;
            Name = name;
            Letter = letter;
        }

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Letter
        {
            get => _letter;
            set
            {
                _letter = value;
                OnPropertyChanged(nameof(Letter));
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
                    new DataBase.Tables.Facs(DbConnector).EditByID(Id, new DataBase.Tables.Facs(DbConnector, Name, Letter));
                    var ms = MessageBox.Show("Запись была обновлена!");
                    var window = o as Window;
                    window?.Close();
                }
            );
    }
}
