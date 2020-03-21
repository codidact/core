using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class PostTypeHistory : AuditEntity<PostType>
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

    }
}
