using Codidact.Domain.Entities;

namespace Codidact.Domain.Common.Interfaces
{
    public interface ICommunityable
    {
        public long CommunityId { get; set; }
        public Community Community { get; set; }
    }
}
