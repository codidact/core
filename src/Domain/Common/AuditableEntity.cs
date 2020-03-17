using System;

namespace Codidact.Core.Domain.Common
{
    /// <summary>
    /// Class that all of the entities inherit from
    /// </summary>
    public class AuditableEntity
    {
        /// <summary>
        /// The date at which the entity was created
        /// </summary>
        public DateTime CreateDateAt { get; set; }

        /// <summary>
        /// The date at which the entity was last modified
        /// </summary>
        public DateTime? LastModifiedAt { get; set; }

        /// <summary>
        /// The member id which created this entity
        /// </summary>
        public long CreatedByMemberId { get; set; }

        /// <summary>
        /// The member id which last modified this entity
        /// </summary>
        public long? LastModifiedByMemberId { get; set; }
    }
}
