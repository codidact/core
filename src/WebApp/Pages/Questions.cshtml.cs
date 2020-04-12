using System;
using System.Threading.Tasks;
using Codidact.Core.Application.Questions.Queries;
using Codidact.Core.Application.Questions.Queries.QuestionsQuery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Codidact.Core.WebApp.Pages.Posts
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

        public async Task<IActionResult> OnGetAsync(QuestionsQueryRequest request, string category)
        {
            _logger.LogInformation($"{DateTime.UtcNow.ToString("g")} - Received request for Questions List");
            if (ModelState.IsValid)
            {
                Result = await _questionsQuery.Handle(request)
                      .ConfigureAwait(false);
            }

            return Page();
        }
    }
}
