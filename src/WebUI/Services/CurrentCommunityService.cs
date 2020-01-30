using Codidact.Application.Common.Interfaces;
using Codidact.Application.Repositories.Communities;
using Codidact.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Threading.Tasks;

namespace Codidact.WebUI.Services
{
    /// <summary>
    /// A class with information regarding the current community
    /// </summary>
    public class CurrentCommunityService : ICurrentCommunityService
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly ICommunityRepository _communityRepository;

        public CurrentCommunityService(
            ICommunityRepository communityRepository,
            IActionContextAccessor actionContextAccessor)
        {
            _actionContextAccessor = actionContextAccessor;
            _communityRepository = communityRepository;
        }

        /// <summary>
        /// Retreives Community Id from current request
        /// </summary>
        /// <returns>Community Id</returns>
        public async Task<long> GetCurrentCommunityIdAsync()
        {
            if (_actionContextAccessor.ActionContext == null)
            {
                throw new CommunityInvalidException("ActionContext needs to exists in order to access community data");
            }

            var routingData = _actionContextAccessor.ActionContext.RouteData;

            if (!routingData.Values.ContainsKey("community"))
            {
                throw new CommunityInvalidException("When accessing community data, community name must be specified in route");
            }

            var communityName = routingData.Values["community"].ToString();
            var community = await _communityRepository.GetAsync(communityName).ConfigureAwait(false);
            if (community == null)
            {
                throw new CommunityInvalidException("Community not found");
            }
            return community.Id;
        }
    }
}
