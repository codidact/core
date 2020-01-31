using Codidact.Application.Repositories;
using Codidact.Application.Repositories.Communities;
using Codidact.Infrastructure.Persistence;
using Codidact.Infrastructure.IntegrationTests;
using Codidact.Infrastructure.IntegrationTests.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Codidact.Application.UnitTests.Repositories
{
    public class CommunityRepositoryTest
    {
        private readonly ICommunityRepository _communityRepository;

        public CommunityRepositoryTest()
        {
            var _sutContext = new ApplicationDbContext(
                  new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options,
                  new TestCurrentCommunityService()
              ).Seed();

            _communityRepository = new CommunityRepository(_sutContext);
        }

        [Fact]
        public async Task GetsCommunityByName()
        {
            var community = await _communityRepository.GetAsync("Meta");
            Assert.NotNull(community);
        }
    }
}
