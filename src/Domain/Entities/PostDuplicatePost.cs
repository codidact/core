using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class PostDuplicatePost : AuditableEntity
    {
        public long Id { get; set; }
        public long OriginalPostId { get; set; }
        public long DuplicatePostId { get; set; }

        public virtual Post DuplicatePost { get; set; }
        public virtual Post OriginalPost { get; set; }
        public virtual Member CreatedByMember { get; set; }
        public virtual Member LastModifiedByMember { get; set; }

    }
}
