using Codidact.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace Infrastructure.IntegrationTests
{
    /// <summary>
    /// Mock for the current community service
    /// </summary>
    public class TestCurrentCommunityService : ICurrentCommunityService
    {
        public static long? CommunityId = 1;

        public Task<long?> GetCurrentCommunityIdAsync()
        {
            return Task.FromResult(CommunityId);
        }
    }
}
