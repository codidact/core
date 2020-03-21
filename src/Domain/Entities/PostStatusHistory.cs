using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class PostStatusHistory : AuditEntity<PostStatus>
    {
        public long Id { get; set; }
        public long PostId { get; set; }
        public long PostStatusTypeId { get; set; }
    }
}
