using Codidact.Application.Common.Interfaces;
using Codidact.Application.Repositories.Communities;
using Codidact.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Codidact.WebUI.Services
{
    /// <summary>
    /// A class with information regarding the current community
    /// </summary>
    public class CurrentCommunityService : ICurrentCommunityService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CurrentCommunityService(
            IServiceScopeFactory serviceScopeFactory
          )
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        /// <summary>
        /// Retreives Community Id from current request
        /// </summary>
        /// <returns>Community Id</returns>
        public async Task<long?> GetCurrentCommunityIdAsync()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var actionContextAcessor = scope.ServiceProvider.GetService<IActionContextAccessor>();
            var communityRepository = scope.ServiceProvider.GetRequiredService<ICommunityRepository>();

            if (actionContextAcessor?.ActionContext == null)
            {
                // ActionContext needs to exists in order to access community data
                return null;
            }

            var routingData = actionContextAcessor.ActionContext.RouteData;

            if (!routingData.Values.ContainsKey("community"))
            {
                // When accessing community data, community name must be specified in route
                return null;
            }

            var communityName = routingData.Values["community"].ToString();
            var community = await communityRepository.GetAsync(communityName).ConfigureAwait(false);
            if (community == null)
            {
                throw new CommunityInvalidException("Community not found");
            }
            return community.Id;
        }
    }
}
