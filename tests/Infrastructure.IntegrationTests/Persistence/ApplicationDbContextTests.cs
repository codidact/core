using System;
using System.Linq;
using System.Threading.Tasks;
using Codidact.Domain.Entities;
using Codidact.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Infrastructure.IntegrationTests.Persistence
{
    public class ApplicationDbContextTests
    {
        private readonly ApplicationDbContext _sutContext
            = new ApplicationDbContext(
                new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options
                );

        [Fact]
        public async Task SaveChangesShouldAssignAnAutoIncrementId()
        {
            var member = new Member
            {
                DisplayName = "John Doe",
                Bio = "Not to be confused with John Galt",
                Email = "john@gmail.com"
            };
            _sutContext.Add(member);
            await _sutContext.SaveChangesAsync();

            Assert.True(member.Id > 0);
        }

        [Fact]
        public async Task AbleToFindIdFromTable()
        {
            var member = new Member
            {
                DisplayName = "John Doe",
                Bio = "Not to be confused with John Galt",
                Email = "john@gmail.com"
            };
            _sutContext.Add(member);
            _sutContext.SaveChanges();

            var foundMember = await _sutContext.FindAsync<Member>(member.Id);
            Assert.NotNull(foundMember);
        }

        [Fact]
        public void AssureTableNameIsSnakeCase()
        {
            var trustLevelModel = _sutContext.Model.GetEntityTypes().FirstOrDefault(type => type.ClrType == typeof(TrustLevel));

            Assert.NotNull(trustLevelModel);
            Assert.Equal("trust_level", trustLevelModel.GetTableName());
        }
    }
}
