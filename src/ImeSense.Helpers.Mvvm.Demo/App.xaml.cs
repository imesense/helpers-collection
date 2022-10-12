using System.Windows;

using ImeSense.Helpers.Mvvm.Demo.Views;

namespace ImeSense.Helpers.Mvvm.Demo;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application {
    protected override void OnStartup(StartupEventArgs eventArgs) {
        base.OnStartup(eventArgs);

        var mainWindow = new MainWindow();
        mainWindow.Show();
    }
}
