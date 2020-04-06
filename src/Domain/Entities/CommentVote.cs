using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class CommentVote : AuditableEntity
    {
        public long Id { get; set; }
        public long CommentId { get; set; }
        public long MemberId { get; set; }
        public long VoteTypeId { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual Member Member { get; set; }
        public virtual VoteType VoteType { get; set; }
        public virtual Member CreatedByMember { get; set; }
        public virtual Member LastModifiedByMember { get; set; }

    }
}
