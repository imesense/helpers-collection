using System;

namespace ImeSense.Helpers.Mvvm.Input {
    public class RelayCommand : IRelayCommand {
        /// <summary>
        /// <see cref="Action" /> delegate for invoke on using
        /// <see cref="Execute" />
        /// </summary>
        private readonly Action _execute;

        /// <summary>
        /// Optional <see cref="Func{TResult}" /> delegate for invoke on using
        /// <see cref="CanExecute" />
        /// </summary>
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute) {
            if (execute == null) {
                throw new ArgumentException("Delegate \"execute\" can not be null!");
            }

            _execute = execute;
        }

        public RelayCommand(Action execute, Func<bool> canExecute) {
            if (execute == null) {
                throw new ArgumentException("Delegate \"execute\" can not be null!");
            }
            if (canExecute == null) {
                throw new ArgumentException("Delegate \"canExecute\" can not be null!");
            }

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
            _execute();
        }

        public void NotifyCanExecuteChanged() {
            var canExecuteChanged = CanExecuteChanged;
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
            if (execute == null) {
                throw new ArgumentNullException();
            }

            _execute = execute;
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute) {
            if (execute == null) {
                throw new ArgumentNullException("Delegate \"execute\" can not be null!");
            }
            if (canExecute == null) {
                throw new ArgumentNullException("Delegate \"execute\" can not be null!");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        public void NotifyCanExecuteChanged() {
            var canExecuteChanged = CanExecuteChanged;
            if (canExecuteChanged != null) {
                canExecuteChanged(this, EventArgs.Empty);
            }
        }

        internal static bool TryGetCommandArgument(object parameter, out T result) {
            if (parameter == null && default(T) == null) {
                result = default(T);
                return true;
            }

            if (parameter is T) {
                T val = (result = (T)parameter);
                return true;
            }

            result = default(T);
            return false;
        }

        public bool CanExecute(T parameter) {
            var predicate = _canExecute;
            if (predicate == null) {
                return true;
            }
            return predicate(parameter);
        }

        public bool CanExecute(object parameter) {
            if (parameter == null && default(T) != null) {
                return false;
            }

            T result;
            if (!TryGetCommandArgument(parameter, out result)) {
                throw new ArgumentException("Parameter \"parameter\" can not be null!", "parameter");
            }

            return CanExecute(result);
        }

        public void Execute(T parameter) {
            _execute(parameter);
        }

        public void Execute(object parameter) {
            T result;
            if (!TryGetCommandArgument(parameter, out result)) {
                throw new ArgumentException("Parameter \"parameter\" can not be null!", "parameter");
            }
            Execute(result);
        }
    }
}
