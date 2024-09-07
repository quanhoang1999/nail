using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Data.Entities.Identity;
using Cms.Infrastructure.Dtos;
using Cms.RazorPages.Extensions;
using Cms.RazorPages.Model;
using Cms.Service.Systems.Users;
using Cms.Service.Systems.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.RazorPages.Pages.Admin
{
    [Authorize]
    public class UserModel : PageModel
    {
        private readonly IUserService _userService;

        private readonly IAuthorizationService _authorizationService;

        public UserModel(IUserService userService, IAuthorizationService authorizationService)
        {
            _userService = userService;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public RegisterUserViewModel RegisterUser { get; set; }

        public PagedResult<AppUserViewModel> AppUsers { get; private set; }

        public PaginatedList<AppUserViewModel> AppUsersModel { get; set; }

        //public void OnGet()
        //{
        //    // AppUsers = _userService.GetAllPagingAsync
        //}
        public async Task OnGetAsync(string keyWord, int? pageIndex)
        {
            int pageSize = 3;

            AppUsersModel = await PaginatedList<AppUserViewModel>.CreateAsync(
        _userService.GetAll(), pageIndex ?? 1, pageSize);
        }

        public async Task<IActionResult> OnGetUserById(Guid id)
        {
            var userModel = await _userService.GetById(id);
            RegisterUser = new RegisterUserViewModel
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                Name = userModel.FullName,
                PhoneNumber = userModel.PhoneNumber,
                BirthDay = userModel.BirthDay
            };
            return new JsonResult(userModel);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ICollection<string> col = new List<string>() { "Admin" };
                var userInfo = await _userService.GetById(RegisterUser.Id);
                bool result = false;
                var user = new AppUserViewModel
                {
                    Id = RegisterUser.Id,
                    UserName = RegisterUser.UserName,
                    Email = RegisterUser.Email,
                    FullName = RegisterUser.Name,
                    BirthDay = RegisterUser.BirthDay,
                    Password = RegisterUser.Password,
                    PhoneNumber = RegisterUser.PhoneNumber,
                    Roles = col
                };
                if (userInfo != null)
                {
                    await _userService.UpdateAsync(user);
                    return RedirectToPage("User");
                }
                else
                {

                    result = await _userService.AddAsync(user);
                }

                if (result)
                {
                    //_logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { userId = user.Id, code = code },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //   await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToPage("User");
                }

                //foreach (var error in result.Errors)
                //{
                //    ModelState.AddModelError(string.Empty, error.Description);
                //}
            }
            return Page();
        }

    }
}