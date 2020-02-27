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
using System.Windows;

namespace DotNetToolBox.RibbonDock.Ribbon
{
    public class RibbonGroupViewModel : ObservableObject
    {
        private string _header;
        private string _label;
        private string _width;
        private Visibility _visibility;
        private ObservableCollection<RibbonItemViewModelBase> _buttons;

        #region Constructors

        public RibbonGroupViewModel()
        {
            _buttons = new ObservableCollection<RibbonItemViewModelBase>();
        }

        public RibbonGroupViewModel(string header)
            : this()
        {
            _header = header;
            _width = "Auto";
        }

        #endregion

        #region Properties

        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                OnPropertyChanged("Header");
            }
        }

        public string Label
        {
            get { return _label; }
            set
            {
                _label = value;
                OnPropertyChanged("Label");
            }
        }

        public string Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged("Width");
            }
        }

        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        public ObservableCollection<RibbonItemViewModelBase> Buttons
        {
            get { return _buttons; }
        }

        #endregion
    }
}
