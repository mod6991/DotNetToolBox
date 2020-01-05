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
using System.IO;
using System.Xml;

namespace DotNetToolBox.Database
{
    public class DbManager : IDisposable
    {
        private bool _disposed;
        private DbConnection _connection;
        private DbProviderFactory _factory;
        private DbTransaction _transaction;
        private string _connectionString;
        private string _provider;
        private Dictionary<Type, TypeAccessor> _typeAccessors;
        private Dictionary<Type, List<DbObjectMapping>> _typeMappings;
        private Dictionary<string, RequestFileManager> _requestFileManagers;

        #region Constructors

        /// <summary>
        /// Create a new instance of DbManager with ConnectionString and Provider
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="provider">Provider name</param>
        public DbManager(string connectionString, string provider)
        {
            _connectionString = connectionString;
            _provider = provider;
            _factory = DbProviderFactories.GetFactory(_provider);
            _connection = _factory.CreateConnection();
            _connection.ConnectionString = _connectionString;

            _typeAccessors = new Dictionary<Type, TypeAccessor>();
            _typeMappings = new Dictionary<Type, List<DbObjectMapping>>();
            _requestFileManagers = new Dictionary<string, RequestFileManager>();
        }

        ~DbManager()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        public string ConnectionString
        {
            get { return _connectionString; }
        }

        public string Provider
        {
            get { return _provider; }
        }

        public DbProviderFactory Factory
        {
            get { return _factory; }
        }

        public DbConnection Connection
        {
            get { return _connection; }
        }

        public DbTransaction Transaction
        {
            get { return _transaction; }
        }

        public Dictionary<Type, TypeAccessor> TypeAccessors
        {
            get { return _typeAccessors; }
        }

        public Dictionary<Type, List<DbObjectMapping>> TypeMappings
        {
            get { return _typeMappings; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Open the connection to the database
        /// </summary>
        public void Open()
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(DbManager).FullName);

            _connection.Open();
        }

        /// <summary>
        /// Close the connection to the database
        /// </summary>
        public void Close()
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(DbManager).FullName);

