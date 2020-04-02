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
    public class NewAnswerVM
    {
        public string Body { get; set; }
        public int QuestionID { get; set; }
    }


    public class DetailsModel : PageModel
    {
        private readonly Codidact.CodidactContext _context;

        public DetailsModel(Codidact.CodidactContext context)
        {
            _context = context;
        }

        public Posts Question { get; set; }
        public List<Posts> Answers { get; set; }
        public NewAnswerVM NewAnswer { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Posts.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            NewAnswer = new NewAnswerVM { QuestionID = Question.Id, Body = "" };

            if (Question == null)
            {
                return NotFound();
            }
            else
            {
                Answers = await _context.Posts.AsNoTracking().Include(i => i.OwnerUser).Where(m => m.ParentId == id).ToListAsync();
            }
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(NewAnswerVM NewAnswer)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var answer = new Posts();
            answer.Body = NewAnswer.Body;
            answer.ParentId = NewAnswer.QuestionID;
            answer.Type = Posts.PostType.Answer;
            answer.CreationDate = DateTime.Now;
            answer.LastActivityDate = DateTime.Now;
            answer.OwnerUserId = Services.ClaimsHelper.GetLoggedOnUserId(HttpContext.User);

            _context.Posts.Add(answer);
            await _context.SaveChangesAsync();

            return await OnGetAsync(NewAnswer.QuestionID);
        }
    }
}
