using DotNetToolBox.Icons.OpenIconLibrary;
using DotNetToolBox.MVVM;
using DotNetToolBox.RibbonDock;
using DotNetToolBox.RibbonDock.Dock;
using System;
using System.Windows.Input;

namespace DotNetToolBox.Tester.ViewModel
{
    public class PropertyViewModel : DockingToolViewModelBase
    {
        private RibbonDockWindowViewModel _rdVM;
        private ICommand _hideCommand;
        private object _propertiesObj;

        #region Constructor

        public PropertyViewModel(RibbonDockWindowViewModel rdVM)
        {
            _rdVM = rdVM;
            _hideCommand = new RelayCommand(Hide, ReturnTrue);
            CanClose = false;
            CanFloat = false;
            ContentId = "Properties";
            Title = "Properties ";
            IconSource = PngIcons.GetIcon(IconName.Settings, IconSize.Size16);
        }

        #endregion

        #region Properties

        public object PropertiesObj
        {
            get { return _propertiesObj; }
            set
            {
                _propertiesObj = value;
                OnPropertyChanged(nameof(PropertiesObj));
            }
        }

        #endregion

        #region Commands

        public override ICommand HideCommand
        {
            get { return _hideCommand; }
        }

        #endregion

        #region Methods

        private bool ReturnTrue(object param)
        {
            return true;
        }

        private void Hide(object param)
        {

        }

        #endregion
    }
}
