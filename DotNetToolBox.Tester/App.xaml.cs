using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DotNetToolBox.Tester
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow window = new MainWindow();
            ViewModel.MainWindowViewModel vm = new ViewModel.MainWindowViewModel(window);
            window.DataContext = vm;
            window.Show();
        }
    }
}
