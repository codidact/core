using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class CategoryPostType : AuditableEntity
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }
        public long PostTypeId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Member CreatedByMember { get; set; }
        public virtual Member LastModifiedByMember { get; set; }
    }
}
