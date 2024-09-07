using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Website.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Website.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            var email = User.GetSpecificClaim("Email");
            return View();
        }
    }
}