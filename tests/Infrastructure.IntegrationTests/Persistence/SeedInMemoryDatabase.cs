using Codidact.Domain.Entities;
using Codidact.Domain.Enums;
using Codidact.Infrastructure.Persistence;

namespace Infrastructure.IntegrationTests.Persistence
{
    public static class SeedInMemoryDatabase
    {
        public static ApplicationDbContext Seed(this ApplicationDbContext dbContext)
        {
            dbContext.Add(new Community
            {
                Id = 1,
                Name = "Meta",
                Tagline = "Meta Discussions",
                Status = CommunityStatus.Active,
            });
            dbContext.SaveChanges();

            return dbContext;
        }
    }
}
