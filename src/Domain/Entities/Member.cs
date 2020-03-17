using Codidact.Core.Domain.Common;
using Codidact.Core.Domain.Common.Interfaces;
using System;

namespace Codidact.Core.Domain.Entities
{
    /// <summary>
    /// An Entity that represents a member of the community
    /// </summary>
    public class Member : AuditableEntity, ISoftDeletable
    {
        /// <summary>
        /// Auto Incremented Identification number
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The display name that is to be displayed for the member
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The bio information of the member
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Geographical location of the member
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Whether the member is imported from StackExchange or not
        /// </summary>
        public bool IsFromStackExchange { get; set; }

        /// <summary>
        /// The StackExchange Id if it was imported
        /// </summary>
        public long? StackExchangeId { get; set; }

        /// <summary>
        /// When was the data validated against StackExchange
        /// </summary>
        public DateTime? StackExchangeValidatedAt { get; set; }

        /// <summary>
        /// When was the last time the data was imported from StackExchange
        /// </summary>
        public DateTime? StackExchangeLastImportedAt { get; set; }

        /// <summary>
        /// Whether the member is suspended
        /// </summary>
        public bool IsSuspended { get; set; }

        /// <summary>
        /// When does the suspension end
        /// </summary>
        public DateTime? SuspensionEndAt { get; set; }

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

        /// <summary>
        /// User Id in the authentication database
        /// </summary>
        public long UserId { get; set; }
    }
}
