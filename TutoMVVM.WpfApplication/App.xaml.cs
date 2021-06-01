using System.Windows;
using TutoMVVM.WpfApplication.ViewModels;

namespace TutoMVVM.WpfApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.DataContext = new MainVIewModel();
            window.Show();

            base.OnStartup(e);
        }
    }
}
