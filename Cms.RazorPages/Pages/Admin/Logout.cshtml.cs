using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Cms.RazorPages.Pages.Admin
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger _logger;

        public LogoutModel(SignInManager<AppUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }
        public async Task<IActionResult> OnPost()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToPage("/Admin/Dashboard");
        }
        public void OnGet()
        {
             _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            RedirectToPage("/Admin/Dashboard");
        }
    }
}