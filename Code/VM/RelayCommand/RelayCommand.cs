using System;
using System.Windows.Input;

namespace WpfBDLab2.VM.RelayCommand {
    internal class RelayCommand : ICommand {
        private readonly Action<object> _action;
        private readonly Func<object, bool> _canAction;

        public RelayCommand(Action<object> action, Func<object, bool> canAction = null) {
            _action = action;
            _canAction = canAction;
        }

        public bool CanExecute(object parameter) { return _canAction?.Invoke(parameter) ?? true; }

        public void Execute(object parameter) { _action(parameter); }

        public event EventHandler CanExecuteChanged {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}