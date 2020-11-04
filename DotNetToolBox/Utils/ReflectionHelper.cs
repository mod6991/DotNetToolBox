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

using FastMember;
using System;
using System.Collections.Generic;

namespace DotNetToolBox.Utils
{
    public static class ReflectionHelper
    {
        private static Dictionary<Type, TypeAccessor> _typeAccessors = new Dictionary<Type, TypeAccessor>();

        public static void RegisterType(Type t)
        {
            _typeAccessors.Add(t, TypeAccessor.Create(t));
        }

        /// <summary>
        /// Get the value of the given property on an object
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="propertyName">Property name</param>
        public static object GetValue(object obj, string propertyName)
        {
            Type t = obj.GetType();

            if (!_typeAccessors.ContainsKey(t))
                throw new InvalidOperationException($"Type '{t.FullName}' not registered! Use ReflectionHelper.RegisterType()");

            return _typeAccessors[t][obj, propertyName];
        }

        /// <summary>
        /// Set the value of the given property on a given object
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="propertyName">Property name</param>
        /// <param name="value">Value to set</param>
        public static void SetValue(object obj, string propertyName, object value)
        {
            Type t = obj.GetType();

            if (!_typeAccessors.ContainsKey(t))
                throw new InvalidOperationException($"Type '{t.FullName}' not registered! Use ReflectionHelper.RegisterType()");

            _typeAccessors[t][obj, propertyName] = value;
        }
    }
}
