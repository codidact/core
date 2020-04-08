using System;
using System.Collections.Generic;
using Codidact.Core.Domain.Entities;

namespace Codidact.Core.Application.Questions.Queries.QuestionsQuery
{
    public class QuestionsQueryResult
    {
        public IEnumerable<QuestionsQueryDto> Items { get; set; } = new List<QuestionsQueryDto>();

        public int Total { get; set; }

    }
}
