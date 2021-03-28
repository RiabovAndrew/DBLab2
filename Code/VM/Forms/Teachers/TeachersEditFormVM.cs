using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfBDLab2.VM.Forms.Teachers
{
    class TeachersEditFormVM : BaseVM.BaseVM
    {
        private int _id;
        private string _name;
        private int _idFac;
        private ICommand _cancelCommand;
        private ICommand _editCommand;

        public TeachersEditFormVM(int id, string name, int idFac)
        {
            Id = id;
            Name = name;
            IdFac = idFac;
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

        public int IdFac
        {
            get => _idFac;
            set
            {
                _idFac = value;
                OnPropertyChanged(nameof(IdFac));
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
                    var ms = MessageBox.Show(new DataBase.Tables.Teachers(DbConnector).EditByID(Id,
                            new DataBase.Tables.Teachers(DbConnector, Name, IdFac)
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
