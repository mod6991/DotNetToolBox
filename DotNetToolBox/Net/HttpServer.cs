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
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetToolBox.Net
{
    public sealed class HttpServer
    {
        private HttpListener _listener;
        private Thread _listenerThread;
        private Action<HttpListenerContext> _requestHandler;

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="prefixes">HttpServer prefixes. sample: "http://*:10000/Test/"</param>
        public HttpServer(string[] prefixes)
        {
            if (!HttpListener.IsSupported)
                throw new Exception("HttpListener not supported !");

            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            _listener = new HttpListener();

            foreach (string prefix in prefixes)
                _listener.Prefixes.Add(prefix);
        }

        #endregion

        #region Properties

        public Action<HttpListenerContext> RequestHandler
        {
            get { return _requestHandler; }
            set { _requestHandler = value; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Start the HttpServer
        /// </summary>
        public void Start()
        {
            if (_requestHandler == null)
                throw new Exception("RequestHandler not defined (null) !");

            _listenerThread = new Thread(new ThreadStart(WaitForClients));
            _listenerThread.Start();
        }

        /// <summary>
        /// Close the HttpServer
        /// </summary>
        public void Close()
        {
            if (_listener != null)
            {
                _listener.Stop();
                _listener.Close();
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Wait for clients
        /// </summary>
        private void WaitForClients()
        {
            _listener.Start();

            while (true)
            {
                HttpListenerContext context = _listener.GetContext();

                Task task = new Task(() => HandleRequest(context));
                task.Start();
            }
        }

        /// <summary>
        /// Handle request
        /// </summary>
        /// <param name="param">HttpListenerContext given as parameter</param>
        private void HandleRequest(object param)
        {
            HttpListenerContext context = (HttpListenerContext)param;

            if (context == null)
                throw new Exception("HttpListenerContext is null !");

            RequestHandler(context);
        }

        #endregion
    }
}