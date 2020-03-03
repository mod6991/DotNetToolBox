using DotNetToolBox.Icons.OpenIconLibrary;
using DotNetToolBox.MVVM;
using DotNetToolBox.RibbonDock;
using DotNetToolBox.RibbonDock.Dock;
using System;
using System.Windows.Input;

namespace DotNetToolBox.Tester.ViewModel
{
    public class TcpServerViewModel : DockingDocumentViewModelBase
    {
        private RibbonDockWindowViewModel _rdVM;
        private ICommand _closeCommand;
        private ICommand _closeAllButThisCommand;

        #region Constructor

        public TcpServerViewModel(RibbonDockWindowViewModel rdVM)
        {
            _rdVM = rdVM;
            _closeCommand = new RelayCommand(Close, ReturnTrue);
            _closeAllButThisCommand = new RelayCommand(CloseAllButThis, ReturnTrue);
            CanClose = false;
            CanFloat = false;
            ContentId = "TcpServer";
            Title = "TcpServer ";
            IconSource = PngIcons.GetIcon(IconName.Settings, IconSize.Size16);
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