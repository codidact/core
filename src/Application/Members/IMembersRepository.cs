using Codidact.Domain.Entities;
using System.Threading.Tasks;

namespace Codidact.Application.Members
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
    }
}
