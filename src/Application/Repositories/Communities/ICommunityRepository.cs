using Codidact.Domain.Entities;
using System.Threading.Tasks;

namespace Codidact.Application.Repositories.Communities
{
    /// <summary>
    /// Repository for communities
    /// </summary>
    public interface ICommunityRepository
    {
        /// <summary>
        /// Returns a community by name asynchrounously
        /// </summary>
        /// <param name="name">Name of the community</param>
        /// <returns>A task with a community</returns>
        public Task<Community> GetAsync(string name);
    }
}
