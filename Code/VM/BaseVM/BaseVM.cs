using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfBDLab2.Annotations;
using WpfBDLab2.DataBase.DbConnector;

namespace WpfBDLab2.VM.BaseVM {
    internal class BaseVM : INotifyPropertyChanged {
        protected DBConnector DbConnector;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}