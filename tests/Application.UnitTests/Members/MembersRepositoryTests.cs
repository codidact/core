using Codidact.Core.Application.Members;
using System.Threading.Tasks;
using Xunit;

namespace Codidact.Core.Application.IntegrationTests.Members
{
    public class MembersRepositoryTests
    {
        private readonly IMembersRepository _memberRepository;
        public MembersRepositoryTests()
        {
            _memberRepository = new MembersRepository(ApplicationDbContextFactory.Create());
        }

        [Fact]
        public async Task GetMemberByUserId()
        {
            var member = await _memberRepository.GetSingleByUserIdAsync(1);

            Assert.NotNull(member);
            Assert.Equal(1, member.UserId);
        }
    }
}
