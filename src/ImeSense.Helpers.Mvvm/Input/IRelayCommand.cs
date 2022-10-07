using System.Windows.Input;

namespace ImeSense.Helpers.Mvvm.Input {
    /// <summary>
    /// Extends <see cref="ICommand" /> with the ability to raise
    /// <see cref="ICommand.CanExecuteChanged" /> event externally
    /// </summary>
    public interface IRelayCommand : ICommand {
        /// <summary>
        /// Notifies of changes in
        /// <see cref="ICommand.CanExecute" />
        /// </summary>
        void NotifyCanExecuteChanged();
    }

    /// <summary>
    /// Represents more specific version of <see cref="IRelayCommand" />
    /// </summary>
    /// <typeparam name="T">Argument for the interface methods</typeparam>
    public interface IRelayCommand<in T> : IRelayCommand {
        /// <summary>
        /// Provides a strongly-typed variant of <see cref="ICommand.CanExecute(object)" />
        /// </summary>
        /// <param name="parameter">Input parameter</param>
        /// <returns>Whether or not the current command can be executed</returns>
        bool CanExecute(T parameter);

        /// <summary>
        /// Provides a strongly-typed variant of <see cref="ICommand.Execute(object)" />
        /// </summary>
        /// <param name="parameter">Input parameter</param>
        void Execute(T parameter);
    }
}
