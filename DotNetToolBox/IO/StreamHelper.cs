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

using System.IO;
using System;

namespace DotNetToolBox.IO
{
    public static class StreamHelper
    {
        /// <summary>
        /// Write the input stream into the output stream
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void WriteStream(Stream input, Stream output, int bufferSize = 4096)
        {
            byte[] buffer = new byte[bufferSize];
            int bytesRead;

            do
            {
                bytesRead = input.Read(buffer, 0, bufferSize);
                if (bytesRead > 0)
                    output.Write(buffer, 0, bytesRead);
            } while (bytesRead == bufferSize);
        }

        /// <summary>
        /// Write the input stream into the output stream and notifies the progression
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="notifyProgression">Notify progression method</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void WriteStream(Stream input, Stream output, Action<int> notifyProgression, int bufferSize = 4096)
        {
            byte[] buffer = new byte[bufferSize];
            int bytesRead;

            do
            {
                bytesRead = input.Read(buffer, 0, bufferSize);
                if (bytesRead > 0)
                {
                    output.Write(buffer, 0, bytesRead);
                    notifyProgression(bytesRead);
                }
            } while (bytesRead == bufferSize);
        }

        /// <summary>
        /// Get a new filestream in create mode
        /// </summary>
        /// <param name="file">File path</param>
        public static FileStream GetFileStreamCreate(string file)
        {
            return new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.Write);
        }

        /// <summary>
        /// Get a new filestream in open mode
        /// </summary>
        /// <param name="file">File path</param>
        public static FileStream GetFileStreamOpen(string file)
        {
            return new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
    }
}
