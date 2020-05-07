using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class VoteType : AuditableEntity
    {
        public VoteType()
        {
            CommentVote = new HashSet<CommentVote>();
            PostVote = new HashSet<PostVote>();
        }

        public long Id { get; set; }
        public string DisplayName { get; set; }
        public virtual Member CreatedByMember { get; set; }
        public virtual Member LastModifiedByMember { get; set; }

        public virtual ICollection<CommentVote> CommentVote { get; set; }
        public virtual ICollection<PostVote> PostVote { get; set; }
    }
}
