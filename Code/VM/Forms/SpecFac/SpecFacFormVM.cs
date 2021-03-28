using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfBDLab2.DataBase.DbConnector;
using WpfBDLab2.DataBase.Father;
using WpfBDLab2.DataConvertor;
using WpfBDLab2.Windows.SpecFac;
using WpfBDLab2.Windows.Specs;

namespace WpfBDLab2.VM.Forms.SpecFac {
    class SpecFacFormVM : BaseVM.BaseVM {
        private ICommand _addCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private string _readAllString;
        private int _id;
        private int _idSpec;
        private int _idFac;

        public SpecFacFormVM() {
            SpecFac = new DataBase.Tables.Spec_Fac(new DBConnector());
            readAllString();
            DbConnector = new DBConnector();
        }

        public DataBase.Tables.Spec_Fac SpecFac { get; set; }

        private void readAllString() {
            ReadAllString =
                ListConvertor.ConvertToString(new TableBase().GetColumnNames(SpecFac, new DBConnector().DBConnection),
                    " || "
                ) + "\n\n";
            ReadAllString +=
                ListConvertor.ConvertToString(
                    new TableBase().ReadAllRowsFromTable(SpecFac, new DBConnector().DBConnection, " || ")
                );
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

        public string ReadAllString {
            get => _readAllString;
            set {
                _readAllString = value;
                OnPropertyChanged(nameof(ReadAllString));
            }
        }

        public ICommand AddCommand =>
            _addCommand ??= new RelayCommand.RelayCommand((o) => {
                    new SpecFacAddWindow(Id,
                        Convert.ToInt32(
                            new TableBase().FindByIdByColumn(Id, "id_specs", SpecFac, DbConnector.DBConnection) == ""
                                ? 0
                                : new TableBase().FindByIdByColumn(Id, "id_specs", SpecFac, DbConnector.DBConnection)
                        ),
                        Convert.ToInt32(
                            new TableBase().FindByIdByColumn(Id, "id_fac", SpecFac, DbConnector.DBConnection) == ""
                                ? 0
                                : new TableBase().FindByIdByColumn(Id, "id_fac", SpecFac, DbConnector.DBConnection)
                        )
                    ).ShowDialog();
                    readAllString();
                }
            );

        public ICommand EditCommand =>
            _editCommand ??= new RelayCommand.RelayCommand((o) => {
                    if (new TableBase().FindByIdByColumn(Id, "id", SpecFac, DbConnector.DBConnection) == "") {
                        MessageBox.Show("Нет такого Id!");
                    }
                    else {
                        new SpecFacEditWindow(Id,
                            Convert.ToInt32(
                                new TableBase().FindByIdByColumn(Id, "id_specs", SpecFac, DbConnector.DBConnection) == ""
                                    ? 0
                                    : new TableBase().FindByIdByColumn(Id, "id_specs", SpecFac, DbConnector.DBConnection)
                            ),
                            Convert.ToInt32(
                                new TableBase().FindByIdByColumn(Id, "id_fac", SpecFac, DbConnector.DBConnection) == ""
                                    ? 0
                                    : new TableBase().FindByIdByColumn(Id, "id_fac", SpecFac, DbConnector.DBConnection)
                            )
                        ).ShowDialog();
                    readAllString();
                    }
                }
            );

        public ICommand DeleteCommand =>
            _deleteCommand ??= new RelayCommand.RelayCommand((o) => {
                    MessageBox.Show(new TableBase().FindByIdByColumn(Id, "id", SpecFac, DbConnector.DBConnection) == ""
                        ? "Нет такого Id!"
                        : "Запись удалена!"
                    );
                    new TableBase().DeleteById(Id, new DataBase.Tables.Spec_Fac(), DbConnector.DBConnection);
                    readAllString();
                }
            );
    }
}