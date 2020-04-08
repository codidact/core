using System.Threading.Tasks;
using Codidact.Core.Application.Questions.Queries;
using Codidact.Core.Application.Questions.Queries.QuestionsQuery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Codidact.Core.WebApp.Pages.Posts
{

    public class QuestionsModel : PageModel
    {

        private readonly QuestionsQuery _questionsQuery;
        public QuestionsModel(QuestionsQuery questionsQuery)
        {
            _questionsQuery = questionsQuery;
        }

        public QuestionsQueryResult Result { get; set; }

        public async Task<IActionResult> OnGetAsync(QuestionsQueryRequest request)
        {
            if (ModelState.IsValid)
            {
                Result = await _questionsQuery.Handle(request)
                      .ConfigureAwait(false);
            }

            return Page();
        }
    }
}
