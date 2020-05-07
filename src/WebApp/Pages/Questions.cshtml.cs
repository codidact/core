using System;
using System.Threading.Tasks;
using Codidact.Core.Application.Questions.Queries;
using Codidact.Core.Application.Questions.Queries.QuestionsQuery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Codidact.Core.WebApp.Pages
{

    public class QuestionsModel : PageModel
    {

        private readonly QuestionsQuery _questionsQuery;
        private readonly ILogger<QuestionsModel> _logger;

        public QuestionsModel(QuestionsQuery questionsQuery, ILogger<QuestionsModel> logger)
        {
            _questionsQuery = questionsQuery;
            _logger = logger;
        }

        public QuestionsQueryResult Result { get; set; } = new QuestionsQueryResult();

        public QuestionsQueryRequest Query { get; set; } = new QuestionsQueryRequest();

        public async Task<IActionResult> OnGetAsync(QuestionsQueryRequest query, string category)
        {
            _logger.LogInformation($"{DateTime.UtcNow.ToString("u")} - Received request for Questions List");
            Query = query;
            if (ModelState.IsValid)
            {
                Result = await _questionsQuery.Handle(query)
                      .ConfigureAwait(false);
            }

            return Page();
        }
    }
}
