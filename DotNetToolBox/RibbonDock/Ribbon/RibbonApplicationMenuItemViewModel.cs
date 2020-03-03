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
using System.Windows.Input;
using System.Windows.Media;

namespace DotNetToolBox.RibbonDock.Ribbon
{
    public class RibbonApplicationMenuItemViewModel : ObservableObject
    {
        private string _header;
        private ImageSource _imageSource;
        private string _toolTipTitle;
        private string _toolTipDescription;
        private ImageSource _toolTipImage;
        private ICommand _command;
        private bool _isEnabled;

        #region Constructors

        public RibbonApplicationMenuItemViewModel()
        {
            _isEnabled = true;
        }

        public RibbonApplicationMenuItemViewModel(string header)
            : this()
        {
            _header = header;
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

        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        public string ToolTipTitle
        {
            get { return _toolTipTitle; }
            set
            {
                _toolTipTitle = value;
                OnPropertyChanged("ToolTipTitle");
            }
        }

        public string ToolTipDescription
        {
            get { return _toolTipDescription; }
            set
            {
                _toolTipDescription = value;
                OnPropertyChanged("ToolTipDescription");
            }
        }

        public ImageSource ToolTipImage
        {
            get { return _toolTipImage; }
            set
            {
                _toolTipImage = value;
                OnPropertyChanged("ToolTipImage");
            }
        }

        public ICommand Command
        {
            get { return _command; }
            set
            {
                _command = value;
                OnPropertyChanged("Command");
            }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        #endregion
    }
}
