using Codidact.Domain.Common;
using System;

namespace Codidact.Domain.Entities
{
    /// <summary>
    /// An Entity that represents a member of the community
    /// </summary>
    public class Member : AuditableEntity
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
        /// The main email of the member
        /// </summary>
        public string Email { get; set; }

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
        public DateTime? StackExchangeValidated { get; set; }

        /// <summary>
        /// When was the last time the data was imported from StackExchange
        /// </summary>
        public DateTime? StackExchangeLastImported { get; set; }

        /// <summary>
        /// Whether the email has been verified for the member
        /// </summary>
        public bool IsEmailVerified { get; set; }

        /// <summary>
        /// Whether the member is suspended
        /// </summary>
        public bool IsSuspended { get; set; }

        /// <summary>
        /// When does the suspension end
        /// </summary>
        public DateTime? SuspensionEndDate { get; set; }
    }
}
