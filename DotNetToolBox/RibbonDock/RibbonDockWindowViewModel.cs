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
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xceed.Wpf.AvalonDock.Themes;

namespace DotNetToolBox.RibbonDock
{
    public class RibbonDockWindowViewModel : ViewModelBase<RibbonDockWindow>
    {
        private Theme _theme;
        private ObservableCollection<RibbonTabViewModel> _tabs;
        private ObservableCollection<RibbonApplicationMenuItemViewModel> _applicationMenuItems;
        private ObservableCollection<DockingDocumentViewModelBase> _documents;
        private ObservableCollection<DockingToolViewModelBase> _tools;
        private DockingDocumentViewModelBase _activeDocument;
        private string _title;
        private ICommand _loadedCommand;
        private ICommand _unloadedCommand;

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
                OnPropertyChanged(nameof(ActiveDocument));
                OnActiveDocumentChanged(new ActiveDocumentChangedEventArgs() { Document = value });
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public Theme Theme
        {
            get { return _theme; }
            set
            {
                _theme = value;
                OnPropertyChanged(nameof(Theme));
            }
        }

        #endregion

        #region Commands

        public ICommand LoadedCommand
        {
            get { return _loadedCommand; }
            set { _loadedCommand = value; }
        }

        public ICommand UnloadedCommand
        {
            get { return _unloadedCommand; }
            set { _unloadedCommand = value; }
        }

        #endregion

        #region Event

        public event ActiveDocumentChangedEventHandler ActiveDocumentChanged;

        protected virtual void OnActiveDocumentChanged(ActiveDocumentChangedEventArgs e)
        {
            ActiveDocumentChanged?.Invoke(this, e);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get a new RibbonDock window with a VM attached
        /// </summary>
        public static RibbonDockWindow GetNewWindow()
        {
            RibbonDockWindow window = new RibbonDockWindow();
            RibbonDockWindowViewModel vm = new RibbonDockWindowViewModel(window);
            window.DataContext = vm;
            return window;
        }

        /// <summary>
        /// Add a button to the ribbon
        /// </summary>
        /// <param name="tab">Tab header</param>
        /// <param name="group">Group header</param>
        /// <param name="button">Button</param>
        public void AddRibbonButton(string tab, string group, RibbonButtonViewModel button)
        {
            if (!_tabs.Any(x => x.Header == tab))
                _tabs.Add(new RibbonTabViewModel(tab));

            RibbonTabViewModel tabvm = _tabs.First(x => x.Header == tab);

            if (!tabvm.Groups.Any(x => x.Header == group))
                tabvm.Groups.Add(new RibbonGroupViewModel(group));

            RibbonGroupViewModel groupVM = tabvm.Groups.First(x => x.Header == group);

            groupVM.Buttons.Add(button);
        }

        /// <summary>
        /// Add a button to the ribbon
        /// </summary>
        /// <param name="tab">Tab header</param>
        /// <param name="tabId">Tab id</param>
        /// <param name="group">Group header</param>
        /// <param name="button">Button</param>
        public void AddRibbonButton(string tab, string tabId, string group, RibbonButtonViewModel button)
        {
            if (!_tabs.Any(x => x.Header == tab))
                _tabs.Add(new RibbonTabViewModel(tab, tabId));

            RibbonTabViewModel tabvm = _tabs.First(x => x.Header == tab);

            if (!tabvm.Groups.Any(x => x.Header == group))
                tabvm.Groups.Add(new RibbonGroupViewModel(group));

            RibbonGroupViewModel groupVM = tabvm.Groups.First(x => x.Header == group);

            groupVM.Buttons.Add(button);
        }

        /// <summary>
        /// Select a ribbon tab by its id
        /// </summary>
        /// <param name="tabId"></param>
        public void SelectRibbonTabById(string tabId)
        {
            _tabs.First(x => x.Id == tabId).IsSelected = true;
        }

        /// <summary>
        /// Select a ribbon tab by header name
        /// </summary>
        /// <param name="header"></param>
        public void SelectRibbonTabByHeader(string header)
        {
            _tabs.First(x => x.Header == header).IsSelected = true;
        }

        /// <summary>
        /// Get a document by its id
        /// </summary>
        /// <param name="contentId">Document id</param>
        public DockingDocumentViewModelBase GetDocument(string contentId)
        {
            return _documents.First(x => x.ContentId == contentId);
        }

        /// <summary>
        /// Set a document active by its id
        /// </summary>
        /// <param name="contentId">Document id</param>
        public void SetActiveDocument(string contentId)
        {
            ActiveDocument = _documents.First(x => x.ContentId == contentId);
        }

        /// <summary>
        /// Get a tool by its id
        /// </summary>
        /// <param name="contentId">Tool id</param>
        public DockingToolViewModelBase GetTool(string contentId)
        {
            return _tools.First(x => x.ContentId == contentId);
        }

        /// <summary>
        /// Remove a document
        /// </summary>
        /// <param name="document">Document to remove</param>
        public void CloseDocument(DockingDocumentViewModelBase document)
        {
            _documents.Remove(document);
        }

        /// <summary>
        /// Remove a tool
        /// </summary>
        /// <param name="tool">Tool to remove</param>
        public void HideTool(DockingToolViewModelBase tool)
        {
            _tools.Remove(tool);
        }

        #endregion
    }

    public class ActiveDocumentChangedEventArgs : EventArgs
    {
        public DockingDocumentViewModelBase Document { get; set; }
    }

    public delegate void ActiveDocumentChangedEventHandler(Object sender, ActiveDocumentChangedEventArgs e);
}
