using System;

namespace Gwp.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message) : base(message)
        {
            ExceptionCode = 100;
            HttpStatusCode = 500;
        }

        public int ExceptionCode { get; protected set; }
        public int HttpStatusCode { get; protected set; }
    }
}