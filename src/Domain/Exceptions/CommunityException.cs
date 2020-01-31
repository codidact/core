using System;

namespace Codidact.Domain.Exceptions
{
    /// <summary>
    /// Exception for invalid community, such as community does not exist
    /// or community data problem.
    /// </summary>
    public class CommunityInvalidException : Exception
    {
        public CommunityInvalidException(string message)
            : base(message)
        {
        }
    }
}
