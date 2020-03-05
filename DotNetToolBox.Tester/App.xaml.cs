using System;
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

            try
            {
                WindowManager.Init();
                WindowManager.AddRibbonButtons();
                WindowManager.RegisterViewModels();
                WindowManager.AddDocuments();
                
                WindowManager.VM.Theme = new Xceed.Wpf.AvalonDock.Themes.MetroTheme();
                WindowManager.Window.WindowState = WindowState.Maximized;
                WindowManager.VM.Title = "DotNetToolBox.Tester";
                WindowManager.Window.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}
