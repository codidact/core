using Codidact.Domain.Common;
using Codidact.Domain.Enums;

namespace Codidact.Domain.Entities
{
    public class Community : AuditableEntity
    {
        /// <summary>
        /// Auto Incremented Identification number
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The name of the community
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The tagline that appears on this community
        /// </summary>
        public string Tagline { get; set; }

        /// <summary>
        /// The url to the community website
        /// </summary>
        public string Url { get; set; }

        public CommunityStatus Status { get; set; }
    }
}
