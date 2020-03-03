using DotNetToolBox.Icons.OpenIconLibrary;
using DotNetToolBox.MVVM;
using DotNetToolBox.RibbonDock;
using DotNetToolBox.RibbonDock.Ribbon;
using DotNetToolBox.Tester.View;
using DotNetToolBox.Tester.ViewModel;
using System.Windows;

namespace DotNetToolBox.Tester
{
    public static class WindowManager
    {
        public static RibbonDockWindow Window;
        public static RibbonDockWindowViewModel VM;

        public static void Init()
        {
            Window = RibbonDockWindowViewModel.GetNewWindow();
            VM = (RibbonDockWindowViewModel)WindowManager.Window.DataContext;

            VM.ActiveDocumentChanged += VM_ActiveDocumentChanged;

            VM.LoadedCommand = new RelayCommand(Loaded, ReturnTrue);
            VM.UnloadedCommand = new RelayCommand(Unloaded, ReturnTrue);
        }

        private static void VM_ActiveDocumentChanged(object sender, ActiveDocumentChangedEventArgs e)
        {
            if (e.Document is GlobalViewModel)
            {
                VM.SelectRibbonTabById("Global");
            }
            else if (e.Document is ConfigurationViewModel)
            {
                VM.SelectRibbonTabById("Configuration");
            }
            else if (e.Document is DatabaseViewModel)
            {
                VM.SelectRibbonTabById("Database");
            }
            else if (e.Document is HttpServerViewModel)
            {
                VM.SelectRibbonTabById("HttpServer");
            }
            else if (e.Document is TcpServerViewModel)
            {
                VM.SelectRibbonTabById("TcpServer");
            }
            else if (e.Document is ReportingViewModel)
            {
                VM.SelectRibbonTabById("Reporting");
            }
        }

        public static void AddRibbonButtons()
        {
            RibbonApplicationMenuItemViewModel exitButton = new RibbonApplicationMenuItemViewModel("Exit");
            exitButton.Command = new RelayCommand(Exit, ReturnTrue);
            exitButton.ImageSource = PngIcons.GetIcon(IconName.Exit, IconSize.Size32);
            VM.ApplicationMenuItems.Add(exitButton);



            RibbonButtonViewModel globalButton = new RibbonButtonViewModel("Start tests");
            globalButton.Command = new RelayCommand((param)=> { }, ReturnTrue);
            globalButton.LargeImage = PngIcons.GetIcon(IconName.Play, IconSize.Size32);
            VM.AddRibbonButton("Global", "Global", "", globalButton);

            RibbonButtonViewModel saveConfigButton = new RibbonButtonViewModel("Save config");
            saveConfigButton.Command = new RelayCommand((param)=> { ((ConfigurationViewModel)VM.GetDocument("Configuration")).SaveConfig(null); }, ReturnTrue);
            saveConfigButton.LargeImage = PngIcons.GetIcon(IconName.Save, IconSize.Size32);
            VM.AddRibbonButton("Configuration", "Configuration", "", saveConfigButton);
            RibbonButtonViewModel loadConfigButton = new RibbonButtonViewModel("Load config");
            loadConfigButton.Command = new RelayCommand((param)=> { ((ConfigurationViewModel)VM.GetDocument("Configuration")).LoadConfig(null); }, ReturnTrue);
            loadConfigButton.LargeImage = PngIcons.GetIcon(IconName.DocumentOpen, IconSize.Size32);
            VM.AddRibbonButton("Configuration", "Configuration", "", loadConfigButton);

            RibbonButtonViewModel dbButton = new RibbonButtonViewModel("Start test");
            dbButton.Command = new RelayCommand((param)=> { }, ReturnTrue);
            dbButton.LargeImage = PngIcons.GetIcon(IconName.Play, IconSize.Size32);
            VM.AddRibbonButton("Database", "Database", "", dbButton);

            RibbonButtonViewModel StartHttpButton = new RibbonButtonViewModel("Start server");
            StartHttpButton.Command = new RelayCommand((param)=> { }, ReturnTrue);
            StartHttpButton.LargeImage = PngIcons.GetIcon(IconName.Play, IconSize.Size32);
            VM.AddRibbonButton("HttpServer", "HttpServer", "", StartHttpButton);
            RibbonButtonViewModel stopHttpButton = new RibbonButtonViewModel("Stop server");
            stopHttpButton.Command = new RelayCommand((param)=> { }, ReturnTrue);
            stopHttpButton.LargeImage = PngIcons.GetIcon(IconName.Close, IconSize.Size32);
            VM.AddRibbonButton("HttpServer", "HttpServer", "", stopHttpButton);

            RibbonButtonViewModel startTcpButton = new RibbonButtonViewModel("Start test");
            startTcpButton.Command = new RelayCommand((param)=> { }, ReturnTrue);
            startTcpButton.LargeImage = PngIcons.GetIcon(IconName.Play, IconSize.Size32);
            VM.AddRibbonButton("TcpServer", "TcpServer", "", startTcpButton);
            RibbonButtonViewModel stopTcpButton = new RibbonButtonViewModel("Stop test");
            stopTcpButton.Command = new RelayCommand((param)=> { }, ReturnTrue);
            stopTcpButton.LargeImage = PngIcons.GetIcon(IconName.Close, IconSize.Size32);
            VM.AddRibbonButton("TcpServer", "TcpServer", "", stopTcpButton);

            RibbonButtonViewModel reportingButton = new RibbonButtonViewModel("Start test");
            reportingButton.Command = new RelayCommand((param)=> { }, ReturnTrue);
            reportingButton.LargeImage = PngIcons.GetIcon(IconName.Play, IconSize.Size32);
            VM.AddRibbonButton("Reporting", "Reporting", "", reportingButton);
        }

        public static void RegisterViewModels()
        {
            DockDataTemplateSelector.RegisterDockViewModel(typeof(GlobalViewModel), typeof(GlobalView));
            DockDataTemplateSelector.RegisterDockViewModel(typeof(ConfigurationViewModel), typeof(ConfigurationView));
            DockDataTemplateSelector.RegisterDockViewModel(typeof(DatabaseViewModel), typeof(DatabaseView));
            DockDataTemplateSelector.RegisterDockViewModel(typeof(HttpServerViewModel), typeof(HttpServerView));
            DockDataTemplateSelector.RegisterDockViewModel(typeof(TcpServerViewModel), typeof(TcpServerView));
            DockDataTemplateSelector.RegisterDockViewModel(typeof(ReportingViewModel), typeof(ReportingView));
            DockDataTemplateSelector.RegisterDockViewModel(typeof(PropertyViewModel), typeof(PropertyView));
        }

        public static void AddDocuments()
        {
            VM.Documents.Add(new GlobalViewModel(VM));
            VM.Documents.Add(new ConfigurationViewModel(VM));
            VM.Documents.Add(new DatabaseViewModel(VM));
            VM.Documents.Add(new HttpServerViewModel(VM));
            VM.Documents.Add(new TcpServerViewModel(VM));
            VM.Documents.Add(new ReportingViewModel(VM));
            VM.Tools.Add(new PropertyViewModel(VM));
        }

        private static void Loaded(object param)
        {

        }

        private static void Unloaded(object param)
        {

        }

        private static bool ReturnTrue(object param)
        {
            return true;
        }

        private static void Exit(object param)
        {
            VM.ActiveDocumentChanged -= VM_ActiveDocumentChanged;
            Window.Close();
            Application.Current.Shutdown();
        }
    }
}
