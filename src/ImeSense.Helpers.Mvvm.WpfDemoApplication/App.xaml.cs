using System.Windows;

using ImeSense.Helpers.Mvvm.WpfDemoApplication.Views;

namespace ImeSense.Helpers.Mvvm.WpfDemoApplication {
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
}
