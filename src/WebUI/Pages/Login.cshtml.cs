using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Codidact.WebUI.Pages
{
    [Authorize]
    public class LoginModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public LoginModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Redirect("/Index");
        }
    }
}