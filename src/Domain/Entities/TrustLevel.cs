using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class TrustLevel : AuditableEntity
    {
        public TrustLevel()
        {
            Category = new HashSet<Category>();
            Member = new HashSet<Member>();
        }

        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string Explanation { get; set; }
        public virtual Member CreatedByMember { get; set; }
        public virtual Member LastModifiedByMember { get; set; }

        public virtual ICollection<Category> Category { get; set; }
        public virtual ICollection<Member> Member { get; set; }
    }
}
