using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Entities;

namespace Codidact.Core.Application.Questions.Queries.QuestionsQuery
{
    public class QuestionsQueryResult
    {
        public IEnumerable<Post> Questions { get; set; } = new List<Post>();

        public int Total { get; set; }
    }
}
