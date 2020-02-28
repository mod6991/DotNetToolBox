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


using DotNetToolBox.Utils;
using System;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace DotNetToolBox.Icons.OpenIconLibrary
{
    public static class PngIcons
    {
        private static Assembly _assembly = Assembly.GetAssembly(typeof(PngIcons));
        private static string _pathFormat = "DotNetToolBox.Icons.OpenIconLibrary.png.{0}.{1}.png";

        private static BitmapImage GetImage(string size, string iconName)
        {
            return AssemblyHelper.GetEmbeddedImage(_assembly, String.Format(_pathFormat, size, iconName));
        }
    }
}
