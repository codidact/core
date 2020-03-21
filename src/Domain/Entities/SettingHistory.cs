using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class SettingHistory : AuditEntity<Setting>
    {
        public long Id { get; set; }
    }
}
