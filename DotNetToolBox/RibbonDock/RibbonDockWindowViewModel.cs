#region license

//DotNetToolbox .NET helper library 
//Copyright (C) 2012  Josué Clément
//mod6991@gmail.com

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using DotNetToolBox.MVVM;
using DotNetToolBox.RibbonDock.Dock;
using DotNetToolBox.RibbonDock.Ribbon;
using System.Collections.ObjectModel;

namespace DotNetToolBox.RibbonDock
{
    public class RibbonDockWindowViewModel : ViewModelBase<RibbonDockWindow>
    {
        private ObservableCollection<RibbonTabViewModel> _tabs;
        private ObservableCollection<RibbonApplicationMenuItemViewModel> _applicationMenuItems;
        private ObservableCollection<DockingDocumentViewModelBase> _documents;
        private ObservableCollection<DockingToolViewModelBase> _tools;
        private DockingDocumentViewModelBase _activeDocument;
        private string _title;

        #region Constructor

        public RibbonDockWindowViewModel(RibbonDockWindow window)
            : base(window)
        {
            _tabs = new ObservableCollection<RibbonTabViewModel>();
            _applicationMenuItems = new ObservableCollection<RibbonApplicationMenuItemViewModel>();
            _documents = new ObservableCollection<DockingDocumentViewModelBase>();
            _tools = new ObservableCollection<DockingToolViewModelBase>();
        }

        #endregion

        #region Properties

        public ObservableCollection<RibbonTabViewModel> Tabs
        {
            get { return _tabs; }
        }

        public ObservableCollection<RibbonApplicationMenuItemViewModel> ApplicationMenuItems
        {
            get { return _applicationMenuItems; }
        }

        public ObservableCollection<DockingDocumentViewModelBase> Documents
        {
            get { return _documents; }
        }

        public ObservableCollection<DockingToolViewModelBase> Tools
        {
            get { return _tools; }
        }

        public DockingDocumentViewModelBase ActiveDocument
        {
            get { return _activeDocument; }
            set
            {
                _activeDocument = value;
                OnPropertyChanged("ActiveDocument");
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        #endregion

        #region Methods

        public void CloseDocument(DockingDocumentViewModelBase document)
        {
            _documents.Remove(document);
        }

        public void HideTool(DockingToolViewModelBase tool)
        {
            _tools.Remove(tool);
        }

        #endregion
    }
}
