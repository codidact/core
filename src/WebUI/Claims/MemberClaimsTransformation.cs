using Codidact.Application.Members;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Codidact.WebUI.Claims
{
    /// <summary>
    /// Transforms the claim to include a member Id
    /// </summary>
    public class MemberClaimsTransformation : IClaimsTransformation
    {
        private readonly IMembersRepository _membersRepository;
        public MemberClaimsTransformation(IMembersRepository membersRepository)
        {
            _membersRepository = membersRepository;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var userIdValue = principal.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            if (string.IsNullOrEmpty(userIdValue))
            {
                throw new Exception("Invalid Token. Does not contain user id");
            }
            long userId = long.Parse(userIdValue);

            var member = await _membersRepository.GetSingleByUserIdAsync(userId).ConfigureAwait(false);

            if (member == null)
            {
                throw new Exception($"Member for user {userId} found. User Id has no correlated member");
            }

            var identity = principal.Identities.First();

            identity.AddClaim(new Claim("codidact_member_id", member.Id.ToString()));

            return principal;
        }
    }
}
