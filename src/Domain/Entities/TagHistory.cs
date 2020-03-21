using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class TagHistory : AuditEntity<Tag>
    {
        public long HistoryId { get; set; }
        public DateTime HistoryActivityAt { get; set; }
        public long HistoryMemberId { get; set; }
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public long CreatedByMemberId { get; set; }
        public long LastModifiedByMemberId { get; set; }
        public string Body { get; set; }
        public string Description { get; set; }
        public string TagWiki { get; set; }
        public bool? IsActive { get; set; }
        public long? SynonymTagId { get; set; }
        public long? ParentTagId { get; set; }
        public long Usages { get; set; }

        public virtual Member HistoryMember { get; set; }
    }
}
