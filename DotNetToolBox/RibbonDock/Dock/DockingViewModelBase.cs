#region license

//DotNetToolbox .NET helper library 
//Copyright (C) 2012-2020 Josué Clément
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
using System.Windows.Media;

namespace DotNetToolBox.RibbonDock.Dock
{
    public abstract class DockingViewModelBase : ObservableObject
    {
        private bool _canClose;
        private bool _canFloat;
        private string _contentId;
        private ImageSource _iconSource;
        private bool _isActive;
        private bool _isSelected;
        private string _title;

        #region Properties

        public bool CanClose
        {
            get { return _canClose; }
            set
            {
                _canClose = value;
                OnPropertyChanged(nameof(CanClose));
            }
        }

        public bool CanFloat
        {
            get { return _canFloat; }
            set
            {
                _canFloat = value;
                OnPropertyChanged(nameof(CanFloat));
            }
        }

        public string ContentId
        {
            get { return _contentId; }
            set
            {
                _contentId = value;
                OnPropertyChanged(nameof(ContentId));
            }
        }

        public ImageSource IconSource
        {
            get { return _iconSource; }
            set
            {
                _iconSource = value;
                OnPropertyChanged(nameof(IconSource));
            }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                OnPropertyChanged(nameof(IsActive));
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

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        #endregion
    }
}
