using DotNetToolBox.MVVM;
using DotNetToolBox.RibbonDock;
using DotNetToolBox.RibbonDock.Dock;
using System;
using System.Windows.Input;

namespace DotNetToolBox.Tester.ViewModel
{
    public class TestDocument : DockingDocumentViewModelBase
    {
        private RibbonDockWindowViewModel _rdVM;
        private ICommand _closeCommand;
        private ICommand _closeAllButThisCommand;
        private string _testText;

        #region Constructor

        public TestDocument(RibbonDockWindowViewModel rdVM)
        {
            _rdVM = rdVM;
            _closeCommand = new RelayCommand(Close, ReturnTrue);
            _closeAllButThisCommand = new RelayCommand(CloseAllButThis, ReturnTrue);
            CanClose = true;
            CanFloat = true;
            ContentId = Guid.NewGuid().ToString();
            Title = "My document";
            IsActive = true;
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
            _rdVM.CloseDocument(this);
        }

        private void CloseAllButThis(object param)
        {

        }

        #endregion
    }
}
