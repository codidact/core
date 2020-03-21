using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class PostStatusType : AuditableEntity
    {
        public PostStatusType()
        {
            PostStatus = new HashSet<PostStatus>();
        }

        public long Id { get; set; }
        public string DisplayName { get; set; }
        public short? Description { get; set; }

        public virtual Member CreatedByMember { get; set; }
        public virtual Member LastModifiedByMember { get; set; }
        public virtual ICollection<PostStatus> PostStatus { get; set; }

    }
}
