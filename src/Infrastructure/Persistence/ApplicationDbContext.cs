using Codidact.Application.Common.Interfaces;
using Codidact.Domain.Common;
using Codidact.Domain.Common.Interfaces;
using Codidact.Domain.Entities;
using Codidact.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Codidact.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentCommunityService _currentCommunityService;

        public ApplicationDbContext(DbContextOptions options,
            ICurrentCommunityService currentCommunityService)
            : base(options)
        {
            _currentCommunityService = currentCommunityService;
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<MemberCommunity> MemberCommunities { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<TrustLevel> TrustLevels { get; set; }
        public DbSet<TrustLevelCommunity> TrustLevelCommunities { get; set; }


        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateDateAt = DateTime.UtcNow;
                        // TODO: Once an identity service is added 
                        // set the current Member id to CreatedByMemberId
                        if (entry.Entity is ICommunityScopable communityable)
                        {
                            var communityId = await _currentCommunityService
                                                        .GetCurrentCommunityIdAsync()
                                                        .ConfigureAwait(false);
                            entry.Property("CommunityId").CurrentValue = communityId.Value;
                        }
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedAt = DateTime.UtcNow;
                        // TODO: Once an identity service is added
                        // set the current Member id to LastModifiedByMemberId
                        break;
                    case EntityState.Deleted:
                        if (entry.Entity is ISoftDeletable deletable)
                        {
                            // Unchanged so only the relevant columns are sent to the db
                            entry.State = EntityState.Unchanged;

                            deletable.DeletedAt = DateTime.UtcNow;
                            deletable.IsDeleted = true;
                            // TODO: Once an identity service is added
                            // set the current Member id to DeletedByMemberId
                        }
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected async override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.UseSerialColumns();

            RenameEntitiesToSnakeCase(modelBuilder);

            SetGlobalQueryFiltersToSoftDeletableEntities(modelBuilder);

            await SetGlobalQueryFiltersToCommunityEntities(modelBuilder);

            base.OnModelCreating(modelBuilder);
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

        private async Task SetGlobalQueryFiltersToCommunityEntities(ModelBuilder modelBuilder)
        {
            long? communityId = await _currentCommunityService.GetCurrentCommunityIdAsync();
            if (communityId.HasValue)
            {
                foreach (var entity in modelBuilder.Model.GetEntityTypes())
                {
                    if (typeof(ICommunityScopable).IsAssignableFrom(entity.ClrType))
                    {
                        var property = modelBuilder.Entity(entity.ClrType).Property<long>("CommunityId");
                        var parameter = Expression.Parameter(entity.ClrType, "e");
                        var body = Expression.Equal(
                            Expression.Call(typeof(EF),
                            nameof(EF.Property),
                            new[] { typeof(long) },
                            parameter,
                            Expression.Constant("CommunityId")),
                            Expression.Constant(communityId.Value));
                        var filter = Expression.Lambda(body, parameter);
                        entity.SetQueryFilter(filter);
                    }
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
