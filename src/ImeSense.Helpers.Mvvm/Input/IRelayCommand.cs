using System.Windows.Input;

namespace ImeSense.Helpers.Mvvm.Input {
    public interface IRelayCommand : ICommand {
        void NotifyCanExecuteChanged();
    }
}
