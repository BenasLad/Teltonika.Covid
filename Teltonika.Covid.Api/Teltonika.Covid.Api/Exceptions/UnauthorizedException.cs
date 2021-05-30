using System;

namespace Teltonika.Covid.Api.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base($"Unauthorized: {message}")
        {
        }
    }
}
