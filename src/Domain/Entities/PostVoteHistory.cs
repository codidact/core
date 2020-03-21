using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class PostVoteHistory : AuditEntity<PostVote>
    {
        public long Id { get; set; }
        public long PostId { get; set; }
        public long VoteTypeId { get; set; }
        public long MemberId { get; set; }

    }
}
