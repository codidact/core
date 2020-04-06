using System;
using Codidact.Core.Domain.Common;
using Codidact.Core.Domain.Common.Interfaces;

namespace Codidact.Core.Domain.Entities
{
    public partial class CommentHistory : AuditEntity<CategoryPostTypeHistory>, ISoftDeletable
    {
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
    }
}
