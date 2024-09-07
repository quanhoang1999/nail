using Cms.Data.Entities.Identity;
using Cms.Service.Abstracts.Identiy;
using Cms.Service.ViewModel.Identity;
using Cms.Utilities.Contants;
using Cms.Website.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cms.Website.Areas.Admin.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private IFunctionService _functionService;
        private readonly IUIElementService _uIElementService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public SideBarViewComponent(IFunctionService functionService, IUIElementService uIElementService, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _functionService = functionService;
            _uIElementService = uIElementService;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roles = ((ClaimsPrincipal)User).GetSpecificClaim("Roles");
            List<FunctionViewModel> functions;
            if (roles.Split(";").Contains(CommonConstants.AppRole.AdminRole))
            {
                functions = _functionService.GetAll().ToList();
               // functions = _functionService.GetAll().Where(x => x.Status == Infrastructure.Enums.Status.Active).ToList();
            }
            else
            {
                //TODO: Get by permission
                functions = await _functionService.GetAllAsync();
            }
            return View(functions);
        }
    }
}
