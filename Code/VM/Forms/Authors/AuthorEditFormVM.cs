using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfBDLab2.VM.Forms.Authors
{
    class AuthorEditFormVM : BaseVM.BaseVM
    {
        private int _id;
        private string _name;
        private int _year;
        private ICommand _cancelCommand;
        private ICommand _editCommand;

        public AuthorEditFormVM(int id, string name, int year)
        {
            Id = id;
            Name = name;
            Year = year;
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

        public int Year
        {
            get => _year;
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));
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
                    new DataBase.Tables.Authors(DbConnector).EditByID(Id, new DataBase.Tables.Authors(DbConnector, Name, Year));
                    var ms = MessageBox.Show("Запись была обновлена!");
                    var window = o as Window;
                    window?.Close();
                }
            );
    }
}
