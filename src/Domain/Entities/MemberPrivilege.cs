using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class MemberPrivilege : AuditableEntity
    {
        public long Id { get; set; }
        public long MemberId { get; set; }
        public long PrivilegeId { get; set; }
        public bool IsSuspended { get; set; }
        public DateTime? PrivilegeSuspensionStartAt { get; set; }
        public DateTime? PrivilegeSuspensionEndAt { get; set; }

        public virtual Member Member { get; set; }
        public virtual Privilege Privilege { get; set; }
        public virtual Member CreatedByMember { get; set; }
        public virtual Member LastModifiedByMember { get; set; }

    }
}
