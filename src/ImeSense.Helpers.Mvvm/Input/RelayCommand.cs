using System;

namespace ImeSense.Helpers.Mvvm.Input {
    /// <summary>
    /// Command to relay its functionality to other objects by invoking delegates
    /// </summary>
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

        /// <summary>
        /// Raises on changes whether the command should be executed or not
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Initializes new instance of <see cref="RelayCommand" /> class that can always execute
        /// </summary>
        /// <param name="execute">Execution logic</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="execute" /> is <see langword="null" /></exception>
        public RelayCommand(Action execute) {
            if (execute == null) {
                throw new ArgumentNullException("execute", "Delegate \"execute\" can not be null!");
            }

            _execute = execute;
        }

        /// <summary>
        /// Initializes new instance of <see cref="RelayCommand" /> class
        /// </summary>
        /// <param name="execute">Execution logic</param>
        /// <param name="canExecute">Execution status</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="execute" /> or <paramref name="canExecute" /> are <see langword="null" /></exception>
        public RelayCommand(Action execute, Func<bool> canExecute) {
            if (execute == null) {
                throw new ArgumentNullException("execute", "Delegate \"execute\" can not be null!");
            }
            if (canExecute == null) {
                throw new ArgumentNullException("canExecute", "Delegate \"canExecute\" can not be null!");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state
        /// </summary>
        /// <param name="parameter">Data used by the command</param>
        /// <returns><see langword="true" /> if this command can be executed<br /><see langword="false" /> otherwise</returns>
        public bool CanExecute(object parameter) {
            var func = _canExecute;
            if (func == null) {
                return true;
            }
            return func();
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked
        /// </summary>
        /// <param name="parameter">Data used by the command</param>
        public void Execute(object parameter) {
            _execute();
        }

        /// <summary>
        /// Notifies of changes in
        /// <see cref="CanExecute(object)" />
        /// </summary>
        public void NotifyCanExecuteChanged() {
            var canExecuteChanged = CanExecuteChanged;
            if (canExecuteChanged != null) {
                canExecuteChanged(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Generic command to relay its functionality to other objects by invoking delegates
    /// </summary>
    /// <typeparam name="T">Type of parameter being passed as input to the callbacks</typeparam>
    public class RelayCommand<T> : IRelayCommand<T> {
        /// <summary>
        /// <see cref="Action{T}" /> delegate for invoke on using
        /// <see cref="Execute(T)" />
        /// </summary>
        private readonly Action<T> _execute;

        /// <summary>
        /// Optional <see cref="Predicate{T}" /> delegate for invoke on using
        /// <see cref="CanExecute(T)" />
        /// </summary>
        private readonly Predicate<T> _canExecute;

        /// <summary>
        /// Raises on changes whether the command should be executed or not
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Initializes new instance of <see cref="RelayCommand{T}" /> class
        /// </summary>
        /// <param name="execute">Execution logic</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="execute" /> is <see langword="null" /></exception>
        public RelayCommand(Action<T> execute) {
            if (execute == null) {
                throw new ArgumentNullException("execute", "Delegate \"execute\" can not be null!");
            }

            _execute = execute;
        }

        /// <summary>
        /// Initializes new instance of <see cref="RelayCommand{T}" /> class
        /// </summary>
        /// <param name="execute">Execution logic</param>
        /// <param name="canExecute">Execution status</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="execute" /> or <paramref name="canExecute" /> are <see langword="null" /></exception>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute) {
            if (execute == null) {
                throw new ArgumentNullException("execute", "Delegate \"execute\" can not be null!");
            }
            if (canExecute == null) {
                throw new ArgumentNullException("canExecute", "Delegate \"canExecute\" can not be null!");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Notifies of changes in
        /// <see cref="CanExecute(object)" />
        /// </summary>
        public void NotifyCanExecuteChanged() {
            var canExecuteChanged = CanExecuteChanged;
            if (canExecuteChanged != null) {
                canExecuteChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Tries to get command argument of compatible type <typeparamref name="T" /> from input <see cref="object" />
        /// </summary>
        /// <param name="parameter">Input parameter</param>
        /// <param name="result">Resulting <typeparamref name="T" /> value</param>
        /// <returns>Result of checking whether it is possible to get a compatible command argument or not</returns>
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

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state
        /// </summary>
        /// <param name="parameter">Data used by the command</param>
        /// <returns><see langword="true" /> if this command can be executed<br /><see langword="false" /> otherwise</returns>
        public bool CanExecute(T parameter) {
            var predicate = _canExecute;
            if (predicate == null) {
                return true;
            }
            return predicate(parameter);
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state
        /// </summary>
        /// <param name="parameter">Data used by the command</param>
        /// <returns><see langword="true" /> if this command can be executed<br /><see langword="false" /> otherwise</returns>
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

        /// <summary>
        /// Defines the method to be called when the command is invoked
        /// </summary>
        /// <param name="parameter">Data used by the command</param>
        public void Execute(T parameter) {
            _execute(parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked
        /// </summary>
        /// <param name="parameter">Data used by the command</param>
        public void Execute(object parameter) {
            T result;
            if (!TryGetCommandArgument(parameter, out result)) {
                throw new ArgumentException("Parameter \"parameter\" can not be null!", "parameter");
            }
            Execute(result);
        }
    }
}
