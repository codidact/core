using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class MemberPrivilegeHistory : AuditEntity<MemberPrivilege>
    {
        public long Id { get; set; }
        public long MemberId { get; set; }
        public long PrivilegeId { get; set; }
        public bool IsSuspended { get; set; }
        public DateTime? PrivilegeSuspensionStartAt { get; set; }
        public DateTime? PrivilegeSuspensionEndAt { get; set; }
    }
}
