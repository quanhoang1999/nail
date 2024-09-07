using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Cms.Website.Models;
using Cms.Service.Abstracts.Common;
using Microsoft.Extensions.Localization;
using Cms.Infrastructure.Dtos;
using Microsoft.Extensions.Configuration;
using Cms.Service.Abstracts.Nails;
using Cms.Service.Abstracts.Content;
using Cms.Service.ViewModel.Nails;
using Cms.Service.ViewModel.Content;
using Cms.Service.ViewModel;

namespace Cms.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly INailCustomerService _nailCustomerService;
        private readonly INailPromotionService _nailPromotionService;
        private readonly INailStoreService _nailStoreService;
        private readonly IReviewService _reviewService;
        private readonly IPostService _blogService;
        private readonly IGalleryService _galleryService;
        private IEmailSender _emailSender;

        //   private readonly IProductService _productService;
        private readonly IConfiguration _configuration;
        private readonly IStringLocalizer<LanguageSub> _localizer;
        private readonly IViewRenderService _viewRenderService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            INailCustomerService nailCustomerService,
            INailStoreService nailStoreService,
            IGalleryService galleryService,
              IViewRenderService viewRenderService,
            IEmailSender emailSender,
            IConfiguration configuration, IPostService blogService,
            IStringLocalizer<LanguageSub> localizer, INailPromotionService nailPromotionService,
            IReviewService reviewService)
        {
            _nailCustomerService = nailCustomerService;
            _blogService = blogService;
            _galleryService = galleryService;
            _nailStoreService = nailStoreService;
            _configuration = configuration;
            _emailSender = emailSender;
            _viewRenderService = viewRenderService;
            _localizer = localizer;
            _nailPromotionService = nailPromotionService;
            _reviewService = reviewService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = _blogService.GetTop(3);
            return View(model);
        }
        [Route("store/{id}")]
        public IActionResult IndexBranch(int id)
        {
            var model = _blogService.GetTop(3);
            return View(model);
        }

        [Route("privacy", Name = "Privacy")]

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("sign-in", Name = "SignIn")]
        public IActionResult SignIn()
        {
            return View();
        }
        [Route("sign-in", Name = "SignIn")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SignInAsync(NailCustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isResult = await _nailCustomerService.SignInAsync(model);
                if (isResult)
                    ViewData["Success"] = true;
                ModelState.AddModelError(string.Empty, "Phone number already exists");
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("promotion", Name = "Promotion")]

        public IActionResult Promotion()
        {
            var model = _nailPromotionService.GetAll().Where(x => x.IsActive && x.NailStoreId == 1).ToList();
            model.OrderByDescending(x => x.DateModified);
            var obj = model.OrderByDescending(x => x.DateModified).ToList();
            return View(obj);
        }
        [Route("review")]
        public IActionResult Review()
        {
            FilterCommonViewModel filter = new FilterCommonViewModel();
            filter.PageSize = _configuration.GetValue<int>("PageSize");
            filter.PageIndex = 1;
            var data = _reviewService.Filter(filter);
            return View(data);
        }

        [Route("gallery")]
        public IActionResult Gallery()
        {
            FilterCommonViewModel filter = new FilterCommonViewModel();
            filter.PageSize = _configuration.GetValue<int>("PageSize");
            filter.PageIndex = 1;
            var data = _galleryService.Filter(filter);
            return View(data);
        }
    }
}
