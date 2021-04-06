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
            if (string.IsNullOrWhiteSpace(tag))
                throw new ArgumentException("tag");
            if (value == null)
                throw new ArgumentException("value");

            Tag = tag;
            Value = value;
        }

        public string Tag { get; set; }
        public byte[] Value { get; set; }

        public List<TagValue> InnerTlv()
        {
            using (MemoryStream ms = new MemoryStream(Value))
            {
                BinaryTlvReader tlv = new BinaryTlvReader(ms);
                return tlv.ReadAll();
            }
        }

        public override string ToString()
        {
            return $"{Tag} ({Value.Length} bytes)";
        }
    }

    public class BinaryTlvWriter
    {
        private Stream _output;
        private byte _tagLength;

        public BinaryTlvWriter(Stream output, byte tagLength)
        {
            _output = output;
            _tagLength = tagLength;

            BinaryHelper.WriteByte(_output, _tagLength);
        }

        public void Write(string tag, byte[] value)
        {
            string padTag = tag.PadRight(_tagLength);

            if(padTag.Length != _tagLength)
                throw new TlvException("Invalid tag length");

            BinaryHelper.WriteString(_output, padTag, Encoding.ASCII);
            BinaryHelper.WriteInt32(_output, value.Length);
            BinaryHelper.WriteBytes(_output, value);
        }

        public static byte[] BuildTlv(List<TagValue> values, byte tagLength)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryTlvWriter tlv = new BinaryTlvWriter(ms, tagLength);
                foreach (TagValue tv in values)
                    tlv.Write(tv.Tag, tv.Value);

                return ms.ToArray();
            }
        }
    }

    public class BinaryTlvReader
    {
        private Stream _input;
        private byte _tagLength;

        public BinaryTlvReader(Stream input)
        {
            _input = input;
            _tagLength = BinaryHelper.ReadByte(_input);
        }

        public TagValue Read()
        {
            byte[] tagData = BinaryHelper.ReadBytes(_input, _tagLength);
            string tag = Encoding.ASCII.GetString(tagData).Trim();
            int valueLength = BinaryHelper.ReadInt32(_input);
            byte[] value = BinaryHelper.ReadBytes(_input, valueLength);
            return new TagValue(tag, value);
        }

        public List<TagValue> ReadAll()
        {
            List<TagValue> values = new List<TagValue>();
            TagValue tv;

            do
            {
                tv = Read();
                if (tv != null)
                    values.Add(tv);
            } while (tv != null);

            return values;
        }
    }
}
