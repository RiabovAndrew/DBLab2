using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfBDLab2.DataBase.Tables;
using WpfBDLab2.VM.Forms;
using WpfBDLab2.Windows;

namespace WpfBDLab2.VM.MainFormVM
{
    class MainFormVM : BaseVM.BaseVM {
        private ICommand _openCitiesTableCommand;

        public ICommand OpenCitiesTableCommand =>
            _openCitiesTableCommand ??= new RelayCommand.RelayCommand((o) => {
                new CitiesWindow().ShowDialog();
            });
    }
}
