using Codidact.Domain.Common;
using Codidact.Domain.Common.Interfaces;
using System;

namespace Codidact.Domain.Entities
{
    /// <summary>
    /// Member of the community
    /// </summary>
    public class MemberCommunity : AuditableEntity, ICommunityable
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
        public DateTime? StackExchangeValidatedAt { get; set; }

        /// <summary>
        /// When was the last time the data was imported from StackExchange
        /// </summary>
        public DateTime? StackExchangeLastImportedAt { get; set; }

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
        /// Amount of up votes cast by the community member
        /// </summary>
        public long UpvotesCast { get; set; }

        /// <summary>
        /// Amount of down votes cast by the community member
        /// </summary>
        public long DownvotesCast { get; set; }

        /// <summary>
        /// Amount of profile views on the member
        /// </summary>
        public long ProfileViews { get; set; }

        /// <summary>
        /// Reputation of the member in the community
        /// </summary>
        public long Reputation { get; set; }

        /// <summary>
        /// TODO: Explain what this is 
        /// </summary>
        public bool IsSameAsInstance { get; set; } = true;

        /// <summary>
        /// Trust level Id in related trust level community 
        /// </summary>
        public long TrustLevelCommunityId { get; set; }

        /// <summary>
        /// TrustLevelCommunity instance of related community
        /// </summary>
        public TrustLevelCommunity TrustLevelCommunity { get; set; }

        /// <summary>
        /// CommunityId of the member
        /// </summary>
        public long CommunityId { get; set; }

        /// <summary>
        /// Community instance related to the MemberCommunity
        /// </summary>
        public Community Community { get; set; }

        /// <summary>
        /// MemberId of the related member
        /// </summary>
        public long MemberId { get; set; }

        /// <summary>
        /// Member instance of related instance
        /// </summary>
        public Member Member { get; set; }
    }
}
