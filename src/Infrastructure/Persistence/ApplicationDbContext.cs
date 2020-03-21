using Codidact.Core.Application.Common.Interfaces;
using Codidact.Core.Domain.Common;
using Codidact.Core.Domain.Common.Interfaces;
using Codidact.Core.Domain.Entities;
using Codidact.Core.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Codidact.Core.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationDbContext(
                DbContextOptions<ApplicationDbContext> options,
                ICurrentUserService currentUserService
            ) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryHistory> CategoryHistories { get; set; }
        public virtual DbSet<CategoryPostType> CategoryPostTypes { get; set; }
        public virtual DbSet<CategoryPostTypeHistory> CategoryPostTypeHistories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CommentHistory> CommentHistories { get; set; }
        public virtual DbSet<CommentVote> CommentVotes { get; set; }
        public virtual DbSet<CommentVoteHistory> CommentVoteHistories { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberHistory> MemberHistories { get; set; }
        public virtual DbSet<MemberPrivilege> MemberPrivileges { get; set; }
        public virtual DbSet<MemberPrivilegeHistory> MemberPrivilegeHistories { get; set; }
        public virtual DbSet<MemberSocialMediaType> MemberSocialMediaTypes { get; set; }
        public virtual DbSet<MemberSocialMediaTypeHistory> MemberSocialMediaTypeHistories { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostDuplicatePost> PostDuplicatePosts { get; set; }
        public virtual DbSet<PostDuplicatePostHistory> PostDuplicatePostHistories { get; set; }
        public virtual DbSet<PostHistory> PostHistories { get; set; }
        public virtual DbSet<PostStatus> PostStatuss { get; set; }
        public virtual DbSet<PostStatusHistory> PostStatusHistories { get; set; }
        public virtual DbSet<PostStatusType> PostStatusTypes { get; set; }
        public virtual DbSet<PostStatusTypeHistory> PostStatusTypeHistories { get; set; }
        public virtual DbSet<PostTag> PostTags { get; set; }
        public virtual DbSet<PostTagHistory> PostTagHistories { get; set; }
        public virtual DbSet<PostType> PostTypes { get; set; }
        public virtual DbSet<PostTypeHistory> PostTypeHistories { get; set; }
        public virtual DbSet<PostVote> PostVotes { get; set; }
        public virtual DbSet<PostVoteHistory> PostVoteHistories { get; set; }
        public virtual DbSet<Privilege> Privileges { get; set; }
        public virtual DbSet<PrivilegeHistory> PrivilegeHistories { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<SettingHistory> SettingHistories { get; set; }
        public virtual DbSet<SocialMediaType> SocialMediaTypes { get; set; }
        public virtual DbSet<SocialMediaTypeHistory> SocialMediaTypeHistories { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TagHistory> TagHistories { get; set; }
        public virtual DbSet<TrustLevel> TrustLevels { get; set; }
        public virtual DbSet<TrustLevelHistory> TrustLevelHistories { get; set; }
        public virtual DbSet<VoteType> VoteTypes { get; set; }
        public virtual DbSet<VoteTypeHistory> VoteTypeHistories { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
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
            {
                modelBuilder.HasPostgresEnum("audit", "history_activity_type", new[] { "CREATE", "UPDATE_BEFORE", "UPDATE_AFTER", "DELETE" })
                    .HasPostgresExtension("adminpack");

                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

                modelBuilder.UseSerialColumns();

                RenameEntitiesToSnakeCase(modelBuilder);

                SetGlobalQueryFiltersToSoftDeletableEntities(modelBuilder);

                base.OnModelCreating(modelBuilder);
            }
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

                if (entity.ClrType.BaseType.IsGenericType &&
                    entity.ClrType.BaseType.GetGenericTypeDefinition() == typeof(AuditEntity<>))
                {
                    entity.SetSchema("audit");
                }

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
