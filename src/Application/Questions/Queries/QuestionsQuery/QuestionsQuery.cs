using System;
using System.Linq;
using System.Threading.Tasks;
using Codidact.Core.Application.Common.Contracts;
using Codidact.Core.Application.Common.Interfaces;
using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Codidact.Core.Application.Questions.Queries.QuestionsQuery
{
    /// <summary>
    /// Returns a sets of questions based on the request
    /// </summary>
    public class QuestionsQuery : IRequestHandler<QuestionsQueryRequest, QuestionsQueryResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<QuestionsQuery> _logger;

        public QuestionsQuery(IApplicationDbContext context, ILogger<QuestionsQuery> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<QuestionsQueryResult> Handle(QuestionsQueryRequest request)
        {
            _logger.LogInformation($"{DateTime.UtcNow.ToString("u")} - Starting to handle request for Questions List");

            var category = await _context.Categories
                .FirstOrDefaultAsync(category => category.DisplayName.ToLower() == request.Category.ToLower())
                .ConfigureAwait(false);
            if (category == null)
            {
                throw new Exception($"Category not found: {request.Category}");
            }
            var questionsQuery = _context.Posts
                .Where(post => post.PostTypeId == Domain.Enums.PostType.Question)
                .Where(post => post.IsDeleted == false)
                .Where(post => post.CategoryId == category.Id).AsQueryable();

            questionsQuery = AddSortToQuery(request, questionsQuery);

            questionsQuery = AddFiltersToQuery(request, questionsQuery);

            return new QuestionsQueryResult
            {
                Items = questionsQuery
                            .Take(request.Take)
                            .Skip(request.Skip)
                            .Select(MapToQuestion),
                Total = questionsQuery.Count(),
                Category = category
            };
        }

        private QuestionsQueryDto MapToQuestion(Post post)
        {
            return new QuestionsQueryDto
            {
                Answers = post.InverseParentPost.Count,
                CreatedAt = post.CreatedAt,
                LastModifiedAt = post.LastModifiedAt,
                // Votes are not score
                Votes = post.Upvotes + (post.Downvotes),
                Title = post.Title,
                Tags = post.PostTag.Select(posttag =>
                    new QuestionTag { Id = posttag.Tag.Id, Name = posttag.Tag.Body })
            };
        }

        private IQueryable<Post> AddFiltersToQuery(QuestionsQueryRequest request, IQueryable<Post> questionsQuery)
        {
            // TODO: Implement filters
            return questionsQuery;
        }

        private IQueryable<Post> AddSortToQuery(QuestionsQueryRequest request, IQueryable<Post> questionsQuery)
        {
            switch (request.Sort)
            {
                case QuestionsQuerySortType.Newest:
                default:
                    questionsQuery = questionsQuery
                          .OrderByDescending(question => question.LastModifiedAt)
                          .ThenByDescending(question => question.CreatedAt);
                    break;
                case QuestionsQuerySortType.Best:
                    // TODO: implement popularity
                    break;
                case QuestionsQuerySortType.Oldest:
                    questionsQuery = questionsQuery
                          .OrderBy(question => question.LastModifiedAt)
                          .ThenBy(questions => questions.CreatedAt);
                    break;
            }
            return questionsQuery;
        }
    }
}
