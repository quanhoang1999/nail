using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Data.Entities.Identity;
using Cms.RazorPages.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Cms.RazorPages.Pages.Admin
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger _logger;
        public LoginModel(
           UserManager<AppUser> userManager,
           SignInManager<AppUser> signInManager,
           ILogger<LoginModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        public void OnGet()
        {

        }
        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }


        public async Task<IActionResult> OnPostAsync(string ReturnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(LoginViewModel.UserName, LoginViewModel.Password, LoginViewModel.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl))
                        return RedirectToPage(ReturnUrl);
                    return RedirectToPage("Dashboard");
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    ModelState.AddModelError(string.Empty, "Tài khoản đã bị khoá.");
                    return Page();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Sai tài khoản hoặc mật khẩu.");
                    return Page();
                }
            }
            return Page();
        }
    }
}