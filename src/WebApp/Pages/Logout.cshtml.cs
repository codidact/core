using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Codidact.Core.WebApp.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public LogoutModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnPost()
        {
            return SignOut("cookie", "oidc");
        }
    }
}
