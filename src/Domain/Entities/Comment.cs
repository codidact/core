using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;
using Codidact.Core.Domain.Common.Interfaces;

namespace Codidact.Core.Domain.Entities
{
    public partial class Comment : AuditableEntity, ISoftDeletable
    {
        public Comment()
        {
            CommentVote = new HashSet<CommentVote>();
            InverseParentComment = new HashSet<Comment>();
        }

        public long Id { get; set; }
        public long MemberId { get; set; }
        public long PostId { get; set; }
        public long? ParentCommentId { get; set; }
        public string Body { get; set; }
        public long Upvotes { get; set; }
        public long Downvotes { get; set; }
        public long? NetVotes { get; set; }
        public decimal Score { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeletedByMemberId { get; set; }

        public virtual Member DeletedByMember { get; set; }
        public virtual Member Member { get; set; }
        public virtual Comment ParentComment { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<CommentVote> CommentVote { get; set; }
        public virtual ICollection<Comment> InverseParentComment { get; set; }
        public virtual Member CreatedByMember { get; set; }
        public virtual Member LastModifiedByMember { get; set; }

    }
}
