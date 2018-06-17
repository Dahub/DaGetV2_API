using System;

namespace DaGetCore.Service.Tools
{
    public class DaGetServiceException : Exception
    {
        public DaGetServiceException() : base() { }
        public DaGetServiceException(string msg) : base(msg) { }
        public DaGetServiceException(string msg, Exception innerException) : base(msg, innerException) { }
    }
}
