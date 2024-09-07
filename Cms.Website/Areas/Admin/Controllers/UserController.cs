using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cms.Data.Entities.Identity;
using Cms.Infrastructure.Dtos;
using Cms.Service.Abstracts.Identiy;
using Cms.Service.ViewModel.Identity;
using Cms.Service.ViewModel.Zaloka;
using Cms.Website.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Cms.Website.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        public UserController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILoggerFactory loggerFactory, IConfiguration config, IMapper mapper, IUserService userService, IAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _userService = userService;
            _signInManager = signInManager;
            _authorizationService = authorizationService;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _config = config;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            //var result = await _authorizationService.AuthorizeAsync(User, "USER", Operations.Read);
            //if (result.Succeeded == false)
            //    return new RedirectResult("/Admin/Login/Index");

            return View();
        }
        [HttpGet]      
      
        public IActionResult GetAll()
        {
            var model = _userManager.Users.ToList();
          
            var appUserModel = _mapper.Map<List<AppUserViewModel>>(model);
            return new OkObjectResult(new GenericResult(true, appUserModel));
        }

        [HttpPost]
    
        public async Task<IActionResult> Filter(FilterUserViewModel viewModel)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var filterModel = _userService.FilterUser(viewModel, user);
            return new OkObjectResult(filterModel);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var model = await _userService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEntity(RegisterViewModel userVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (userVm.Id == null)
                {
                    await _userService.CreateAsync(userVm);
                }
                else
                {
                    await _userService.UpdateAsync(userVm);
                }
                return new OkObjectResult(userVm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                await _userService.DeleteAsync(id);

                return new OkObjectResult(id);
            }
        }
    }
}