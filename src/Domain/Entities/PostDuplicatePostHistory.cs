using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class PostDuplicatePostHistory : AuditEntity<PostDuplicatePost>
    {
        public long Id { get; set; }
        public long OriginalPostId { get; set; }
        public long DuplicatePostId { get; set; }
    }
}
