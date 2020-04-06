using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class Privilege : AuditableEntity
    {
        public Privilege()
        {
            MemberPrivilege = new HashSet<MemberPrivilege>();
        }

        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public virtual Member CreatedByMember { get; set; }
        public virtual Member LastModifiedByMember { get; set; }
        public virtual ICollection<MemberPrivilege> MemberPrivilege { get; set; }
    }
}
