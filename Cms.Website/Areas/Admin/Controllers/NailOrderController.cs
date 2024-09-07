using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Data.Entities.Identity;
using Cms.Service.Abstracts.Nails;
using Cms.Service.ViewModel;
using Cms.Website.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Website.Areas.Admin.Controllers
{
    public class NailOrderController : BaseController
    {
        private readonly INailOrderService _nailOrderService;
        private readonly INailStoreService _nailStoreService;
        private readonly UserManager<AppUser> _userManager;

        public NailOrderController(INailOrderService nailOrderService,
             INailStoreService nailStoreService,
            UserManager<AppUser> userManager)
        {
            _nailOrderService = nailOrderService;
            _nailStoreService = nailStoreService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewBag.NailStore = _nailStoreService.GetAll().Where(x => !x.IsDeleted);
            return View();
        }
        [HttpPost]
        public IActionResult Filter(FilterCommonViewModel viewModel)
        {
            var filterModel = _nailOrderService.Filter(viewModel);
            return new OkObjectResult(filterModel);
        }
        [HttpPost]
        public IActionResult UpdateStatus(StatusViewModel viewModel)
        {
            _nailOrderService.UpdateStatus(viewModel.Type, viewModel.Ids);
            return new OkObjectResult(true);
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _nailOrderService.GetByOrderId(id);
            return new OkObjectResult(model);
        }
    }
}