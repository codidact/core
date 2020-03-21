using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class CommentVoteHistory : AuditEntity<CommentVote>
    {
        public long Id { get; set; }
        public long CommentId { get; set; }
        public long MemberId { get; set; }
        public long VoteTypeId { get; set; }
    }
}
