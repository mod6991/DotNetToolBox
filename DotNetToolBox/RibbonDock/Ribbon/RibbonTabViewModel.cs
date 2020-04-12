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
using System.Collections.ObjectModel;

namespace DotNetToolBox.RibbonDock.Ribbon
{
    public class RibbonTabViewModel : ObservableObject
    {
        private string _header;
        private bool _isSelected;
        private string _id;
        private ObservableCollection<RibbonGroupViewModel> _groups;

        #region Constructors

        public RibbonTabViewModel()
        {
            _groups = new ObservableCollection<RibbonGroupViewModel>();
        }

        public RibbonTabViewModel(string header)
            : this()
        {
            _header = header;
        }

        public RibbonTabViewModel(string header, string id)
            : this()
        {
            _header = header;
            _id = id;
        }

        #endregion

        #region Properties

        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                OnPropertyChanged(nameof(Header));
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public ObservableCollection<RibbonGroupViewModel> Groups
        {
            get { return _groups; }
        }

        #endregion
    }
}
