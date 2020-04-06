using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class TrustLevelHistory : AuditEntity<TrustLevel>
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string Explanation { get; set; }

    }
}
