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

namespace DotNetToolBox.Database
{
    public interface IDbObject
    {
        /// <summary>
        /// Returns the object mapping between its properties and the names returned by the request
        /// </summary>
        List<DbObjectMapping> GetMapping();
    }

    public class DbObjectMapping
    {
        private string _propertyName;
        private string _dbFieldName;

        public DbObjectMapping()
        {

        }

        public DbObjectMapping(string propertyName, string dbFieldName)
        {
            _propertyName = propertyName;
            _dbFieldName = dbFieldName;
        }

        public string PropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; }
        }

        public string DbFieldName
        {
            get { return _dbFieldName; }
            set { _dbFieldName = value; }
        }
    }
}
