using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Entities;

namespace Codidact.Core.Application.Questions.Queries.QuestionsQuery
{
    public class QuestionsQueryResult
    {
        public IEnumerable<Post> Items { get; set; } = new List<Post>();

        public int Total { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
    }
}
