using Codidact.Core.Application.Common.Interfaces;

namespace Codidact.Core.Infrastructure.IntegrationTests
{
    public class CurrentUserServiceMock : ICurrentUserService
    {
        public long GetMemberId()
        {
            return 1;
        }

        public string GetUserId()
        {
            return "1";
        }
    }
}
