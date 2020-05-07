using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class MemberSocialMediaType : AuditableEntity
    {
        public long Id { get; set; }
        public long SocialMediaId { get; set; }
        public long MemberId { get; set; }
        public string Content { get; set; }

        public virtual Member Member { get; set; }
        public virtual SocialMediaType SocialMedia { get; set; }
        public virtual Member CreatedByMember { get; set; }
        public virtual Member LastModifiedByMember { get; set; }

    }
}
