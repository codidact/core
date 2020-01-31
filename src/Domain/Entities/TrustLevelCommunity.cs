using Codidact.Domain.Common;
using Codidact.Domain.Common.Interfaces;

namespace Codidact.Domain.Entities
{
    /// <summary>
    /// Levels of trust for each member in a community
    /// </summary>
    public class TrustLevelCommunity : AuditableEntity, ICommunityScopable
    {
        /// <summary>
        /// Auto Incremented Identification number
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name of the trust level
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A short explaination of this trust level and its meaning
        /// </summary>
        public string Explanation { get; set; }

        /// <summary>
        /// Community id of related community
        /// </summary>
        public long CommunityId { get; set; }

        /// <summary>
        /// Community instance of related community
        /// </summary>
        public Community Community { get; set; }

        /// <summary>
        /// TODO: Explain what this is 
        /// </summary>
        public bool IsSameAsInstance { get; set; } = true;
    }
}
