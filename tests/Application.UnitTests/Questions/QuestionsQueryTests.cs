using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Codidact.Core.Application.Common.Interfaces;
using Codidact.Core.Application.Questions.Queries;
using Codidact.Core.Application.Questions.Queries.QuestionsQuery;
using Codidact.Core.Domain.Entities;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Codidact.Core.Application.IntegrationTests.Questions
{
    public class QuestionsQueryTests
    {
        private readonly QuestionsQuery _questionsQuery;
        private readonly IApplicationDbContext _applicationDbContext;
        public QuestionsQueryTests()
        {
            _applicationDbContext = ApplicationDbContextFactory.Create();
            _questionsQuery = new QuestionsQuery(_applicationDbContext, NullLogger<QuestionsQuery>.Instance);
        }
        [Fact]
        public async Task QueryShouldReturnResultsByDefaults()
        {
            await SetupQuestions()
                .ConfigureAwait(false);

            var result = await _questionsQuery.Handle(new QuestionsQueryRequest { Category = "main" });

            Assert.NotNull(result.Items);
            Assert.True(result.Items.Any());

            // date is default sort
            // Max date is more closest to now
            var minimumResultDate = result.Items.Max(question => question.CreatedAt);
            var actualMinimumDate = _applicationDbContext.Posts
                .Where(post => post.PostTypeId == Domain.Enums.PostType.Question)
                .Max(post => post.CreatedAt);
            Assert.Equal(minimumResultDate.Ticks, actualMinimumDate.Ticks);

            // take 20 is default
            Assert.Equal(20, result.Items.Count());

            Assert.NotNull(result.Category);
        }

        public async Task SetupQuestions()
        {
            var category = _applicationDbContext.Categories.FirstOrDefault();

            var postsTypeA = Enumerable.Repeat(new Post
            {
                Title = "Test Question A",
                Body = "Body Question",
                PostTypeId = Domain.Enums.PostType.Question,
                CategoryId = category.Id,
                CreatedAt = DateTime.Now.AddDays(-1),
            }, 15).Select((question, i) =>
            {
                question.Title += i;
                return question;
            });
            var postsTypeB = Enumerable.Repeat(
                new Post
                {
                    Title = "Test Question B",
                    Body = "Body Question",
                    PostTypeId = Domain.Enums.PostType.Question,
                    CreatedAt = DateTime.Now,
                    CategoryId = category.Id,
                }, 15).Select((question, i) =>
                {
                    question.Title += i;
                    return question;
                });
            for (int i = 0; i < 15; i++)
            {
                var postA = postsTypeA.ElementAt(i);
                postA.Id = 0;
                var postB = postsTypeB.ElementAt(i);
                postB.Id = 0;
                _applicationDbContext.Posts.Add(postsTypeA.ElementAt(i));
                _applicationDbContext.Posts.Add(postsTypeB.ElementAt(i));
                await _applicationDbContext.SaveChangesAsync(new CancellationTokenSource().Token);
            }
        }
    }
}
