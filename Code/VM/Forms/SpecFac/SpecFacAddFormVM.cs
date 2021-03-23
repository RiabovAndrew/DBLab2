using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfBDLab2.VM.Forms.SpecFac {
    class SpecFacAddFormVM : BaseVM.BaseVM {
        private int _id;
        private int _idSpec;
        private int _idFac;
        private ICommand _cancelCommand;
        private ICommand _addCommand;

        public SpecFacAddFormVM(int id, int idSpec, int idFac) {
            Id = id;
            IdSpec = idSpec;
            IdFac = idFac;
        }

        public int Id {
            get => _id;
            set {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int IdSpec {
            get => _idSpec;
            set {
                _idSpec = value;
                OnPropertyChanged(nameof(IdSpec));
            }
        }

        public int IdFac {
            get => _idFac;
            set {
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

        public ICommand AddCommand =>
            _addCommand ??= new RelayCommand.RelayCommand((o) => {
                    MessageBox.Show(new DataBase.Tables.Spec_Fac(DbConnector, IdSpec, IdFac).Insert()
                        ? "Новая запись была добавлена!"
                        : "Внешние ключи заданы неверно!"
                    );

                    var window = o as Window;
                    window?.Close();
                }
            );
    }
}