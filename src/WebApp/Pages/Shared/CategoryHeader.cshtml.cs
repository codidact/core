using System.Collections.Generic;
using System.Threading.Tasks;
using Codidact.Core.Application.Categories.Queries.ShortCategoriesListQuery;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Codidact.Core.WebApp.Pages.Shared
{
    public class CategoryHeaderModel : PageModel
    {
        private readonly ShortCategoriesListQuery _categoriesQuery;
        private readonly ILogger<CategoryHeaderModel> _logger;

        public CategoryHeaderModel(ShortCategoriesListQuery categoriesQuery, ILogger<CategoryHeaderModel> logger)
        {
            _categoriesQuery = categoriesQuery;
            _logger = logger;
        }

        public IEnumerable<ShortCategoryResult> Categories { get; set; }

        public async Task OnGetAsync(ShortCategoriesListRequest request)
        {
            Categories = await _categoriesQuery.Handle(new ShortCategoriesListRequest());
        }
    }
}
