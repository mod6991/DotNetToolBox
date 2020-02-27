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
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DotNetToolBox.RibbonDock
{
    public class DockDataTemplateSelector : DataTemplateSelector
    {
        private static Dictionary<Type, DataTemplate> _templates = new Dictionary<Type, DataTemplate>();

        /// <summary>
        /// Register a viewmodel with its view
        /// </summary>
        /// <param name="viewModelType">ViewModel type</param>
        /// <param name="viewType">View type</param>
        public static void RegisterDockViewModel(Type viewModelType, Type viewType)
        {
            _templates.Add(viewModelType, DataTemplateHelper.CreateTemplate(viewModelType, viewType));
        }

        /// <summary>
        /// Select a template for an docking object
        /// </summary>
        /// <param name="item"></param>
        /// <param name="container"></param>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            foreach(KeyValuePair<Type, DataTemplate> kvp in _templates)
            {
                if (item.GetType() == kvp.Key)
                    return kvp.Value;
            }

            //if (item is ProductListViewModel)
            //    return DataTemplateHelper.CreateTemplate(typeof(ViewModel.ProductListViewModel), typeof(View.ProductList));
            //else if (item is ProductViewModel)
            //    return DataTemplateHelper.CreateTemplate(typeof(ViewModel.ProductViewModel), typeof(View.ProductView));

            return base.SelectTemplate(item, container);
        }
    }
}
