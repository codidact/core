using Codidact.Application.Common.Interfaces;
using Codidact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Codidact.Application.Communities
{
    /// <summary>
    /// Repository for communities
    /// </summary>
    public class CommunityRepository : ICommunityRepository
    {
        private readonly IApplicationDbContext _context;
        public CommunityRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a community by name asynchrounously
        /// </summary>
        /// <param name="name">Name of the community</param>
        /// <returns>A task with a community</returns>
        public async Task<Community> GetAsync(string name)
        {
            return await _context.Communities
                    .FirstOrDefaultAsync(community => community.Name == name)
                    .ConfigureAwait(false);
        }
    }
}
