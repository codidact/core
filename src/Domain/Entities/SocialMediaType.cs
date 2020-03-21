using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class SocialMediaType : AuditableEntity
    {
        public SocialMediaType()
        {
            MemberSocialMediaType = new HashSet<MemberSocialMediaType>();
        }

        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string AccountUrl { get; set; }
        public virtual Member CreatedByMember { get; set; }
        public virtual Member LastModifiedByMember { get; set; }

        public virtual ICollection<MemberSocialMediaType> MemberSocialMediaType { get; set; }
    }
}
