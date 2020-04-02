using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codidact
{
    public partial class Posts
    {
        public int Id { get; set; }

        public int? AcceptedAnswerId { get; set; }
        public int? AnswerCount { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTime? ClosedDate { get; set; }
        public int? CommentCount { get; set; }
        public DateTime? CommunityOwnedDate { get; set; }
        public DateTime CreationDate { get; set; }
        public int? FavoriteCount { get; set; }
        public DateTime LastActivityDate { get; set; }
        public DateTime? LastEditDate { get; set; }
        [MaxLength(40)]
        public string LastEditorDisplayName { get; set; }
        public int? LastEditorUserId { get; set; }

        public int? OwnerUserId { get; set; }
        [ForeignKey("OwnerUserId")]
        public virtual Users OwnerUser { get; set; }

        public int? ParentId { get; set; }
        public enum PostType { Question = 1, Answer };
        public PostType Type { get; set; }

        public int Score { get; set; }
        [MaxLength(150)]
        public string Tags { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        public int ViewCount { get; set; }
    }
}
