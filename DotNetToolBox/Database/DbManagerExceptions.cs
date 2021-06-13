using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetToolBox.Database
{
    public class ObjectNotRegisteredException : Exception
    {
        public ObjectNotRegisteredException(string message) : base(message) { }
        public ObjectNotRegisteredException(string message, Exception ex) : base(message, ex) { }
    }

    public class RequestFileAlreadyAddedException : Exception
    {
        public RequestFileAlreadyAddedException(string message) : base(message) { }
        public RequestFileAlreadyAddedException(string message, Exception ex) : base(message, ex) { }
    }

    public class InvalidRequestFileException : Exception
    {
        public InvalidRequestFileException(string message) : base(message) { }
        public InvalidRequestFileException(string message, Exception ex) : base(message, ex) { }
    }

    public class RequestFileNotFoundException : Exception
    {
        public RequestFileNotFoundException(string message) : base(message) { }
        public RequestFileNotFoundException(string message, Exception ex) : base(message, ex) { }
    }

    public class RequestsNotFoundException : Exception
    {
        public RequestsNotFoundException(string message) : base(message) { }
        public RequestsNotFoundException(string message, Exception ex) : base(message, ex) { }
    }

    public class RequestNotFoundException : Exception
    {
        public RequestNotFoundException(string message) : base(message) { }
        public RequestNotFoundException(string message, Exception ex) : base(message, ex) { }
    }
}
