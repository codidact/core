using Codidact.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Codidact.Infrastructure.Identity
{
    /// <summary>
    /// Application user for identity management.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Connected member Id.
        /// </summary>
        public long? MemberId { get; set; }

        /// <summary>
        /// Instance of connected Member.
        /// </summary>
        public Member Member { get; set; }
    }
}