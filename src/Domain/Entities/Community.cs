using Codidact.Core.Domain.Common;
using Codidact.Core.Domain.Common.Interfaces;
using Codidact.Core.Domain.Enums;
using System;

namespace Codidact.Core.Domain.Entities
{
    public class Community : AuditableEntity, ISoftDeletable
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

        /// <summary>
        /// The status of the community
        /// </summary>
        public CommunityStatus Status { get; set; }
        /// <summary>
        /// Date at which the entity has been deleted
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Whether the entity has been deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// By which member Id the entity has been deleted
        /// </summary>
        public long? DeletedByMemberId { get; set; }
    }
}
