using Codidact.Core.Domain.Common;
using Codidact.Core.Domain.Entities;
using System.Threading.Tasks;

namespace Codidact.Core.Application.Members
{
    /// <summary>
    /// Repository for Member Entity
    /// </summary>
    public interface IMembersRepository
    {
        /// <summary>
        /// Gets the member based on the user id
        /// </summary>
        /// <param name="userId">The user id to search for</param>
        /// <returns>Member</returns>
        Task<Member> GetSingleByUserIdAsync(long userId);

        /// <summary>
        /// Creates a new member
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        Task<EntityResult> Create(Member member);
    }
}
