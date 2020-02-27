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


    }
}
