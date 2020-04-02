using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;
using Codidact.Core.Domain.Common.Interfaces;

namespace Codidact.Core.Domain.Entities
{
    public partial class Member : AuditableEntity, ISoftDeletable
    {
        public Member()
        {
            CategoryCreatedByMember = new HashSet<Category>();
            CategoryHistory = new HashSet<CategoryHistory>();
            CategoryLastModifiedByMember = new HashSet<Category>();
            CategoryPostTypeCreatedByMember = new HashSet<CategoryPostType>();
            CategoryPostTypeHistory = new HashSet<CategoryPostTypeHistory>();
            CategoryPostTypeLastModifiedByMember = new HashSet<CategoryPostType>();
            CommentCreatedByMember = new HashSet<Comment>();
            CommentHistory = new HashSet<CommentHistory>();
            CommentLastModifiedByMember = new HashSet<Comment>();
            CommentMember = new HashSet<Comment>();
            CommentVoteCreatedByMember = new HashSet<CommentVote>();
            CommentVoteHistory = new HashSet<CommentVoteHistory>();
            CommentVoteLastModifiedByMember = new HashSet<CommentVote>();
            CommentVoteMember = new HashSet<CommentVote>();
            InverseCreatedByMember = new HashSet<Member>();
            InverseLastModifiedByMember = new HashSet<Member>();
            MemberHistory = new HashSet<MemberHistory>();
            MemberPrivilegeCreatedByMember = new HashSet<MemberPrivilege>();
            MemberPrivilegeHistory = new HashSet<MemberPrivilegeHistory>();
            MemberPrivilegeLastModifiedByMember = new HashSet<MemberPrivilege>();
            MemberPrivilegeMember = new HashSet<MemberPrivilege>();
            MemberSocialMediaTypeCreatedByMember = new HashSet<MemberSocialMediaType>();
            MemberSocialMediaTypeHistory = new HashSet<MemberSocialMediaTypeHistory>();
            MemberSocialMediaTypeLastModifiedByMember = new HashSet<MemberSocialMediaType>();
            MemberSocialMediaTypeMember = new HashSet<MemberSocialMediaType>();
            PostCreatedByMember = new HashSet<Post>();
            PostDuplicatePostCreatedByMember = new HashSet<PostDuplicatePost>();
            PostDuplicatePostHistory = new HashSet<PostDuplicatePostHistory>();
            PostDuplicatePostLastModifiedByMember = new HashSet<PostDuplicatePost>();
            PostHistory = new HashSet<PostHistory>();
            PostLastModifiedByMember = new HashSet<Post>();
            PostMember = new HashSet<Post>();
            PostStatusCreatedByMember = new HashSet<PostStatus>();
            PostStatusHistory = new HashSet<PostStatusHistory>();
            PostStatusLastModifiedByMember = new HashSet<PostStatus>();
            PostStatusTypeCreatedByMember = new HashSet<PostStatusType>();
            PostStatusTypeHistory = new HashSet<PostStatusTypeHistory>();
            PostStatusTypeLastModifiedByMember = new HashSet<PostStatusType>();
            PostTagCreatedByMember = new HashSet<PostTag>();
            PostTagHistory = new HashSet<PostTagHistory>();
            PostTagLastModifiedByMember = new HashSet<PostTag>();
            PostTypeCreatedByMember = new HashSet<PostType>();
            PostTypeHistory = new HashSet<PostTypeHistory>();
            PostTypeLastModifiedByMember = new HashSet<PostType>();
            PostVoteCreatedByMember = new HashSet<PostVote>();
            PostVoteHistory = new HashSet<PostVoteHistory>();
            PostVoteLastModifiedByMember = new HashSet<PostVote>();
            PostVoteMember = new HashSet<PostVote>();
            PrivilegeCreatedByMember = new HashSet<Privilege>();
            PrivilegeHistory = new HashSet<PrivilegeHistory>();
            PrivilegeLastModifiedByMember = new HashSet<Privilege>();
            SettingCreatedByMember = new HashSet<Setting>();
            SettingHistory = new HashSet<SettingHistory>();
            SettingLastModifiedByMember = new HashSet<Setting>();
            SocialMediaTypeCreatedByMember = new HashSet<SocialMediaType>();
            SocialMediaTypeHistory = new HashSet<SocialMediaTypeHistory>();
            SocialMediaTypeLastModifiedByMember = new HashSet<SocialMediaType>();
            TagCreatedByMember = new HashSet<Tag>();
            TagHistory = new HashSet<TagHistory>();
            TagLastModifiedByMember = new HashSet<Tag>();
            TrustLevelCreatedByMember = new HashSet<TrustLevel>();
            TrustLevelHistory = new HashSet<TrustLevelHistory>();
            TrustLevelLastModifiedByMember = new HashSet<TrustLevel>();
            VoteTypeCreatedByMember = new HashSet<VoteType>();
            VoteTypeHistory = new HashSet<VoteTypeHistory>();
            VoteTypeLastModifiedByMember = new HashSet<VoteType>();
        }

        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public string ProfilePictureLink { get; set; }
        public bool IsTemporarilySuspended { get; set; }
        public DateTime? TemporarySuspensionEndAt { get; set; }
        public string TemporarySuspensionReason { get; set; }
        public long TrustLevelId { get; set; }
        public long? NetworkAccountId { get; set; }
        public bool IsModerator { get; set; }
        public bool IsAdministrator { get; set; }
        public bool? IsSyncedWithNetworkAccount { get; set; }
        public long UserId { get; set; }

        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeletedByMemberId { get; set; }


