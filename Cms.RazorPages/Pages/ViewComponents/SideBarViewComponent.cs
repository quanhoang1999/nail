using Cms.Data.Entities.Identity;
using Cms.Utilities.Contants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cms.RazorPages.Pages.ViewComponents
{
    public class SideBarViewComponent : ViewComponent
    {
     
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public SideBarViewComponent(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
           
            _userManager = userManager;
            _roleManager = roleManager;
        }

       
    }
}
