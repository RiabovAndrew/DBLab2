using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private string _readAllString;
        private int _id;

        public CitiesFormVM() {
            Cities = new DataBase.Tables.Cities(new DBConnector());
            _readAllString =
                ListConvertor.ConvertToString(new TableBase().GetColumnNames(Cities, new DBConnector().DBConnection), "\t") + "\n\n";
            _readAllString += ListConvertor.ConvertToString(new TableBase().ReadAllRowsFromTable(Cities, new DBConnector().DBConnection));
        }

        public DataBase.Tables.Cities Cities { get; set; }

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
            new CityAddWindow(2, "123").ShowDialog();
        });
    }
}
