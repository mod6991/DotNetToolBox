using DotNetToolBox.Configuration;
using DotNetToolBox.Icons.OpenIconLibrary;
using DotNetToolBox.MVVM;
using DotNetToolBox.RibbonDock;
using DotNetToolBox.RibbonDock.Dock;
using DotNetToolBox.Tester.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
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

        public bool ReturnTrue(object param)
        {
            return true;
        }

        private void Close(object param)
        {

        }

        private void CloseAllButThis(object param)
        {

        }

        public void ClearConfig(object param)
        {
            try
            {
                _settings.Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void LoadXmlConfig(object param)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Load config file";
                ofd.Multiselect = false;
                ofd.Filter = "All files (*.*)|*.*";

                bool? sdr = ofd.ShowDialog();
                if (!sdr.Value)
                    return;

                _settings.Clear();

                XMLConfigFileManager xml = new XMLConfigFileManager(ofd.FileName);
                xml.LoadConfigurationFile();
                
                foreach(KeyValuePair<string, Dictionary<string, string>> kvp in xml.Settings)
                {
                    foreach(KeyValuePair<string, string> kvp2 in xml.Settings[kvp.Key])
                    {
                        _settings.Add(new SettingModel() { Section = kvp.Key, Setting = kvp2.Key, Value = kvp2.Value });
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SaveXmlConfig(object param)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Load config file";
                sfd.Filter = "All files (*.*)|*.*";

                bool? sdr = sfd.ShowDialog();
                if (!sdr.Value)
                    return;

                XMLConfigFileManager xml = new XMLConfigFileManager(sfd.FileName);
                
                foreach(SettingModel sm in _settings)
                {
                    xml.AddSetting(sm.Section, sm.Setting, sm.Value);
                }

                xml.SaveConfigurationFile();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void LoadIniConfig(object param)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Load config file";
                ofd.Multiselect = false;
                ofd.Filter = "All files (*.*)|*.*";

                bool? sdr = ofd.ShowDialog();
                if (!sdr.Value)
                    return;

                _settings.Clear();

                INIConfigFileManager xml = new INIConfigFileManager(ofd.FileName);
                xml.LoadConfigurationFile();

                foreach (KeyValuePair<string, Dictionary<string, string>> kvp in xml.Settings)
                {
                    foreach (KeyValuePair<string, string> kvp2 in xml.Settings[kvp.Key])
                    {
                        _settings.Add(new SettingModel() { Section = kvp.Key, Setting = kvp2.Key, Value = kvp2.Value });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SaveIniConfig(object param)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Load config file";
                sfd.Filter = "All files (*.*)|*.*";

                bool? sdr = sfd.ShowDialog();
                if (!sdr.Value)
                    return;

                INIConfigFileManager xml = new INIConfigFileManager(sfd.FileName);

                foreach (SettingModel sm in _settings)
                {
                    xml.AddSetting(sm.Section, sm.Setting, sm.Value);
                }

                xml.SaveConfigurationFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
    }
}
