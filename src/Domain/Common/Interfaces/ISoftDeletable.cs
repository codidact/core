using System;

namespace Codidact.Domain.Common.Interfaces
{
    /// <summary>
    /// Signifies that the entity should be only soft deleted
    /// </summary>
    public interface ISoftDeletable
    {
        /// <summary>
        /// Date at which the entity has been deleted
        /// </summary>
        DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Whether the entity has been deleted
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// By which member Id the entity has been deleted
        /// </summary>
        long? DeletedByMemberId { get; set; }
    }
}
