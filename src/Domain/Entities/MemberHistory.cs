using System;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class MemberHistory : AuditEntity<Member>
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public string ProfilePictureLink { get; set; }
        public bool IsTemporarilySuspended { get; set; }
        public DateTime? TemporarySuspensionEndAt { get; set; }
        public string TemporarySuspensionReason { get; set; }
        public long TrustLevelId { get; set; }
        public long? NetworkAccountId { get; set; }
        public bool IsModerator { get; set; }
        public bool IsAdministrator { get; set; }
        public bool? IsSyncedWithNetworkAccount { get; set; }
    }
}
