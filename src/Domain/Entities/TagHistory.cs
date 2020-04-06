using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class TagHistory : AuditEntity<Tag>
    {
        public long Id { get; set; }
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
