using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfBDLab2.Annotations;

namespace WpfBDLab2.VM.BaseVM {
    internal class BaseVM : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}