        public virtual TrustLevel TrustLevel { get; set; }
        public virtual Member CreatedByMember { get; set; }
        public virtual Member LastModifiedByMember { get; set; }
        public virtual ICollection<Category> CategoryCreatedByMember { get; set; }
        public virtual ICollection<CategoryHistory> CategoryHistory { get; set; }
        public virtual ICollection<Category> CategoryLastModifiedByMember { get; set; }
        public virtual ICollection<CategoryPostType> CategoryPostTypeCreatedByMember { get; set; }
        public virtual ICollection<CategoryPostTypeHistory> CategoryPostTypeHistory { get; set; }
        public virtual ICollection<CategoryPostType> CategoryPostTypeLastModifiedByMember { get; set; }
        public virtual ICollection<Comment> CommentCreatedByMember { get; set; }
        public virtual ICollection<CommentHistory> CommentHistory { get; set; }
        public virtual ICollection<Comment> CommentLastModifiedByMember { get; set; }
        public virtual ICollection<Comment> CommentMember { get; set; }
        public virtual ICollection<CommentVote> CommentVoteCreatedByMember { get; set; }
        public virtual ICollection<CommentVoteHistory> CommentVoteHistory { get; set; }
        public virtual ICollection<CommentVote> CommentVoteLastModifiedByMember { get; set; }
        public virtual ICollection<CommentVote> CommentVoteMember { get; set; }
        public virtual ICollection<Member> InverseCreatedByMember { get; set; }
        public virtual ICollection<Member> InverseLastModifiedByMember { get; set; }
        public virtual ICollection<MemberHistory> MemberHistory { get; set; }
        public virtual ICollection<MemberPrivilege> MemberPrivilegeCreatedByMember { get; set; }
        public virtual ICollection<MemberPrivilegeHistory> MemberPrivilegeHistory { get; set; }
        public virtual ICollection<MemberPrivilege> MemberPrivilegeLastModifiedByMember { get; set; }
        public virtual ICollection<MemberPrivilege> MemberPrivilegeMember { get; set; }
        public virtual ICollection<MemberSocialMediaType> MemberSocialMediaTypeCreatedByMember { get; set; }
        public virtual ICollection<MemberSocialMediaTypeHistory> MemberSocialMediaTypeHistory { get; set; }
        public virtual ICollection<MemberSocialMediaType> MemberSocialMediaTypeLastModifiedByMember { get; set; }
        public virtual ICollection<MemberSocialMediaType> MemberSocialMediaTypeMember { get; set; }
        public virtual ICollection<Post> PostCreatedByMember { get; set; }
        public virtual ICollection<PostDuplicatePost> PostDuplicatePostCreatedByMember { get; set; }
        public virtual ICollection<PostDuplicatePostHistory> PostDuplicatePostHistory { get; set; }
        public virtual ICollection<PostDuplicatePost> PostDuplicatePostLastModifiedByMember { get; set; }
        public virtual ICollection<PostHistory> PostHistory { get; set; }
        public virtual ICollection<Post> PostLastModifiedByMember { get; set; }
        public virtual ICollection<Post> PostMember { get; set; }
        public virtual ICollection<PostStatus> PostStatusCreatedByMember { get; set; }
        public virtual ICollection<PostStatusHistory> PostStatusHistory { get; set; }
        public virtual ICollection<PostStatus> PostStatusLastModifiedByMember { get; set; }
        public virtual ICollection<PostStatusType> PostStatusTypeCreatedByMember { get; set; }
        public virtual ICollection<PostStatusTypeHistory> PostStatusTypeHistory { get; set; }
        public virtual ICollection<PostStatusType> PostStatusTypeLastModifiedByMember { get; set; }
        public virtual ICollection<PostTag> PostTagCreatedByMember { get; set; }
        public virtual ICollection<PostTagHistory> PostTagHistory { get; set; }
        public virtual ICollection<PostTag> PostTagLastModifiedByMember { get; set; }
        public virtual ICollection<PostType> PostTypeCreatedByMember { get; set; }
        public virtual ICollection<PostTypeHistory> PostTypeHistory { get; set; }
        public virtual ICollection<PostType> PostTypeLastModifiedByMember { get; set; }
        public virtual ICollection<PostVote> PostVoteCreatedByMember { get; set; }
        public virtual ICollection<PostVoteHistory> PostVoteHistory { get; set; }
        public virtual ICollection<PostVote> PostVoteLastModifiedByMember { get; set; }
        public virtual ICollection<PostVote> PostVoteMember { get; set; }
        public virtual ICollection<Privilege> PrivilegeCreatedByMember { get; set; }
        public virtual ICollection<PrivilegeHistory> PrivilegeHistory { get; set; }
        public virtual ICollection<Privilege> PrivilegeLastModifiedByMember { get; set; }
        public virtual ICollection<Setting> SettingCreatedByMember { get; set; }
        public virtual ICollection<SettingHistory> SettingHistory { get; set; }
        public virtual ICollection<Setting> SettingLastModifiedByMember { get; set; }
        public virtual ICollection<SocialMediaType> SocialMediaTypeCreatedByMember { get; set; }
        public virtual ICollection<SocialMediaTypeHistory> SocialMediaTypeHistory { get; set; }
        public virtual ICollection<SocialMediaType> SocialMediaTypeLastModifiedByMember { get; set; }
        public virtual ICollection<Tag> TagCreatedByMember { get; set; }
        public virtual ICollection<TagHistory> TagHistory { get; set; }
        public virtual ICollection<Tag> TagLastModifiedByMember { get; set; }
        public virtual ICollection<TrustLevel> TrustLevelCreatedByMember { get; set; }
        public virtual ICollection<TrustLevelHistory> TrustLevelHistory { get; set; }
        public virtual ICollection<TrustLevel> TrustLevelLastModifiedByMember { get; set; }
        public virtual ICollection<VoteType> VoteTypeCreatedByMember { get; set; }
        public virtual ICollection<VoteTypeHistory> VoteTypeHistory { get; set; }
        public virtual ICollection<VoteType> VoteTypeLastModifiedByMember { get; set; }
    }
}
