using Codidact.Application.Common.Interfaces;

namespace Codidact.Infrastructure.IntegrationTests
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
