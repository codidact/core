using Codidact.Application.Common.Interfaces;
using Codidact.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Codidact.Infrastructure.Identity
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Retreives the user id of the current user.
        /// </summary>
        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        /// <summary>
        /// Retreives the member id of the current user.
        /// </summary>
        public long GetMemberId()
        {
            string memberId = _httpContextAccessor.HttpContext.User.FindFirstValue(nameof(ApplicationUser.MemberId));
            if (!string.IsNullOrEmpty(memberId))
            {
                return long.Parse(memberId);
            }
            else
            {
                throw new Exception("Claim for memberId is missing in token");
            }
        }
    }
}
