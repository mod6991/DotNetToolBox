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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetToolBox.IO
{
    public class CsvColumn
    {
        public CsvColumn(int index, string name)
        {
            Index = index;
            Name = name;
        }

        public int Index { get; set; }
        public string Name { get; set; }
    }

    public class CsvCell
    {
        public CsvCell(int index, string value)
        {
            Index = index;
            Value = value;
        }

        public int Index { get; set; }
        public string Value { get; set; }
    }

    public class UnknownColumnException : Exception { public UnknownColumnException(string columnName) : base($"Unknown column '{columnName}'") { } }
    public class AlreadyAddedColumn : Exception { public AlreadyAddedColumn(string columnName) : base($"Already added column '{columnName}'") { } }
    public class CannotReadColumnsException : Exception { public CannotReadColumnsException() : base("The class has been initialized with no header") { } }
    public class EndingQuoteNotFoundException : Exception { public EndingQuoteNotFoundException() : base("Ending quote not found") { } }
    public class SeparatorNotFollowingQuoteException : Exception { public SeparatorNotFollowingQuoteException() : base("Separator not following quote") { } }

    public class CsvReader : IDisposable
    {
        private bool _disposed;
        private Stream _inputStream;
        private StreamReader _sr;

        private bool _hasHeader;
        private string _header;
        private char _cSeparator;
        private char _cQuote;

        private Dictionary<string, CsvColumn> _columns;
        private Dictionary<int, string> _columnNames;
        private Dictionary<string, string> _values;

        #region Constructor

        /// <summary>
        /// Create a new instance of CsvReader with input stream
        /// </summary>
        /// <param name="inputStream">Input stream</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="hasHeader">Has header</param>
        /// <param name="separator">Separator</param>
        /// <param name="quote">Quote</param>
        public CsvReader(Stream inputStream, Encoding encoding, bool hasHeader, char separator = ';', char quote = '"')
        {
            _inputStream = inputStream;
            _sr = new StreamReader(_inputStream, encoding);

            _hasHeader = hasHeader;
            _cSeparator = separator;
            _cQuote = quote;

            _columns = new Dictionary<string, CsvColumn>();
            _columnNames = new Dictionary<int, string>();
            _values = new Dictionary<string, string>();

            if (_hasHeader)
                _header = _sr.ReadLine();
        }

        /// <summary>
        /// Create a new instance of CsvReader with file
        /// </summary>
        /// <param name="file">Input file</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="hasHeader">Has header</param>
        /// <param name="separator">Separator</param>
        /// <param name="quote">Quote</param>
        public CsvReader(string file, Encoding encoding, bool hasHeader, char separator = ';', char quote = '"')
        {
            _inputStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
            _sr = new StreamReader(_inputStream, encoding);

            _hasHeader = hasHeader;
            _cSeparator = separator;
            _cQuote = quote;

            _columns = new Dictionary<string, CsvColumn>();
            _columnNames = new Dictionary<int, string>();
            _values = new Dictionary<string, string>();

            if (_hasHeader)
                _header = _sr.ReadLine();
        }

        ~CsvReader()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Indexer for the values by column name
        /// </summary>
        /// <param name="columnName">Column name</param>
        public string this[string columnName]
        {
            get
            {
                if (!_values.ContainsKey(columnName))
                    throw new UnknownColumnException(columnName);

                return _values[columnName];
            }
        }

        public Dictionary<string, CsvColumn> Columns
        {
            get { return _columns; }
        }

        public Dictionary<string, string> Values
        {
            get { return _values; }
        }

        public string Header
        {
            get { return _header; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a new column
        /// </summary>
        /// <param name="index">Column index (starting at 0)</param>
        /// <param name="columnName">Column name</param>
        public void AddColumn(int index, string columnName)
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(CsvReader).FullName);
            if (_columns.ContainsKey(columnName))
                throw new AlreadyAddedColumn(columnName);

            _columns.Add(columnName, new CsvColumn(index, columnName));
            _columnNames.Add(index, columnName);
            _values.Add(columnName, null);
        }

        /// <summary>
        /// Read columns from header
        /// </summary>
        public void ReadColumns()
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(CsvReader).FullName);
            if (!_hasHeader)
                throw new CannotReadColumnsException();

            _columns.Clear();
            _columnNames.Clear();
            _values.Clear();

            List<CsvCell> headerValues = ReadLineValues(_header);

            for (int i = 0; i < headerValues.Count; i++)
            {
                _columns.Add(headerValues[i].Value, new CsvColumn(i, headerValues[i].Value));
                _columnNames.Add(i, headerValues[i].Value);
                _values.Add(headerValues[i].Value, null);
            }
        }

        /// <summary>
        /// Read a line
        /// </summary>
        public bool ReadLine()
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(CsvReader).FullName);

            if (_sr.EndOfStream)
                return false;

            string line = _sr.ReadLine();

            if (string.IsNullOrEmpty(line))
                return false;
            
            List<CsvCell> values = ReadLineValues(line);

            foreach(KeyValuePair<string, CsvColumn> kvp in _columns)
                _values[kvp.Value.Name] = values[kvp.Value.Index].Value;

            return true;
        }

        /// <summary>
        /// Read line values
        /// </summary>
        /// <param name="line">CSV line</param>
        private List<CsvCell> ReadLineValues(string line)
        {
            List<CsvCell> values = new List<CsvCell>();
            int i = 0;
            bool done = false;


            while (!done)
            {
                char c = line[i];

                if (c == _cQuote) //if char is starting quote
                {
                    int ioq = line.IndexOf(_cQuote, i + 1); //search the ending quote
                    if (ioq == -1)
                        throw new EndingQuoteNotFoundException();

                    string value = line.Substring(i + 1, ioq - i - 1); //Get the value in the quotes
                    values.Add(new CsvCell(values.Count, value));

                    if (ioq == line.Length - 1) //If the ending quote is the last char on the line -> DONE
                        done = true;
                    else
                    {
                        if (line[ioq + 1] != _cSeparator) //If the ending quote is not followed by a separator
                            throw new SeparatorNotFollowingQuoteException();

                        if (ioq + 1 == line.Length - 1) //If the last char on the line is a separator -> add empty value -> DONE
                        {
                            values.Add(new CsvCell(values.Count, string.Empty));
                            done = true;
                        }

                        i = ioq + 2; //next pos goes after the separator that follows the ending quote
                    }
                }
                else if (c == _cSeparator) //if char is a separator
                {
                    //Add empty value
                    values.Add(new CsvCell(values.Count, string.Empty));

                    if (i == line.Length - 1) //If the separator is the last char of the line -> add empty value -> DONE
                    {
                        values.Add(new CsvCell(values.Count, string.Empty));
                        done = true;
                    }
                    else
                        i++; //next pos goes to the next char
                }
                else //if it's not a starting quote or a separator
                {
                    int ios = line.IndexOf(_cSeparator, i + 1); //look for the next separator
                    if (ios > i) //If separator found -> Get the value 
                    {
                        string value = line.Substring(i, ios - i);
                        values.Add(new CsvCell(values.Count, value));
                        i = ios + 1; //next pos goes after the separator
                    }
                    else //If separator not found -> the value is the last thing on the line -> read it -> DONE
                    {
                        string value = line.Substring(i, line.Length - i);
                        values.Add(new CsvCell(values.Count, value));
                        done = true;
                    }
                }
            }

            return values;
        }

        #endregion

        #region IDisposable implementation

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
                if (_sr != null)
                {
                    _sr.Dispose();
                    _sr = null;
                }

                if (_inputStream != null)
                {
                    _inputStream.Dispose();
                    _inputStream = null;
                }
            }

            _disposed = true;
        }

        #endregion
    }
}
