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
        /// property
        /// </summary>
        void NotifyCanExecuteChanged();
    }
}
