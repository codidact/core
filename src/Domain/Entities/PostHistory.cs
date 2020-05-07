using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class PostHistory : AuditEntity<Post>
    {
        public long Id { get; set; }
        public long MemberId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public short Upvotes { get; set; }
        public short Downvotes { get; set; }
        public long? NetVotes { get; set; }
        public decimal Score { get; set; }
        public long Views { get; set; }
        public long PostTypeId { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsClosed { get; set; }
        public bool IsProtected { get; set; }
        public long? ParentPostId { get; set; }
        public long CategoryId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
