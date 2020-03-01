using DotNetToolBox.Icons.OpenIconLibrary;
using DotNetToolBox.MVVM;
using DotNetToolBox.RibbonDock;
using DotNetToolBox.RibbonDock.Ribbon;
using DotNetToolBox.Tester.View;
using DotNetToolBox.Tester.ViewModel;
using System;
using System.Linq;
using System.Windows;

namespace DotNetToolBox.Tester
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected RibbonDockWindowViewModel vm;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                RibbonDock.RibbonDockWindow win = RibbonDockWindowViewModel.GetNewWindow();
                vm = (RibbonDockWindowViewModel)win.DataContext;

                RibbonButtonViewModel button = new RibbonButtonViewModel("test button");
                button.Command = new RelayCommand(ButtonClick, ReturnTrue);
                button.LargeImage = PngIcons.GetIcon(IconName.Home, IconSize.Size32);
                vm.AddRibbonButton("HomeTab", "1st group", button);

                RibbonButtonViewModel button2 = new RibbonButtonViewModel("test button 2");
                button2.Command = new RelayCommand(ButtonClick2, ReturnTrue);
                button2.LargeImage = PngIcons.GetIcon(IconName.Help, IconSize.Size32);
                vm.AddRibbonButton("HomeTab", "1st group", button2);

                DockDataTemplateSelector.RegisterDockViewModel(typeof(ConfigurationViewModel), typeof(ConfigurationView));
                DockDataTemplateSelector.RegisterDockViewModel(typeof(DatabaseViewModel), typeof(DatabaseView));
                DockDataTemplateSelector.RegisterDockViewModel(typeof(GlobalViewModel), typeof(GlobalView));
                DockDataTemplateSelector.RegisterDockViewModel(typeof(HttpServerViewModel), typeof(HttpServerView));
                DockDataTemplateSelector.RegisterDockViewModel(typeof(ReportingViewModel), typeof(ReportingView));
                DockDataTemplateSelector.RegisterDockViewModel(typeof(TcpServerViewModel), typeof(TcpServerView));

                vm.Documents.Add(new GlobalViewModel(vm));
                vm.Documents.Add(new DatabaseViewModel(vm));
                vm.Documents.Add(new ConfigurationViewModel(vm));
                vm.Documents.Add(new HttpServerViewModel(vm));
                vm.Documents.Add(new TcpServerViewModel(vm));
                vm.Documents.Add(new ReportingViewModel(vm));

                vm.ActiveDocument = vm.Documents.First(x => x.ContentId == "Global");

                win.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private bool ReturnTrue(object param)
        {
            return true;
        }

        private void ButtonClick(object param)
        {

        }

        private void ButtonClick2(object param)
        {
            MessageBox.Show("Test !!!!!");
        }
    }

    
}
