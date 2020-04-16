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

using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DotNetToolBox.Database
{
    public class RequestFileManager
    {
        private DbManager _dbManager;
        private Dictionary<string, string> _requests;

        #region Constructor

        public RequestFileManager(DbManager dbManager, Dictionary<string, string> requests)
        {
            _dbManager = dbManager;
            _requests = requests;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Execute a SQL request by its name and stores the results in a DataTable
        /// </summary>
        /// <param name="requestName">Request name</param>
        /// <param name="parameters">Parameters</param>
        /// <param name="table">DataTable</param>
        public void FillDataTable(string requestName, List<DbParameter> parameters, DataTable table)
        {
            if (!_requests.ContainsKey(requestName))
                throw new Exception($"Request '{requestName}' not found");

            using (DbCommand command = _dbManager.Connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = _requests[requestName];

                if (_dbManager.Transaction != null)
                    command.Transaction = _dbManager.Transaction;

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (DbParameter param in parameters)
                        command.Parameters.Add(param);
                }

                using (DbDataAdapter adapter = _dbManager.Factory.CreateDataAdapter())
                {
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                }
            }
        }

        /// <summary>
        /// Execute a SQL request by its name and stores the results in a list of objects
        /// </summary>
        /// <typeparam name="T">Type of objects to return</typeparam>
        /// <param name="requestName">Request name</param>
        /// <param name="parameters">Parameters</param>
        public List<T> FillObjects<T>(string requestName, List<DbParameter> parameters)
        {
            if (!_requests.ContainsKey(requestName))
                throw new Exception($"Request '{requestName}' not found");
            if (!_dbManager.TypeMappings.ContainsKey(typeof(T)) || !_dbManager.TypeAccessors.ContainsKey(typeof(T)))
                throw new Exception($"Type '{typeof(T).FullName}' not registered! Use DbManager RegisterDbObject method");

            List<T> list = new List<T>();
            List<DbObjectMapping> mappingList = _dbManager.TypeMappings[typeof(T)];
            TypeAccessor ta = _dbManager.TypeAccessors[typeof(T)];

            using (DbCommand command = _dbManager.Connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = _requests[requestName];

                if (_dbManager.Transaction != null)
                    command.Transaction = _dbManager.Transaction;

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (DbParameter param in parameters)
                        command.Parameters.Add(param);
                }

                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T obj = (T)Activator.CreateInstance(typeof(T));

                        for (int i = 0, l = mappingList.Count; i < l; i++)
                        {
                            object readerValue = reader[mappingList[i].DbFieldName];
                            if (!(readerValue is DBNull))
                                ta[obj, mappingList[i].PropertyName] = readerValue;
                        }

                        list.Add(obj);
                    }

                    reader.Close();
                }
            }

            return list;
        }

        /// <summary>
        /// Execute a SQL request by its name
        /// </summary>
        /// <param name="requestName">Request name</param>
        /// <param name="parameters">Parameters</param>
        public int ExecuteNonQuery(string requestName, List<DbParameter> parameters)
        {
            if (!_requests.ContainsKey(requestName))
                throw new Exception($"Request '{requestName}' not found");

            using (DbCommand command = _dbManager.Connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = _requests[requestName];

                if (_dbManager.Transaction != null)
                    command.Transaction = _dbManager.Transaction;

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (DbParameter param in parameters)
                        command.Parameters.Add(param);
                }

                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Execute a SQL request by its name and returns the single result
        /// </summary>
        /// <param name="requestName">Request name</param>
        /// <param name="parameters">Parameters</param>
        public object ExecuteScalar(string requestName, List<DbParameter> parameters)
        {
            if (!_requests.ContainsKey(requestName))
                throw new Exception($"Request '{requestName}' not found");

            using (DbCommand command = _dbManager.Connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = _requests[requestName];

                if (_dbManager.Transaction != null)
                    command.Transaction = _dbManager.Transaction;

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (DbParameter param in parameters)
                        command.Parameters.Add(param);
                }

                return command.ExecuteScalar();
            }
        }

        #endregion
    }
}
