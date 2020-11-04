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

using System.Windows.Input;

namespace DotNetToolBox.RibbonDock.Dock
{
    public abstract class DockingDocumentViewModelBase : DockingViewModelBase
    {
        #region Commands

        public abstract ICommand CloseCommand
        {
            get;
        }

        public abstract ICommand CloseAllButThisCommand
        {
            get;
        }

        #endregion
    }
}
