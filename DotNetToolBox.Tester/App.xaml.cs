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
        public static RibbonDockWindowViewModel VM;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                RibbonDock.RibbonDockWindow win = RibbonDockWindowViewModel.GetNewWindow();
                VM = (RibbonDockWindowViewModel)win.DataContext;

                RibbonButtonViewModel button = new RibbonButtonViewModel("test button");
                button.Command = new RelayCommand(ButtonClick, ReturnTrue);
                button.LargeImage = PngIcons.GetIcon(IconName.Home, IconSize.Size32);
                VM.AddRibbonButton("HomeTab", "1st group", button);

                RibbonButtonViewModel button2 = new RibbonButtonViewModel("test button 2");
                button2.Command = new RelayCommand(ButtonClick2, ReturnTrue);
                button2.LargeImage = PngIcons.GetIcon(IconName.Help, IconSize.Size32);
                VM.AddRibbonButton("HomeTab", "1st group", button2);

                RibbonButtonViewModel button3 = new RibbonButtonViewModel("Test button 3");
                button3.Command = new RelayCommand(ButtonClick3, ReturnTrue);
                button3.LargeImage = PngIcons.GetIcon(IconName.AppointmentSoon, IconSize.Size32);
                button3.ToolTipTitle = "title";
                button3.ToolTipDescription = "description\nblah blah\nblih bloh";
                button3.ToolTipImage = PngIcons.GetIcon(IconName.AppointmentSoon, IconSize.Size16);
                VM.AddRibbonButton("2nd tab", "2nd", "3rd group", button3);

                RibbonApplicationMenuItemViewModel exitButton = new RibbonApplicationMenuItemViewModel("Exit");
                exitButton.Command = new RelayCommand(Exit, ReturnTrue);
                exitButton.ImageSource = PngIcons.GetIcon(IconName.Exit, IconSize.Size32);
                VM.ApplicationMenuItems.Add(exitButton);

                DockDataTemplateSelector.RegisterDockViewModel(typeof(ConfigurationViewModel), typeof(ConfigurationView));
                DockDataTemplateSelector.RegisterDockViewModel(typeof(DatabaseViewModel), typeof(DatabaseView));
                DockDataTemplateSelector.RegisterDockViewModel(typeof(GlobalViewModel), typeof(GlobalView));
                DockDataTemplateSelector.RegisterDockViewModel(typeof(HttpServerViewModel), typeof(HttpServerView));
                DockDataTemplateSelector.RegisterDockViewModel(typeof(ReportingViewModel), typeof(ReportingView));
                DockDataTemplateSelector.RegisterDockViewModel(typeof(TcpServerViewModel), typeof(TcpServerView));
                DockDataTemplateSelector.RegisterDockViewModel(typeof(PropertyViewModel), typeof(PropertyView));

                VM.Documents.Add(new GlobalViewModel(VM));
                VM.Documents.Add(new DatabaseViewModel(VM));
                VM.Documents.Add(new ConfigurationViewModel(VM));
                VM.Documents.Add(new HttpServerViewModel(VM));
                VM.Documents.Add(new TcpServerViewModel(VM));
                VM.Documents.Add(new ReportingViewModel(VM));
                VM.Tools.Add(new PropertyViewModel(VM));

                VM.Theme = new Xceed.Wpf.AvalonDock.Themes.MetroTheme();

                VM.ActiveDocumentChanged += VM_ActiveDocumentChanged;
                VM.Title = "Test";

                VM.LoadedCommand = new RelayCommand(Loaded, ReturnTrue);
                VM.UnloadedCommand = new RelayCommand(Unloaded, ReturnTrue);

                win.WindowState = WindowState.Maximized;
                win.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private void VM_ActiveDocumentChanged(object sender, ActiveDocumentChangedEventArgs e)
        {
            if(e.Document is HttpServerViewModel)
            {
                VM.Tabs.First(x => x.Id == "2nd").IsSelected = true;
            }
            else
            {

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

        private void ButtonClick3(object param)
        {
            MessageBox.Show("click 3");
        }

        private void Exit(object param)
        {
            VM.VisualObject.Close();
        }

        private void Loaded(object param)
        {
            VM.SetActiveDocument("Database");

        }

        private void Unloaded(object param)
        {

        }
    }
}
