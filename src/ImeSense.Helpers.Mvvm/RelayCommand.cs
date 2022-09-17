using System;

namespace ImeSense.Helpers.Mvvm {
    public class RelayCommand : IRelayCommand {
        private readonly Action _execute;

        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute) {
            _execute = execute;
        }

        public RelayCommand(Action execute, Func<bool> canExecute) {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) {
            var func = _canExecute;
            if (func == null) {
                return true;
            }
            return func();
        }

        public void Execute(object parameter) {
            if (CanExecute(parameter)) {
                _execute();
            }
        }

        public void NotifyCanExecuteChanged() {
            var canExecuteChanged = this.CanExecuteChanged;
            if (canExecuteChanged != null) {
                canExecuteChanged(this, EventArgs.Empty);
            }
        }
    }

    public class RelayCommand<T> : IRelayCommand<T> {
        private readonly Action<T> _execute;

        private readonly Predicate<T> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<T> execute) {
            _execute = execute;
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute) {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(T parameter) {
            var predicate = _canExecute;
            if (predicate == null) {
                return true;
            }
            return predicate(parameter);
        }

        public bool CanExecute(object parameter) {
            if (default(T) != null && parameter == null) {
                return false;
            }
            return CanExecute((T) parameter);
        }

        public void Execute(T parameter) {
            if (CanExecute(parameter)) {
                _execute(parameter);
            }
        }

        public void Execute(object parameter) {
            Execute((T) parameter);
        }

        public void NotifyCanExecuteChanged() {
            var canExecuteChanged = this.CanExecuteChanged;
            if (canExecuteChanged != null) {
                canExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
