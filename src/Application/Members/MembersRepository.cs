using Codidact.Application.Common.Interfaces;
using Codidact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Codidact.Application.Members
{
    public class MembersRepository : IMembersRepository
    {
        private readonly IApplicationDbContext _context;
        public MembersRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Member> GetSingleByUserIdAsync(long userId)
        {
            return await _context.Members.SingleAsync(member => member.UserId == userId).ConfigureAwait(false);
        }
    }
}
