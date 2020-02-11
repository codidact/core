using Codidact.Application.Common.Interfaces;
using Codidact.Domain.Common;
using Codidact.Domain.Common.Interfaces;
using Codidact.Domain.Entities;
using Codidact.Domain.Extensions;
using Codidact.Infrastructure.Identity;
using Codidact.Infrastructure.Persistence.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Codidact.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationDbContext(
                DbContextOptions<ApplicationDbContext> options,
                ICurrentUserService currentUserService
            ) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<TrustLevel> TrustLevels { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateDateAt = DateTime.UtcNow;
                        entry.Entity.CreatedByMemberId = _currentUserService.GetMemberId();
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedAt = DateTime.UtcNow;
                        entry.Entity.LastModifiedByMemberId = _currentUserService.GetMemberId();
                        break;
                    case EntityState.Deleted:
                        if (entry.Entity is ISoftDeletable deletable)
                        {
                            // Unchanged so only the relevant columns are sent to the db
                            entry.State = EntityState.Unchanged;

                            deletable.DeletedAt = DateTime.UtcNow;
                            deletable.IsDeleted = true;
                            deletable.DeletedByMemberId = _currentUserService.GetMemberId();
                        }
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.UseSerialColumns();

            RenameEntitiesToSnakeCase(modelBuilder);

            SetGlobalQueryFiltersToSoftDeletableEntities(modelBuilder);

            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureIdentity();
        }

        private static void SetGlobalQueryFiltersToSoftDeletableEntities(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entity.ClrType))
                {
                    var isDeletableProperty = entity.FindProperty(nameof(ISoftDeletable.IsDeleted));
                    var parameter = Expression.Parameter(entity.ClrType, "p");
                    var equalExpression = Expression.Equal(
                            Expression.Property(parameter, isDeletableProperty.PropertyInfo),
                            Expression.Constant(false)
                        );
                    var filter = Expression.Lambda(equalExpression, parameter);
                    entity.SetQueryFilter(filter);
                }
            }
        }

        private static void RenameEntitiesToSnakeCase(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToSnakeCase());

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToSnakeCase());
                }

                foreach (var key in entity.GetKeys())
                {
                    key.SetName(key.GetName().ToSnakeCase());
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(key.GetConstraintName().ToSnakeCase());
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.SetName(index.GetName().ToSnakeCase());
                }
            }
        }
    }
}