            _connection.Close();
        }

        /// <summary>
        /// Start a database transaction
        /// </summary>
        public void BeginTransaction()
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(DbManager).FullName);

            _transaction = _connection.BeginTransaction();
        }

        /// <summary>
        /// End a database transaction.
        /// </summary>
        /// <param name="commit">Commits the transaction</param>
        public void EndTransaction(bool commit)
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(DbManager).FullName);

            if (_transaction != null)
            {
                if (commit)
                    _transaction.Commit();
                else
                    _transaction.Rollback();

                _transaction = null;
            }
        }

        /// <summary>
        /// Return a new parameter
        /// </summary>
        public DbParameter CreateParameter()
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(DbManager).FullName);

            return _factory.CreateParameter();
        }

        /// <summary>
        /// Return a new parameter
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Parameter value</param>
        /// <param name="paramDirection">Parameter direction</param>
        public DbParameter CreateParameter(string name, object value, ParameterDirection paramDirection = ParameterDirection.Input)
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(DbManager).FullName);

            DbParameter param = _factory.CreateParameter();
            param.ParameterName = name;
            param.Value = value;
            param.Direction = paramDirection;
            return param;
        }

        /// <summary>
        /// Register a DbObject
        /// </summary>
        /// <param name="t">Object type</param>
        public void RegisterDbObject(Type t)
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(DbManager).FullName);

            _typeAccessors.Add(t, TypeAccessor.Create(t));
            IDbObject obj = (IDbObject)Activator.CreateInstance(t);
            _typeMappings.Add(t, obj.GetMapping());
        }

        /// <summary>
        /// Execute a SQL request and stores the results in a DataTable
        /// </summary>
        /// <param name="request">SQL request</param>
        /// <param name="parameters">Parameters</param>
        /// <param name="table">DataTable</param>
        public void FillDataTableWithRequest(string request, List<DbParameter> parameters, DataTable table)
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(DbManager).FullName);

            using (DbCommand command = _connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = request;

                if (_transaction != null)
                    command.Transaction = _transaction;

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (DbParameter param in parameters)
                        command.Parameters.Add(param);
                }

                using (DbDataAdapter adapter = _factory.CreateDataAdapter())
                {
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                }
            }
        }

        /// <summary>
        /// Execute a stored procedure and stores the results in a DataTable
        /// </summary>
        /// <param name="procedureName">Procedure name</param>
        /// <param name="parameters">Parameters</param>
        /// <param name="table">DataTable</param>
        public void FillDataTableWithProcedure(string procedureName, List<DbParameter> parameters, DataTable table)
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(DbManager).FullName);

            using (DbCommand command = _connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = procedureName;

                if (_transaction != null)
                    command.Transaction = _transaction;

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (DbParameter param in parameters)
                        command.Parameters.Add(param);
                }

                using (DbDataAdapter adapter = _factory.CreateDataAdapter())
                {
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                }
            }
        }

        /// <summary>
        /// Execute a SQL request and stores the results in a list of objects
        /// </summary>
        /// <typeparam name="T">Type of objects to return</typeparam>
        /// <param name="request">SQL request</param>
        /// <param name="parameters">Parameters</param>
        public List<T> FillObjectsWithRequest<T>(string request, List<DbParameter> parameters)
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(DbManager).FullName);

            if (!_typeMappings.ContainsKey(typeof(T)) || !_typeAccessors.ContainsKey(typeof(T)))
                throw new Exception(String.Format("The type '{0}' is not registered! Use DbManager RegisterDbObject method", typeof(T).FullName));

            List<T> list = new List<T>();
            List<DbObjectMapping> mappingList = _typeMappings[typeof(T)];
            TypeAccessor ta = _typeAccessors[typeof(T)];

            using (DbCommand command = _connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = request;

                if (_transaction != null)
                    command.Transaction = _transaction;

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
        /// Execute a stored procedure and stores the results in a list of objects
        /// </summary>
        /// <typeparam name="T">Type of objects to return</typeparam>
        /// <param name="procedureName">Procedure name</param>
        /// <param name="parameters">Parameters</param>
        public List<T> FillObjectsWithProcedure<T>(string procedureName, List<DbParameter> parameters)
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(DbManager).FullName);

            if (!_typeMappings.ContainsKey(typeof(T)) || !_typeAccessors.ContainsKey(typeof(T)))
                throw new Exception(String.Format("The type '{0}' is not registered! Use DbManager RegisterDbObject method", typeof(T).FullName));

            List<T> list = new List<T>();
            List<DbObjectMapping> mappingList = _typeMappings[typeof(T)];
            TypeAccessor ta = _typeAccessors[typeof(T)];

            using (DbCommand command = _connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = procedureName;

                if (_transaction != null)
                    command.Transaction = _transaction;

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
        /// Execute a SQL request
        /// </summary>
        /// <param name="request">SQL request</param>
        /// <param name="parameters">Parameters</param>
        public int ExecuteNonQueryWithRequest(string request, List<DbParameter> parameters)
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(DbManager).FullName);

            using (DbCommand command = _connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = request;

                if (_transaction != null)
                    command.Transaction = _transaction;

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (DbParameter param in parameters)
                        command.Parameters.Add(param);
                }

                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Execute a stored procedure
        /// </summary>
        /// <param name="procedureName">Procedure name</param>
        /// <param name="parameters">Parameters</param>
        public int ExecuteNonQueryWithProcedure(string procedureName, List<DbParameter> parameters)
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(DbManager).FullName);

            using (DbCommand command = _connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = procedureName;

                if (_transaction != null)
                    command.Transaction = _transaction;

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (DbParameter param in parameters)
                        command.Parameters.Add(param);
                }

                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Execute a SQL request and returns the single result
        /// </summary>
        /// <param name="request">SQL request</param>
        /// <param name="parameters">Parameters</param>
        public object ExecuteScalarWithRequest(string request, List<DbParameter> parameters)
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(DbManager).FullName);

            using (DbCommand command = _connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = request;

                if (_transaction != null)
                    command.Transaction = _transaction;

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (DbParameter param in parameters)
                        command.Parameters.Add(param);
                }

                return command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Execute a stored procedure and returns the single result
        /// </summary>
        /// <param name="procedureName">Procedure name</param>
        /// <param name="parameters">Parameters</param>
        public object ExecuteScalarWithProcedure(string procedureName, List<DbParameter> parameters)
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(DbManager).FullName);

            using (DbCommand command = _connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = procedureName;

                if (_transaction != null)
                    command.Transaction = _transaction;

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (DbParameter param in parameters)
                        command.Parameters.Add(param);
                }

                return command.ExecuteScalar();
            }
        }

        #endregion

        #region RequestFileManager

        /// <summary>
        /// Add a request file
        /// </summary>
        /// <param name="name">Name of the request file</param>
        /// <param name="requestFile">Path of the file</param>
        public void AddRequestFile(string name, string filePath)
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(DbManager).FullName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException(String.Format("Request file '{0}' not found !", filePath), filePath);

            Dictionary<string, string> requests = new Dictionary<string, string>();
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            foreach (XmlNode rootNode in doc.ChildNodes)
            {
                if (rootNode.Name == "Requests")
                {
                    foreach (XmlNode requestNode in rootNode.ChildNodes)
                    {
                        if (requestNode.Name == "Request")
                            requests.Add(requestNode.Attributes["Name"].Value, requestNode.InnerText.Trim());
                    }
                }
            }

            RequestFileManager manager = new RequestFileManager(this, requests);
            _requestFileManagers.Add(name, manager);
        }

        /// <summary>
        /// Indexer for the request files
        /// </summary>
        /// <param name="name">Request file name</param>
        public RequestFileManager this[string name]
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException(typeof(DbManager).FullName);

                if (!_requestFileManagers.ContainsKey(name))
                    throw new Exception(String.Format("The Request file '{0}' is not found", name));
                return _requestFileManagers[name];
            }
        }

        #endregion

        #region IDisposable members

        /// <summary>
        /// Releases all resources used
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources used
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }

                _transaction = null;
                _factory = null;
                _connectionString = null;
                _provider = null;
            }

            _disposed = true;
        }
        
        #endregion
    }
}
