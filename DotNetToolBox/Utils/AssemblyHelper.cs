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

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace DotNetToolBox.Utils
{
    public static class AssemblyHelper
    {
        #region Loaded Assemblies

        /// <summary>
        /// Get the referenced assemblies of an assembly
        /// </summary>
        /// <param name="assembly">Assembly</param>
        public static Assembly[] GetLoadedAssemblies(Assembly assembly)
        {
            List<Assembly> list = new List<Assembly>();
            list = InternalGetAssemblies(assembly, ref list);
            return list.OrderBy(x => x.FullName).ToArray();
        }

        /// <summary>
        /// Get the referenced assemblies of an assembly
        /// </summary>
        /// <param name="assembly">Assembly</param>
        /// <param name="list">Reference of an empty list of assemblies</param>
        private static List<Assembly> InternalGetAssemblies(Assembly assembly, ref List<Assembly> list)
        {
            if (list.Contains(assembly))
                return new List<Assembly>();

            list.Add(assembly);
            AssemblyName[] names = assembly.GetReferencedAssemblies();

            foreach (AssemblyName name in names)
            {
                Assembly referencedAssembly = Assembly.Load(name);
                List<Assembly> tmpList = InternalGetAssemblies(referencedAssembly, ref list);
                foreach (Assembly tmpAssembly in tmpList)
                    if (!list.Contains(tmpAssembly))
                        list.Add(tmpAssembly);
            }

            return list;
        }

        #endregion

        #region Assembly details

        /// <summary>
        /// Get the assembly name
        /// </summary>
        /// <param name="assembly">Assembly</param>
        public static string GetName(Assembly assembly)
        {
            return assembly.GetName().Name;
        }

        /// <summary>
        /// Get the assembly version
        /// </summary>
        /// <param name="assembly">Assembly</param>
        public static string GetVersion(Assembly assembly)
        {
            return assembly.GetName().Version.ToString();
        }

        /// <summary>
        /// Get the assembly title
        /// </summary>
        /// <param name="assembly">Assembly</param>
        public static string GetTitle(Assembly assembly)
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
                return ((AssemblyTitleAttribute)attributes[0]).Title;
            else
                return null;
        }

        /// <summary>
        /// Get the assembly description
        /// </summary>
        /// <param name="assembly">Assembly</param>
        public static string GetDescription(Assembly assembly)
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length > 0)
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            else
                return null;
        }

        /// <summary>
        /// Get the assembly configuration
        /// </summary>
        /// <param name="assembly">Assembly</param>
        public static string GetConfiguration(Assembly assembly)
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyConfigurationAttribute), false);
            if (attributes.Length > 0)
                return ((AssemblyConfigurationAttribute)attributes[0]).Configuration;
            else
                return null;
        }

        /// <summary>
        /// Get the assembly company
        /// </summary>
        /// <param name="assembly">Assembly</param>
        public static string GetCompany(Assembly assembly)
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            if (attributes.Length > 0)
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            else
                return null;
        }

        /// <summary>
        /// Get the assembly product
        /// </summary>
        /// <param name="assembly">Assembly</param>
        public static string GetProduct(Assembly assembly)
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            if (attributes.Length > 0)
                return ((AssemblyProductAttribute)attributes[0]).Product;
            else
                return null;
        }

        /// <summary>
        /// Get the assembly copyright
        /// </summary>
        /// <param name="assembly">Assembly</param>
        public static string GetCopyright(Assembly assembly)
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (attributes.Length > 0)
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            else
                return null;
        }

        /// <summary>
        /// Get the assembly trademark
        /// </summary>
        /// <param name="assembly">Assembly</param>
        public static string GetTrademark(Assembly assembly)
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyTrademarkAttribute), false);
            if (attributes.Length > 0)
                return ((AssemblyTrademarkAttribute)attributes[0]).Trademark;
            else
                return null;
        }

        /// <summary>
        /// Get the assembly culture
        /// </summary>
        /// <param name="assembly">Assembly</param>
        public static string GetCulture(Assembly assembly)
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyCultureAttribute), false);
            if (attributes.Length > 0)
                return ((AssemblyCultureAttribute)attributes[0]).Culture;
            else
                return null;
        }

        /// <summary>
        /// Get the assembly guid
        /// </summary>
        /// <param name="assembly">Assembly</param>
        public static string GetGuid(Assembly assembly)
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(GuidAttribute), false);
            if (attributes.Length > 0)
                return ((GuidAttribute)attributes[0]).Value;
            else
                return null;
        }

        #endregion
    }
}