using Codidact.Core.Domain.Common;

namespace Codidact.Core.Domain.Entities
{
    public partial class MemberSocialMediaTypeHistory : AuditEntity<MemberSocialMediaType>
    {
        public long Id { get; set; }
        public long SocialMediaId { get; set; }
        public long MemberId { get; set; }
        public string Content { get; set; }
    }
}
