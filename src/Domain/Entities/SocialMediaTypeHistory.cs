using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class SocialMediaTypeHistory : AuditEntity<SocialMediaType>
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string AccountUrl { get; set; }

    }
}
