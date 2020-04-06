using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;
using Codidact.Core.Domain.Common.Interfaces;

namespace Codidact.Core.Domain.Entities
{
    public partial class Category : AuditableEntity, ISoftDeletable
    {
        public Category()
        {
            Post = new HashSet<Post>();
        }

        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string UrlPart { get; set; }
        public bool IsPrimary { get; set; }
        public string ShortExplanation { get; set; }
        public string LongExplanation { get; set; }
        public bool? ContributesToTrustLevel { get; set; }
        public long? Calculations { get; set; }
        public long ParticipationMinimumTrustLevelId { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeletedByMemberId { get; set; }

        public virtual TrustLevel ParticipationMinimumTrustLevel { get; set; }
        public virtual ICollection<Post> Post { get; set; }
        public virtual Member CreatedByMember { get; set; }
        public virtual Member LastModifiedByMember { get; set; }

    }
}
