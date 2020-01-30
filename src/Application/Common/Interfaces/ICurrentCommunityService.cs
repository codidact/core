using System.Threading.Tasks;

namespace Codidact.Application.Common.Interfaces
{
    public interface ICurrentCommunityService
    {
        public Task<long?> GetCurrentCommunityIdAsync();
    }
}
