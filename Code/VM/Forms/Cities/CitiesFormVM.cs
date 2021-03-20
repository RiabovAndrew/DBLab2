using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfBDLab2.DataBase.DbConnector;
using WpfBDLab2.DataBase.Father;
using WpfBDLab2.DataBase.Tables;
using WpfBDLab2.DataConvertor;
using WpfBDLab2.VM.Forms.Cities;
using WpfBDLab2.Windows.Cities;

namespace WpfBDLab2.VM.Forms
{
    class CitiesFormVM : BaseVM.BaseVM {
        private ICommand _addCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private string _readAllString;
        private int _id;

        public CitiesFormVM() {
            Cities = new DataBase.Tables.Cities(new DBConnector());
            readAllString();
            DbConnector = new DBConnector();
        }

        public DataBase.Tables.Cities Cities { get; set; }

        private void readAllString() {
            ReadAllString =
                ListConvertor.ConvertToString(new TableBase().GetColumnNames(Cities, new DBConnector().DBConnection), "\t") + "\n\n";
            ReadAllString += ListConvertor.ConvertToString(new TableBase().ReadAllRowsFromTable(Cities, new DBConnector().DBConnection));
        }

        public int Id {
            get => _id;
            set {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string ReadAllString {
            get => _readAllString;
            set {
                _readAllString = value;
                OnPropertyChanged(nameof(ReadAllString));
            }
        }
        public ICommand AddCommand => _addCommand ??= new RelayCommand.RelayCommand((o) => {
            new CityAddWindow(Id, new TableBase().FindByIdByColumn(Id, "city_name", Cities, DbConnector.DBConnection)).ShowDialog();
            readAllString();
        });

        public ICommand EditCommand => _editCommand ??= new RelayCommand.RelayCommand((o) => {
            if (new TableBase().FindByIdByColumn(Id, "id", Cities, DbConnector.DBConnection) == "") {
                MessageBox.Show("Нет такого Id!");
            }
            else {
                new CityEditWindow(Id, new TableBase().FindByIdByColumn(Id, "city_name", Cities, DbConnector.DBConnection)).ShowDialog();
                readAllString();
            }
        });

        public ICommand DeleteCommand => _deleteCommand ??= new RelayCommand.RelayCommand((o) => {
            MessageBox.Show(new TableBase().FindByIdByColumn(Id, "id", Cities, DbConnector.DBConnection) == ""
                ? "Нет такого Id!"
                : "Запись удалена!"
            );
            new TableBase().DeleteById(Id, new DataBase.Tables.Cities(), DbConnector.DBConnection);
            readAllString();
        });
    }
}
