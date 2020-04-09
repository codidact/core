using Codidact.Core.Application.Common.Interfaces;
using Codidact.Core.Domain.Common;
using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Codidact.Core.Application.Members
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

        public async Task<EntityResult> Create(Member member)
        {
            _context.Members.Add(member);

            await _context.SaveChangesAsync(CancellationToken.None);

            return new EntityResult(member.Id);
        }
    }
}
