using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Data.Entities.Identity;
using Cms.Service.Abstracts.Nails;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Nails;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Website.Areas.Admin.Controllers
{
    public class NailPromotionController : BaseController
    {
        private readonly INailPromotionService _nailPromotionService;
        private readonly INailStoreService _nailStoreService;
        private readonly UserManager<AppUser> _userManager;

        public NailPromotionController(INailPromotionService nailPromotionService,
            INailStoreService nailStoreService,
            UserManager<AppUser> userManager)
        {
            _nailPromotionService = nailPromotionService;
            _nailStoreService = nailStoreService;
            _userManager = userManager;
        }
        public IActionResult Edit(int id)
        {
            ViewBag.NailStore = _nailStoreService.GetAll().Where(x => !x.IsDeleted);
            ViewBag.NailStores = _nailStoreService.GetStores();
            if (id > 0)
            {
                var model = _nailPromotionService.GetById(id);
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Filter(FilterCommonViewModel viewModel)
        {
            var filterModel = _nailPromotionService.Filter(viewModel);
            return new OkObjectResult(filterModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                _nailPromotionService.Delete(id);
                _nailPromotionService.Save();
                return new OkObjectResult(id);
            }
        }

        [HttpPost]
        public IActionResult SaveEntity(NailPromotionViewModel viewModel)
        {

            if (viewModel.Id > 0)
            {
                _nailPromotionService.UpdatePromotion(viewModel);

            }
            else
            {
                _nailPromotionService.Add(viewModel);
            }
            _nailPromotionService.Save();
            return new OkObjectResult(viewModel);

        }
        public IActionResult Index()
        {
            ViewBag.NailStore = _nailStoreService.GetAll().Where(x => !x.IsDeleted);
            return View();
        }
    }
}