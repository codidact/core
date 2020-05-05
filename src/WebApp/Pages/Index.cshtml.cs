using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

using Codidact.Core.Application.Common.Interfaces;
using Entities = Codidact.Core.Domain.Entities;
using Enums = Codidact.Core.Domain.Enums;

namespace Codidact.Core.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, IApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public List<Entities.Post> Questions;
        public void OnGet()
        {
            Questions = _context.Posts.Where(post => post.PostTypeId == Enums.PostType.Question).ToList();
        }
    }
}
