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
            bool result = false;
            if (_canExecute != null) {
                result = _canExecute.Invoke();
            }
            return result != false;
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

    public class RelayCommand<T> : IRelayCommand<T> {
        private readonly Action<T> _execute;

        private readonly Predicate<T> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<T> execute) {
            if (execute == null) {
                throw new ArgumentNullException(ExceptionMessages.ExecuteIsNull);
            }

            _execute = execute;
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute) {
            if (execute == null) {
                throw new ArgumentNullException(ExceptionMessages.ExecuteIsNull);
            }
            if (canExecute == null) {
                throw new ArgumentNullException(ExceptionMessages.CanExecuteIsNull);
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(T parameter) {
            bool result = false;
            if (_canExecute != null) {
                result = _canExecute.Invoke(parameter);
            }
            return result != false;
        }

        public bool CanExecute(object parameter) {
            if (parameter == null && default(T) != null) {
                return false;
            }

            T result;
            if (!TryGetCommandArgument(parameter, out result)) {
                throw new ArgumentException(ExceptionMessages.ParameterIsNull);
            }
            return CanExecute(result);
        }

        public void Execute(T parameter) {
            _execute(parameter);
        }

        public void Execute(object parameter) {
            T result;
            if (!TryGetCommandArgument(parameter, out result)) {
                throw new ArgumentException(ExceptionMessages.ParameterIsNull);
            }
            Execute(result);
        }

        public void NotifyCanExecuteChanged() {
            if (CanExecuteChanged != null) {
                CanExecuteChanged.Invoke(this, EventArgs.Empty);
            }
        }

        internal static bool TryGetCommandArgument(object parameter, out T result) {
            if (parameter == null && default(T) == null) {
                result = default(T);
                return true;
            }

            if (parameter is T) {
                result = default(T);
                return true;
            }

            result = default(T);
            return false;
        }
    }
}
