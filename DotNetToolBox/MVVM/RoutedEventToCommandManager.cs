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

using System;
using System.Windows;
using System.Windows.Input;

namespace DotNetToolBox.MVVM
{
    public static class RoutedEventToCommandManager
    {
        public static DependencyProperty CreateCommandForRoutedEvent(RoutedEvent routedEvent, string propertyName, Type ownerType)
        {
            DependencyProperty property = DependencyProperty.RegisterAttached(propertyName, typeof(ICommand), ownerType,
                                          new PropertyMetadata(null, new RoutedEventToCommandHandler(routedEvent).PropertyChangedCallback));

            return property;
        }
    }
}
