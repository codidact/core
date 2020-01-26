using System;
using System.Linq;
using System.Threading.Tasks;
using Codidact.Domain.Entities;
using Codidact.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Codidact.Infrastructure.IntegrationTests.Persistence
{
    public class ApplicationDbContextTests
    {
        private readonly ApplicationDbContext _sutContext;

        public ApplicationDbContextTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var currentUserService = new CurrentUserServiceMock();
            _sutContext = new ApplicationDbContext(options, currentUserService);
        }

        [Fact]
        public async Task SaveChangesShouldAssignAnAutoIncrementId()
        {
            var member = new Member
            {
                DisplayName = "John Doe",
                Bio = "Not to be confused with John Galt",
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

        [Fact]
        public async Task CreatedDateAtIsModifiedOnInsert()
        {
            var member = new Member
            {
                DisplayName = "John Doe",
                Bio = "Not to be confused with John Galt",
            };
            _sutContext.Add(member);
            await _sutContext.SaveChangesAsync();

            Assert.Equal(member.CreateDateAt.Date, DateTime.UtcNow.Date);
        }

        [Fact]
        public async Task LastModifiedDateAtIsModifiedOnUpdate()
        {
            var member = new Member
            {
                DisplayName = "John Doe",
                Bio = "Not to be confused with John Galt",
            };
            _sutContext.Add(member);
            await _sutContext.SaveChangesAsync();

            Assert.Null(member.LastModifiedAt);

            member.DisplayName = "John Galt";
            _sutContext.Update(member);
            await _sutContext.SaveChangesAsync();

            Assert.True(member.LastModifiedAt.HasValue);
            Assert.Equal(member.LastModifiedAt.Value.Date, DateTime.UtcNow.Date);
        }

        [Fact]
        public async Task DeletingASoftDeletableShouldOnlyUpdateIsDeletedAndDate()
        {
            var member = new Member
            {
                DisplayName = "John Doe",
                Bio = "Not to be confused with John Galt",
            };
            _sutContext.Add(member);
            await _sutContext.SaveChangesAsync();

            _sutContext.Remove(member);
            await _sutContext.SaveChangesAsync();

            Assert.True(member.IsDeleted);
            Assert.NotNull(member.DeletedAt);
            Assert.Equal(member.DeletedAt.Value.Date, DateTime.UtcNow.Date);
        }


        [Fact]
        public async Task DeletedEntitiesDontShowUpInQuery()
        {
            var member = new Member
            {
                DisplayName = "John Doe",
                Bio = "Not to be confused with John Galt",
            };
            _sutContext.Add(member);
            await _sutContext.SaveChangesAsync();

            _sutContext.Remove(member);
            await _sutContext.SaveChangesAsync();

            Assert.Null(_sutContext.Members.FirstOrDefault(mem => mem.Id == member.Id));

            member.IsDeleted = false;
            _sutContext.Update(member);
            await _sutContext.SaveChangesAsync();

            Assert.NotNull(_sutContext.Members.FirstOrDefault(mem => mem.Id == member.Id));
        }
    }

}
