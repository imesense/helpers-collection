using System;

namespace ImeSense.Helpers.Mvvm {
    public class RelayCommand : IRelayCommand {
        private readonly Action _execute;

        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute) {
            if (execute == null) {
                throw new ArgumentNullException(ExceptionMessages.ExecuteIsNull);
            }

            _execute = execute;
        }

        public RelayCommand(Action execute, Func<bool> canExecute) {
            if (execute == null) {
                throw new ArgumentNullException(ExceptionMessages.ExecuteIsNull);
            }
            if (canExecute == null) {
                throw new ArgumentNullException(ExceptionMessages.CanExecuteIsNull);
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter) {
            _execute();
        }

        public void NotifyCanExecuteChanged() {
            if (CanExecuteChanged != null) {
                CanExecuteChanged.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
