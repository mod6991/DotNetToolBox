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

using System.Windows;
using System.Windows.Input;

namespace DotNetToolBox.MVVM
{
    public class RoutedEventToCommandHandler
    {
        private RoutedEvent _routedEvent;
        private DependencyProperty _property;

        #region Constructor

        public RoutedEventToCommandHandler(RoutedEvent routedEvent)
        {
            _routedEvent = routedEvent;
        }

        #endregion

        #region Public Methods

        public void PropertyChangedCallback(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = o as UIElement;

            if (element != null)
            {
                _property = e.Property;

                if (e.OldValue != null)
                    element.RemoveHandler(_routedEvent, new RoutedEventHandler(ExecuteCommand));

                if (e.NewValue != null)
                    element.AddHandler(_routedEvent, new RoutedEventHandler(ExecuteCommand));
            }
        }

        public void ExecuteCommand(object sender, RoutedEventArgs e)
        {
            DependencyObject o = sender as DependencyObject;

            if (o != null)
            {
                ICommand command = o.GetValue(_property) as ICommand;

                if (command != null)
                {
                    if (command.CanExecute(e))
                        command.Execute(e);
                }
            }
        }

        #endregion
    }
}
