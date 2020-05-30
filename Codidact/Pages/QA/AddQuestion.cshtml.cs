using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Codidact;

namespace Codidact.Pages.QA
{
    public class AddQuestionModel : PageModel
    {
        private readonly CodidactContext _context;

        public AddQuestionModel(CodidactContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Posts Posts { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Posts.CreationDate = DateTime.Now;
            Posts.LastActivityDate = DateTime.Now;
            Posts.OwnerUserId = Services.ClaimsHelper.GetLoggedOnUserId(HttpContext.User);
            Posts.Type = Posts.PostType.Question;

            _context.Posts.Add(Posts);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
