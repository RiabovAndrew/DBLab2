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

namespace WpfBDLab2.VM.Forms
{
    class CitiesFormVM : BaseVM.BaseVM {
        private ICommand _showDbCommand;
        private string _readAllString;

        public CitiesFormVM() {
            Cities = new Cities(new DBConnector());
            _readAllString =
                ListConvertor.ConvertToString(new TableBase().GetColumnNames(Cities, new DBConnector().DBConnection), "\t") + "\n\n";
            _readAllString += ListConvertor.ConvertToString(new TableBase().ReadAllRowsFromTable(Cities, new DBConnector().DBConnection));
        }

        public Cities Cities { get; set; }

        public string ReadAllString {
            get => _readAllString;
            set {
                _readAllString = value;
                OnPropertyChanged(nameof(ReadAllString));
            }
        }
        public ICommand ShowDbCommand => _showDbCommand ??= new RelayCommand.RelayCommand((o) => {
            _readAllString = ListConvertor.ConvertToString(new TableBase().ReadAllRowsFromTable(Cities, new DBConnector().DBConnection));
        });
    }
}
