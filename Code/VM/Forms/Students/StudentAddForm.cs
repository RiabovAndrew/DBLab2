using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfBDLab2.VM.Forms.Students
{
    class StudentAddForm : BaseVM.BaseVM
    {
        private int _id;
        private string _name;
        private int _idSpecFac;
        private int _year;
        private ICommand _cancelCommand;
        private ICommand _addCommand;

        public StudentAddForm(int id, string name, int idSpecFac, int year)
        {
            Id = id;
            Name = name;
            IdSpecFac = idSpecFac;
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

        public int IdSpecFac
        {
            get => _idSpecFac;
            set
            {
                _idSpecFac = value;
                OnPropertyChanged(nameof(IdSpecFac));
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

        public ICommand AddCommand =>
            _addCommand ??= new RelayCommand.RelayCommand((o) => {
                    MessageBox.Show(new DataBase.Tables.Students(DbConnector, Name, IdSpecFac, Year).Insert()
                        ? "Новая запись была добавлена!"
                        : "Внешние ключи заданы неверно!"
                    );

                    var window = o as Window;
                    window?.Close();
                }
            );
    }
}
