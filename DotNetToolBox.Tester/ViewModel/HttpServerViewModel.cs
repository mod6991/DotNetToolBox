using DotNetToolBox.Icons.OpenIconLibrary;
using DotNetToolBox.MVVM;
using DotNetToolBox.Net;
using DotNetToolBox.RibbonDock;
using DotNetToolBox.RibbonDock.Dock;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace DotNetToolBox.Tester.ViewModel
{
    public class HttpServerViewModel : DockingDocumentViewModelBase
    {
        private RibbonDockWindowViewModel _rdVM;
        private ICommand _closeCommand;
        private ICommand _closeAllButThisCommand;
        private ICommand _startCommand;
        private ICommand _stopCommand;
        private int _port = 8885;
        private HttpServer _server;
        private ObservableCollection<HttpListenerContext> _eventList;
        private HttpListenerContext _selectedEvent;

        #region Constructor

        public HttpServerViewModel(RibbonDockWindowViewModel rdVM)
        {
            _rdVM = rdVM;
            _closeCommand = new RelayCommand(Close, ReturnTrue);
            _closeAllButThisCommand = new RelayCommand(CloseAllButThis, ReturnTrue);
            _startCommand = new RelayCommand((param) => Start(), ReturnTrue);
            _stopCommand = new RelayCommand((param) => Stop(), ReturnTrue);
            CanClose = false;
            CanFloat = false;
            ContentId = "HttpServer";
            Title = "HttpServer ";
            IconSource = PngIcons.GetIcon(IconName.Settings, IconSize.Size16);
            _eventList = new ObservableCollection<HttpListenerContext>();
        }

        #endregion

        #region Properties

        public int Port
        {
            get { return _port; }
            set
            {
                _port = value;
                OnPropertyChanged(nameof(Port));
            }
        }

        public ObservableCollection<HttpListenerContext> EventList
        {
            get { return _eventList; }
        }

        public HttpListenerContext SelectedEvent
        {
            get { return _selectedEvent; }
            set
            {
                _selectedEvent = value;
                OnPropertyChanged(nameof(SelectedEvent));

                PropertyViewModel propVM = (PropertyViewModel)_rdVM.Tools[0];
                if (value != null)
                    propVM.PropertiesObj = value.Request;
                else
                    propVM.PropertiesObj = null;
            }
        }

        #endregion

        #region Commands

        public override ICommand CloseCommand
        {
            get { return _closeCommand; }
        }

        public override ICommand CloseAllButThisCommand
        {
            get { return _closeAllButThisCommand; }
        }

        public ICommand StartCommand
        {
            get { return _startCommand; }
        }

        public ICommand StopCommand
        {
            get { return _stopCommand; }
        }

        #endregion

        #region Methods

        private bool ReturnTrue(object param)
        {
            return true;
        }

        private void Close(object param)
        {

        }

        private void CloseAllButThis(object param)
        {

        }

        private void Start()
        {
            try
            {
                _server = new HttpServer(new string[] { $"http://*:{Port}/" });
                _server.RequestHandler = HandleRequest;
                _server.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Stop()
        {
            try
            {
                _server.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HandleRequest(HttpListenerContext context)
        {
            WindowManager.Window.Dispatcher.Invoke(() =>
            {
                _eventList.Add(context);
            });
            byte[] data = Encoding.ASCII.GetBytes("Hello world !");
            context.Response.OutputStream.Write(data, 0, data.Length);
            context.Response.OutputStream.Close();

        }

        #endregion
    }
}