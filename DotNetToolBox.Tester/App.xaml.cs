using DotNetToolBox.MVVM;
using DotNetToolBox.RibbonDock;
using DotNetToolBox.RibbonDock.Dock;
using DotNetToolBox.RibbonDock.Ribbon;
using DotNetToolBox.Tester.View;
using System;
using System.Windows;
using System.Windows.Input;

namespace DotNetToolBox.Tester
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected RibbonDockWindowViewModel vm2;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow window = new MainWindow();
            ViewModel.MainWindowViewModel vm = new ViewModel.MainWindowViewModel(window);
            window.DataContext = vm;
            window.Show();

            RibbonDock.RibbonDockWindow win2 = RibbonDock.RibbonDock.RibbonDockWindow;
            vm2 = (RibbonDockWindowViewModel)win2.DataContext;

            RibbonTabViewModel tab = new RibbonTabViewModel("HomeTab");
            RibbonGroupViewModel group = new RibbonGroupViewModel("1st group");
            RibbonButtonViewModel button = new RibbonButtonViewModel("test button");
            button.Command = new RelayCommand(ButtonClick, ReturnTrue);
            group.Buttons.Add(button);
            tab.Groups.Add(group);
            vm2.Tabs.Add(tab);

            DockDataTemplateSelector.RegisterDockViewModel(typeof(TestDocument), typeof(TestDocumentView));
            vm2.Documents.Add(new TestDocument());

            win2.Show();

        }

        private bool ReturnTrue(object param)
        {
            return true;
        }

        private void ButtonClick(object param)
        {
            vm2.Documents.Add(new TestDocument());
        }
    }

    public class TestDocument : DockingDocumentViewModelBase
    {
        private ICommand _closeCommand;
        private ICommand _closeAllButThisCommand;
        private string _testText;

        #region Constructor

        public TestDocument()
        {
            _closeCommand = new RelayCommand(Close, ReturnTrue);
            _closeAllButThisCommand = new RelayCommand(CloseAllButThis, ReturnTrue);
            CanClose = true;
            CanFloat = true;
            ContentId = Guid.NewGuid().ToString();
            Title = "My document";
            TestText = "no inspiration right now...";
        }

        #endregion

        #region Properties

        public string TestText
        {
            get { return _testText; }
            set
            {
                _testText = value;
                OnPropertyChanged("TestText");
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

        #endregion
    }
}
