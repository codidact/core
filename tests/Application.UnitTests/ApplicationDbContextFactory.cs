using Codidact.Application.Common.Interfaces;
using Codidact.Domain.Entities;
using Codidact.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;

namespace Codidact.Application.IntegrationTests
{

    public static class ApplicationDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var currentUserServiceMock = new Mock<ICurrentUserService>();
            currentUserServiceMock.Setup(m => m.GetMemberId())
                .Returns(1);

            currentUserServiceMock.Setup(m => m.GetUserId())
                .Returns("1");

            var context = new ApplicationDbContext(
                options,
                currentUserServiceMock.Object);

            context.Database.EnsureCreated();

            SeedSampleData(context);

            return context;
        }

        public static void SeedSampleData(ApplicationDbContext context)
        {
            var member = new Member
            {
                DisplayName = "John Doe",
                Bio = "Not to be confused with John Galt",
                UserId = 1
            };
            context.Add(member);

            context.SaveChanges();
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
