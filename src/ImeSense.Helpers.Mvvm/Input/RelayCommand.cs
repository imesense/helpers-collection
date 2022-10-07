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
}
