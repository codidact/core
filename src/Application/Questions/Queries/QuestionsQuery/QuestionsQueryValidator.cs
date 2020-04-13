using FluentValidation;

namespace Codidact.Core.Application.Questions.Queries.QuestionsQuery
{
    public class QuestionsQueryValidator : AbstractValidator<QuestionsQueryRequest>
    {
        public QuestionsQueryValidator()
        {
            RuleFor(request => request.Take)
                .GreaterThan(0);

            RuleFor(request => request.Skip)
                .GreaterThanOrEqualTo(0);

            RuleFor(request => request.Category)
                       .NotNull()
                       .NotEmpty();
        }
    }
}
