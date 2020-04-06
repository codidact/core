using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Common;
using Codidact.Core.Domain.Common.Interfaces;

namespace Codidact.Core.Domain.Entities
{
    public partial class Post : AuditableEntity, ISoftDeletable
    {
        public Post()
        {
            Comment = new HashSet<Comment>();
            InverseParentPost = new HashSet<Post>();
            PostDuplicatePostDuplicatePost = new HashSet<PostDuplicatePost>();
            PostDuplicatePostOriginalPost = new HashSet<PostDuplicatePost>();
            PostStatus = new HashSet<PostStatus>();
            PostTag = new HashSet<PostTag>();
            PostVote = new HashSet<PostVote>();
        }

        public long Id { get; set; }
        public long MemberId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public short Upvotes { get; set; }
        public short Downvotes { get; set; }
        public long? NetVotes { get; set; }
        public decimal Score { get; set; }
        public long Views { get; set; }
        public long PostTypeId { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsClosed { get; set; }
        public bool IsProtected { get; set; }
        public long? ParentPostId { get; set; }
        public long CategoryId { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeletedByMemberId { get; set; }

        public virtual Member CreatedByMember { get; set; }
        public virtual Member LastModifiedByMember { get; set; }
        public virtual Category Category { get; set; }
        public virtual Member Member { get; set; }
        public virtual Post ParentPost { get; set; }
        public virtual PostType PostType { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Post> InverseParentPost { get; set; }
        public virtual ICollection<PostDuplicatePost> PostDuplicatePostDuplicatePost { get; set; }
        public virtual ICollection<PostDuplicatePost> PostDuplicatePostOriginalPost { get; set; }
        public virtual ICollection<PostStatus> PostStatus { get; set; }
        public virtual ICollection<PostTag> PostTag { get; set; }
        public virtual ICollection<PostVote> PostVote { get; set; }
    }
}
