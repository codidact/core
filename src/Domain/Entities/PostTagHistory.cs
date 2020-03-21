using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class PostTagHistory : AuditEntity<PostTag>
    {
        public long Id { get; set; }
        public long PostId { get; set; }
        public long TagId { get; set; }

    }
}
