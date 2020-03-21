using System;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class CategoryHistory : AuditEntity<Category>
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string UrlPart { get; set; }
        public bool IsPrimary { get; set; }
        public string ShortExplanation { get; set; }
        public string LongExplanation { get; set; }
        public bool? ContributesToTrustLevel { get; set; }
        public long? Calculations { get; set; }
        public long HistoryId { get; set; }
        public DateTime HistoryActivityAt { get; set; }
        public long HistoryActivityMemberId { get; set; }
        public virtual Member HistoryActivityMember { get; set; }
    }
}
