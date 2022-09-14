using System.Windows.Input;

namespace ImeSense.Helpers.Mvvm {
    public interface IRelayCommand : ICommand {
        void NotifyCanExecuteChanged();
    }

    public interface IRelayCommand<in T> : IRelayCommand {
        bool CanExecute(T parameter);
        void Execute(T parameter);
    }
}
