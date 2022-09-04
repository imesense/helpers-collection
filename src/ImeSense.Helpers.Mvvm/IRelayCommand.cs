using System.Windows.Input;

namespace ImeSense.Helpers.Mvvm {
    public interface IRelayCommand : ICommand {
        void NotifyCanExecuteChanged();
    }
}
