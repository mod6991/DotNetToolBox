using DotNetToolBox.Icons.OpenIconLibrary;
using DotNetToolBox.MVVM;
using DotNetToolBox.RibbonDock;
using DotNetToolBox.RibbonDock.Dock;
using DotNetToolBox.Tester.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DotNetToolBox.Tester.ViewModel
{
    public class ConfigurationViewModel : DockingDocumentViewModelBase
    {
        private RibbonDockWindowViewModel _rdVM;
        private ICommand _closeCommand;
        private ICommand _closeAllButThisCommand;
        private ObservableCollection<SettingModel> _settings;

        #region Constructor

        public ConfigurationViewModel(RibbonDockWindowViewModel rdVM)
        {
            _rdVM = rdVM;
            _closeCommand = new RelayCommand(Close, ReturnTrue);
            _closeAllButThisCommand = new RelayCommand(CloseAllButThis, ReturnTrue);
            CanClose = false;
            CanFloat = false;
            ContentId = "Configuration";
            Title = "Configuration ";
            IconSource = PngIcons.GetIcon(IconName.Settings, IconSize.Size16);
            _settings = new ObservableCollection<SettingModel>();
        }

        #endregion

        #region Properties

        public ObservableCollection<SettingModel> Settings
        {
            get { return _settings; }
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

        public void SaveConfig(object param)
        {

        }

        public void LoadConfig(object param)
        {

        }

        #endregion
    }
}
