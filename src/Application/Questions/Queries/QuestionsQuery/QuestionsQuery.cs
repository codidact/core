using System;
using System.Linq;
using System.Threading.Tasks;
using Codidact.Core.Application.Common.Contracts;
using Codidact.Core.Application.Common.Interfaces;
using Codidact.Core.Domain.Entities;

namespace Codidact.Core.Application.Questions.Queries.QuestionsQuery
{
    public class QuestionsQuery : IRequestHandler<QuestionsQueryRequest, QuestionsQueryResult>
    {
        private readonly IApplicationDbContext _context;

        public QuestionsQuery(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<QuestionsQueryResult> Handle(QuestionsQueryRequest request)
        {
            var questionsQuery = _context.Posts
                .Where(post => post.PostTypeId == Domain.Enums.PostType.Question)
                .Where(post => post.IsDeleted == false);

            AddSortToQuery(request, questionsQuery);

            AddFiltersToQuery(request, questionsQuery);

            return Task.FromResult(new QuestionsQueryResult
            {
                Items = questionsQuery
                            .Take(request.Take)
                            .Skip(request.Skip)
                            .Select(MapToQuestion),
                Total = questionsQuery.Count()
            });
        }

        private QuestionsQueryDto MapToQuestion(Post post)
        {
            return new QuestionsQueryDto
            {
                Answers = post.InverseParentPost.Count,
                CreatedAt = post.CreatedAt,
                LastModifiedAt = post.LastModifiedAt,
                Score = post.Upvotes - (post.Downvotes),
                Title = post.Title,
                Tags = post.PostTag.Select(posttag =>
                    new QuestionTag { Id = posttag.Tag.Id, Name = posttag.Tag.Body })
            };
        }

        private void AddFiltersToQuery(QuestionsQueryRequest request, IQueryable<Post> questionsQuery)
        {
            // TODO: Implement filters
        }

        private void AddSortToQuery(QuestionsQueryRequest request, IQueryable<Post> questionsQuery)
        {
            switch (request.Sort)
            {
                case QuestionsQuerySortType.Date:
                default:
                    questionsQuery
                        .OrderByDescending(question => question.LastModifiedAt)
                        .ThenByDescending(question => question.CreatedAt);
                    break;
                case QuestionsQuerySortType.Popularity:
                    // TODO: implement popularity
                    break;
            }
        }
    }
}
