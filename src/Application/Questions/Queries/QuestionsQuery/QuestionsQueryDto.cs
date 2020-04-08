using System;
using System.Collections.Generic;

namespace Codidact.Core.Application.Questions.Queries.QuestionsQuery
{
    public class QuestionsQueryDto
    {
        /// <summary>
        /// Identifier of the question
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Title of the questions
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Quantity of Answers in the question
        /// </summary>
        public long Answers { get; set; }

        /// <summary>
        /// The overall score
        /// </summary>
        public long Score { get; set; }

        /// <summary>
        /// List of tags associated with question
        /// </summary>
        public IEnumerable<QuestionTag> Tags { get; set; } = new List<QuestionTag>();

        /// <summary>
        /// When question was last modified - edit answer, etc
        /// </summary>
        public DateTime? LastModifiedAt { get; set; }

        /// <summary>
        /// When was the question original created
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }

    public class QuestionTag
    {
        /// <summary>
        /// Identifier of tag
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name of tag
        /// </summary>
        public string Name { get; set; }
    }
}
