using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Codidact;

namespace Codidact.Pages.QA
{
    public class IndexModel : PageModel
    {
        private readonly Codidact.CodidactContext _context;

        public IndexModel(Codidact.CodidactContext context)
        {
            _context = context;
        }

        public IList<Posts> Posts { get;set; }

        public async Task OnGetAsync()
        {
            Posts = await _context.Posts.AsNoTracking().Where(m => m.Type == Codidact.Posts.PostType.Question).ToListAsync();
        }
    }
}
