using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;
using Codidact.Core.Domain.Common.Interfaces;

namespace Codidact.Core.Domain.Entities
{
    public partial class Tag : AuditableEntity, ISoftDeletable
    {
        public Tag()
        {
            InverseParentTag = new HashSet<Tag>();
            PostTag = new HashSet<PostTag>();
        }

        public long Id { get; set; }
        public string Body { get; set; }
        public string Description { get; set; }
        public string TagWiki { get; set; }
        public bool? IsActive { get; set; }
        public long? SynonymTagId { get; set; }
        public long? ParentTagId { get; set; }
        public long Usages { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeletedByMemberId { get; set; }
        public virtual Member CreatedByMember { get; set; }
        public virtual Member LastModifiedByMember { get; set; }
        public virtual Tag ParentTag { get; set; }
        public virtual ICollection<Tag> InverseParentTag { get; set; }
        public virtual ICollection<PostTag> PostTag { get; set; }
    }
}
