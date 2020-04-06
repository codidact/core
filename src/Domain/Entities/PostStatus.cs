using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class PostStatus : AuditableEntity
    {
        public long Id { get; set; }
        public long PostId { get; set; }
        public long PostStatusTypeId { get; set; }

        public virtual Member CreatedByMember { get; set; }
        public virtual Member LastModifiedByMember { get; set; }
        public virtual Post Post { get; set; }
        public virtual PostStatusType PostStatusType { get; set; }
    }
}
