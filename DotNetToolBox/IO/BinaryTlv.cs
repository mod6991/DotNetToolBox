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
    public class TlvException : Exception
    {
        public TlvException(string message) : base(message) { }
    }

    public class TlvHeaderException : Exception
    {
        public TlvHeaderException(string message) : base(message) { }
    }

    public class TagValue
    {
        public TagValue(string tag, byte[] value)
        {
            Tag = tag;
            Value = value;
        }

        public string Tag { get; set; }
        public byte[] Value { get; set; }
    }

    public class BinaryTlvWriter
    {
        private Stream _output;
        private byte _tagLength;

        public BinaryTlvWriter(Stream output, byte tagLength)
        {
            _output = output;
            _tagLength = tagLength;

            WriteHeader();
        }

        private void WriteHeader()
        {
            byte[] header= Encoding.ASCII.GetBytes("BinaryTLV!");
            int headerLen = header.Length;

            _output.Write(header, 0, headerLen);
            _output.Write(new byte[] { _tagLength }, 0, 1);
        }

        public void Write(string tag, byte[] value)
        {
            byte[] tagData = Encoding.ASCII.GetBytes(tag.PadRight(_tagLength));

            if (tagData.Length != _tagLength)
                throw new TlvException("Invalid tag length");

            _output.Write(tagData, 0, _tagLength);

            byte[] valueLengthData = BitConverter.GetBytes(value.Length);
            _output.Write(valueLengthData, 0, 4);

            _output.Write(value, 0, value.Length);
        }
    }

    public class BinaryTlvReader
    {
        private Stream _input;
        private byte _tagLength;

        public BinaryTlvReader(Stream input)
        {
            _input = input;

            ReadHeader();
        }

        private void ReadHeader()
        {
            byte[] buffer = new byte[10];
            int bytesRead;

            bytesRead = _input.Read(buffer, 0, 10);

            if (bytesRead != 10)
                throw new TlvHeaderException("Cannot read the header");

            if (Encoding.ASCII.GetString(buffer) != "BinaryTLV!")
                throw new TlvHeaderException("The stream doesn't contain a BinaryTlv");

            buffer = new byte[1];
            bytesRead = _input.Read(buffer, 0, 1);

            if (bytesRead != 1)
                throw new TlvHeaderException("Cannot read tag length");

            _tagLength = buffer[0];
        }

        public TagValue Read()
        {
            byte[] buffer = new byte[_tagLength];
            int bytesRead;

            bytesRead = _input.Read(buffer, 0, _tagLength);

            if (bytesRead == 0)
                return null;

            if (bytesRead != _tagLength)
                throw new TlvException("Cannot read tag");

            string tag = Encoding.ASCII.GetString(buffer).Trim();

            buffer = new byte[4];
            bytesRead = _input.Read(buffer, 0, 4);

            if (bytesRead != 4)
                throw new TlvException("Cannot read length");

            int valueLength = BitConverter.ToInt32(buffer, 0);

            byte[] value = new byte[valueLength];
            bytesRead = _input.Read(value, 0, valueLength);

            if (bytesRead != valueLength)
                throw new TlvException("Cannot read value");

            return new TagValue(tag, value);
        }

        public Dictionary<string, byte[]> ReadAll()
        {
            Dictionary<string, byte[]> values = new Dictionary<string, byte[]>();
            TagValue tv;

            do
            {
                tv = Read();
                if (tv != null)
                    values.Add(tv.Tag, tv.Value);
            } while (tv != null);

            return values;
        }
    }
}
