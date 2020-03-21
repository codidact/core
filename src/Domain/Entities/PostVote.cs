using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;
using Codidact.Core.Domain.Common.Interfaces;

namespace Codidact.Core.Domain.Entities
{
    public partial class PostVote : AuditableEntity, ISoftDeletable
    {
        public long Id { get; set; }
        public long PostId { get; set; }
        public long VoteTypeId { get; set; }
        public long MemberId { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeletedByMemberId { get; set; }

        public virtual Member CreatedByMember { get; set; }
        public virtual Member LastModifiedByMember { get; set; }
        public virtual Member Member { get; set; }
        public virtual Post Post { get; set; }
        public virtual VoteType VoteType { get; set; }
    }
}
