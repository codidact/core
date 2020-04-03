using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codidact.Core.Application.Common.Interfaces;
using Codidact.Core.Domain.Common;
using Codidact.Core.Domain.Entities;
using Codidact.Core.WebApp.Models;
using Xunit;

namespace Codidact.Core.WebApp.IntegrationTests.Controllers
{
    public class MemberControllerTests :
        IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public MemberControllerTests(
            CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateMember()
        {
            var client = _factory.CreateClient();

            var content = IntegrationTestHelper.GetRequestContent(
                new MemberRequest
                {
                    DisplayName = "John",
                    UserId = 1,
                });

            var response = await client.PostAsync($"api/v1/member/create", content);

            response.EnsureSuccessStatusCode();

            var result = await IntegrationTestHelper.GetResponseContent<EntityResult<Member>>(response);

            Assert.True(result.Success);

            var context = _factory.Services.GetService(typeof(IApplicationDbContext))
                as IApplicationDbContext;

            var createdMember = context.Members.FirstOrDefault(member => member.Id == result.Id);

            Assert.NotNull(createdMember);
        }
    }
}
