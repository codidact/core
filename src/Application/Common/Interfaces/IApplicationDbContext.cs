using Codidact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Codidact.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberCommunity> MemberCommunities { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<TrustLevel> TrustLevels { get; set; }
        public DbSet<TrustLevelCommunity> TrustLevelCommunities { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
