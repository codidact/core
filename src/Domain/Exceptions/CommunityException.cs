using System;

namespace Codidact.Domain.Exceptions
{
    public class CommunityInvalidException : Exception
    {
        public CommunityInvalidException(string message)
            : base(message)
        {
        }
    }
}
