using Codidact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Codidact.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Member> Members { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
