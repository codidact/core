using Codidact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Codidact.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Member> Members { get; set; }
        DbSet<TrustLevel> TrustLevels { get; set; }
        DbSet<Community> Communities { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
