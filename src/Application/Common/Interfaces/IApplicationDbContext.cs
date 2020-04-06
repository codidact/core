using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Codidact.Core.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<CategoryHistory> CategoryHistories { get; set; }
        DbSet<CategoryPostType> CategoryPostTypes { get; set; }
        DbSet<CategoryPostTypeHistory> CategoryPostTypeHistories { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<CommentHistory> CommentHistories { get; set; }
        DbSet<CommentVote> CommentVotes { get; set; }
        DbSet<CommentVoteHistory> CommentVoteHistories { get; set; }
        DbSet<Member> Members { get; set; }
        DbSet<MemberHistory> MemberHistories { get; set; }
        DbSet<MemberPrivilege> MemberPrivileges { get; set; }
        DbSet<MemberPrivilegeHistory> MemberPrivilegeHistories { get; set; }
        DbSet<MemberSocialMediaType> MemberSocialMediaTypes { get; set; }
        DbSet<MemberSocialMediaTypeHistory> MemberSocialMediaTypeHistories { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<PostDuplicatePost> PostDuplicatePosts { get; set; }
        DbSet<PostDuplicatePostHistory> PostDuplicatePostHistories { get; set; }
        DbSet<PostHistory> PostHistories { get; set; }
        DbSet<PostStatus> PostStatuss { get; set; }
        DbSet<PostStatusHistory> PostStatusHistories { get; set; }
        DbSet<PostStatusType> PostStatusTypes { get; set; }
        DbSet<PostStatusTypeHistory> PostStatusTypeHistories { get; set; }
        DbSet<PostTag> PostTags { get; set; }
        DbSet<PostTagHistory> PostTagHistories { get; set; }
        DbSet<PostType> PostTypes { get; set; }
        DbSet<PostTypeHistory> PostTypeHistories { get; set; }
        DbSet<PostVote> PostVotes { get; set; }
        DbSet<PostVoteHistory> PostVoteHistories { get; set; }
        DbSet<Privilege> Privileges { get; set; }
        DbSet<PrivilegeHistory> PrivilegeHistories { get; set; }
        DbSet<Setting> Settings { get; set; }
        DbSet<SettingHistory> SettingHistories { get; set; }
        DbSet<SocialMediaType> SocialMediaTypes { get; set; }
        DbSet<SocialMediaTypeHistory> SocialMediaTypeHistories { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<TagHistory> TagHistories { get; set; }
        DbSet<TrustLevel> TrustLevels { get; set; }
        DbSet<TrustLevelHistory> TrustLevelHistories { get; set; }
        DbSet<VoteType> VoteTypes { get; set; }
        DbSet<VoteTypeHistory> VoteTypeHistories { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
