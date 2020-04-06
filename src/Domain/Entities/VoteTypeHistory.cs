using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class VoteTypeHistory : AuditEntity<VoteType>
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }

    }
}
