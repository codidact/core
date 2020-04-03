using Codidact.Core.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Codidact.Core.Infrastructure.Identity
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
            var userClaim = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userClaim.Value))
            {
                return userClaim.Value;
            }
            else
            {
                throw new Exception("Claim for UserId is missing in token");
            }
        }

        /// <summary>
        /// Retreives the member id of the current user.
        /// </summary>
        public long GetMemberId()
        {
            var context = _httpContextAccessor.HttpContext;
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Claims as ClaimsIdentity;
            Claim memberClaim = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(claim => claim.Type == "codidact_member_id");
            if (!string.IsNullOrEmpty(memberClaim?.Value))
            {
                return long.Parse(memberClaim.Value);
            }
            else
            {
                // TODO: remove this hack. Currently 0, maybe don't set memberId if its not existant.;
                return 0;
                // throw new Exception("Claim for memberId is missing in token");
            }
        }
    }
}
