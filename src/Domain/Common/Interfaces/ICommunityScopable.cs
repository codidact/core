using Codidact.Domain.Entities;

namespace Codidact.Domain.Common.Interfaces
{
    /// <summary>
    /// Interface for entities that have a community related to them
    /// </summary>
    public interface ICommunityScopable
    {
        /// <summary>
        /// Community instance of related Community.
        /// </summary>
        public Community Community { get; set; }
    }
}
