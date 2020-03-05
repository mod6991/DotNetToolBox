using DotNetToolBox.Icons.OpenIconLibrary;
using DotNetToolBox.MVVM;
using DotNetToolBox.RibbonDock;
using DotNetToolBox.RibbonDock.Ribbon;
using DotNetToolBox.Tester.View;
using DotNetToolBox.Tester.ViewModel;
using System;
using System.Windows;

namespace DotNetToolBox.Tester
{
    public static class WindowManager
    {
        public static RibbonDockWindow Window;
        public static RibbonDockWindowViewModel VM;

        private static RibbonApplicationMenuItemViewModel _exitButton;
        private static RibbonButtonViewModel _globalButton;
        private static RibbonButtonViewModel _clearConfigButton;
        private static RibbonButtonViewModel _saveXmlConfigButton;
        private static RibbonButtonViewModel _loadXmlConfigButton;
        private static RibbonButtonViewModel _saveIniConfigButton;
        private static RibbonButtonViewModel _loadIniConfigButton;
        private static RibbonButtonViewModel _dbButton;
        private static RibbonButtonViewModel _startHttpButton;
        private static RibbonButtonViewModel _stopHttpButton;
        private static RibbonButtonViewModel _startTcpButton;
        private static RibbonButtonViewModel _stopTcpButton;
        private static RibbonButtonViewModel _reportingButton;

        private static GlobalViewModel _globalVM;
        private static ConfigurationViewModel _configVM;
        private static DatabaseViewModel _dbVM;
        private static HttpServerViewModel _httpVM;
        private static TcpServerViewModel _tcpVM;
        private static ReportingViewModel _reportingVM;
        private static PropertyViewModel _propertyVM;

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
            _exitButton = new RibbonApplicationMenuItemViewModel("Exit");
            _exitButton.Command = new RelayCommand(Exit, ReturnTrue);
            _exitButton.ImageSource = PngIcons.GetIcon(IconName.Exit, IconSize.Size32);
            VM.ApplicationMenuItems.Add(_exitButton);



            _globalButton = new RibbonButtonViewModel("Start tests");
            _globalButton.Command = new RelayCommand((param)=> { }, ReturnTrue);
            _globalButton.LargeImage = PngIcons.GetIcon(IconName.Play, IconSize.Size32);
            VM.AddRibbonButton("Global", "Global", "", _globalButton);

            _clearConfigButton = new RibbonButtonViewModel("Clear");
            _clearConfigButton.LargeImage = PngIcons.GetIcon(IconName.Clear, IconSize.Size32);
            VM.AddRibbonButton("Configuration", "Configuration", "", _clearConfigButton);
            _saveXmlConfigButton = new RibbonButtonViewModel("Save XML config");
            _saveXmlConfigButton.LargeImage = PngIcons.GetIcon(IconName.Save, IconSize.Size32);
            VM.AddRibbonButton("Configuration", "Configuration", "XML", _saveXmlConfigButton);
            _loadXmlConfigButton = new RibbonButtonViewModel("Load XML config");
            _loadXmlConfigButton.LargeImage = PngIcons.GetIcon(IconName.DocumentOpen, IconSize.Size32);
            VM.AddRibbonButton("Configuration", "Configuration", "XML", _loadXmlConfigButton);
            _saveIniConfigButton = new RibbonButtonViewModel("Save INI config");
            _saveIniConfigButton.LargeImage = PngIcons.GetIcon(IconName.Save, IconSize.Size32);
            VM.AddRibbonButton("Configuration", "Configuration", "INI", _saveIniConfigButton);
            _loadIniConfigButton = new RibbonButtonViewModel("Load INI config");
            _loadIniConfigButton.LargeImage = PngIcons.GetIcon(IconName.DocumentOpen, IconSize.Size32);
            VM.AddRibbonButton("Configuration", "Configuration", "INI", _loadIniConfigButton);

            _dbButton = new RibbonButtonViewModel("Start test");
            _dbButton.Command = new RelayCommand((param)=> { }, ReturnTrue);
            _dbButton.LargeImage = PngIcons.GetIcon(IconName.Play, IconSize.Size32);
            VM.AddRibbonButton("Database", "Database", "", _dbButton);

            _startHttpButton = new RibbonButtonViewModel("Start server");
            _startHttpButton.Command = new RelayCommand((param)=> { }, ReturnTrue);
            _startHttpButton.LargeImage = PngIcons.GetIcon(IconName.Play, IconSize.Size32);
            VM.AddRibbonButton("HttpServer", "HttpServer", "", _startHttpButton);
            _stopHttpButton = new RibbonButtonViewModel("Stop server");
            _stopHttpButton.Command = new RelayCommand((param)=> { }, ReturnTrue);
            _stopHttpButton.LargeImage = PngIcons.GetIcon(IconName.Close, IconSize.Size32);
            VM.AddRibbonButton("HttpServer", "HttpServer", "", _stopHttpButton);

            _startTcpButton = new RibbonButtonViewModel("Start test");
            _startTcpButton.Command = new RelayCommand((param)=> { }, ReturnTrue);
            _startTcpButton.LargeImage = PngIcons.GetIcon(IconName.Play, IconSize.Size32);
            VM.AddRibbonButton("TcpServer", "TcpServer", "", _startTcpButton);
            _stopTcpButton = new RibbonButtonViewModel("Stop test");
            _stopTcpButton.Command = new RelayCommand((param)=> { }, ReturnTrue);
            _stopTcpButton.LargeImage = PngIcons.GetIcon(IconName.Close, IconSize.Size32);
            VM.AddRibbonButton("TcpServer", "TcpServer", "", _stopTcpButton);

            _reportingButton = new RibbonButtonViewModel("Start test");
            _reportingButton.Command = new RelayCommand((param)=> { }, ReturnTrue);
            _reportingButton.LargeImage = PngIcons.GetIcon(IconName.Play, IconSize.Size32);
            VM.AddRibbonButton("Reporting", "Reporting", "", _reportingButton);
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
            _globalVM = new GlobalViewModel(VM);

            _configVM = new ConfigurationViewModel(VM);
            _clearConfigButton.Command = new RelayCommand(_configVM.ClearConfig, ReturnTrue);
            _loadXmlConfigButton.Command = new RelayCommand(_configVM.LoadXmlConfig, _configVM.ReturnTrue);
            _saveXmlConfigButton.Command = new RelayCommand(_configVM.SaveXmlConfig, _configVM.ReturnTrue);
            _loadIniConfigButton.Command = new RelayCommand(_configVM.LoadIniConfig, _configVM.ReturnTrue);
            _saveIniConfigButton.Command = new RelayCommand(_configVM.SaveIniConfig, _configVM.ReturnTrue);

            _dbVM = new DatabaseViewModel(VM);
            _httpVM = new HttpServerViewModel(VM);
            _tcpVM = new TcpServerViewModel(VM);
            _reportingVM = new ReportingViewModel(VM);
            _propertyVM = new PropertyViewModel(VM);

            VM.Documents.Add(_globalVM);
            VM.Documents.Add(_configVM);
            VM.Documents.Add(_dbVM);
            VM.Documents.Add(_httpVM);
            VM.Documents.Add(_tcpVM);
            VM.Documents.Add(_reportingVM);
            VM.Tools.Add(_propertyVM);
        }

        private static void Loaded(object param)
        {
            try
            {

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static void Unloaded(object param)
        {
            try
            {
                Application.Current.Shutdown();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
