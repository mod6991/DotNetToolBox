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

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetToolBox.Net
{
    public sealed class TcpServer
    {
        private TcpListener _tcpListener;
        private IPAddress _ipAddress;
        private int _port;
        private Thread _listenerThread;
        private Action<TcpServer, TcpClient> _requestHandler;

        #region Constructors

        /// <summary>
        /// Create a new instance of TcpServer
        /// </summary>
        /// <param name="port">Listen port</param>
        /// <param name="ipAddress">IP address</param>
        public TcpServer(int port, IPAddress ipAddress = null)
        {
            _ipAddress = ipAddress ?? IPAddress.Any;
            _port = port;
        }

        #endregion

        #region Properties

        public Action<TcpServer, TcpClient> RequestHandler
        {
            get { return _requestHandler; }
            set { _requestHandler = value; }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Wait for clients
        /// </summary>
        private void WaitForClients()
        {
            _tcpListener.Start();

            while (true)
            {
                TcpClient client = _tcpListener.AcceptTcpClient();

                Task task = new Task(() => HandleRequest(client));
                task.Start();
            }
        }

        /// <summary>
        /// Handle request
        /// </summary>
        /// <param name="parameter"></param>
        private void HandleRequest(object parameter)
        {
            TcpClient client = parameter as TcpClient;
            RequestHandler(this, client);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Start the TCP server
        /// </summary>
        public void Start()
        {
            if (_requestHandler == null)
                throw new Exception("RequestHandler not defined (null) !");

            _tcpListener = new TcpListener(_ipAddress, _port);
            _listenerThread = new Thread(new ThreadStart(WaitForClients));
            _listenerThread.Start();
        }

        /// <summary>
        /// Close the TCP server
        /// </summary>
        public void Close()
        {
            if (_tcpListener != null)
                _tcpListener.Stop();
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Send TCP request to server
        /// </summary>
        /// <param name="ip">IP address</param>
        /// <param name="port">Listen port</param>
        /// <param name="data">Data to send</param>
        public static byte[] SendToServer(IPAddress ip, int port, byte[] data, int bufferSize = 4096)
        {
            if (data == null)
                throw new ArgumentException("data");

            TcpClient client = new TcpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(ip, port);
            client.Connect(serverEndPoint);

            NetworkStream stream = client.GetStream();
            using (MemoryStream ms = new MemoryStream(data))
            {
                //Send data to server
                IO.StreamHelper.WriteStream(ms, stream, bufferSize);
            }

            using (MemoryStream ms = new MemoryStream())
            {
                //Reads response from server
                IO.StreamHelper.WriteStream(stream, ms, bufferSize);

                if (stream != null)
                    stream.Close();

                if (client != null)
                    client.Close();

                return ms.ToArray();
            }
        }

        #endregion
    }
}
