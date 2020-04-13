using Codidact.Core.Application.Common.Contracts;
using Codidact.Core.Application.Questions.Queries.QuestionsQuery;

namespace Codidact.Core.Application.Questions.Queries
{
    public class QuestionsQueryRequest : IRequest<QuestionsQueryResult>
    {
        /// <summary>
        /// The keyword to search by
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// How many pages to skip to get the result
        /// </summary>
        public int Skip { get; set; } = 0;

        /// <summary>
        /// How many questions to take per page
        /// </summary>
        public int Take { get; set; } = 20;

        /// <summary>
        /// The sort that is applied on the query 
        /// </summary>
        public QuestionsQuerySortType Sort { get; set; } = QuestionsQuerySortType.Newest;

        /// <summary>
        /// The category of the questions
        /// </summary>
        public string Category { get; set; }
    }

    public enum QuestionsQuerySortType
    {
        Newest = 1,
        Best = 2,
        Oldest = 3
    }
}
