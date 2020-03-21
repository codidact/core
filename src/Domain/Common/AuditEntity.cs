using System;
using Codidact.Core.Domain.Entities;

namespace Codidact.Core.Domain.Common
{
    public abstract class AuditEntity<TEntity> : AuditableEntity where TEntity : AuditableEntity
    {
        public long HistoryId { get; set; }
        public DateTime HistoryActivityAt { get; set; }
        public long HistoryActivityMemberId { get; set; }
        public virtual Member HistoryActivityMember { get; set; }

    }
}
