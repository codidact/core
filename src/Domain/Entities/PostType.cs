using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class PostType : AuditableEntity
    {
        public PostType()
        {
            Post = new HashSet<Post>();
        }

        public Enums.PostType Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public virtual Member CreatedByMember { get; set; }
        public virtual Member LastModifiedByMember { get; set; }

        public virtual ICollection<Post> Post { get; set; }
    }
}
