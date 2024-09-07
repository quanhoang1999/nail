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
    public class NailStoreController : BaseController
    {
        private readonly INailStoreService _nailStoreService;

        private readonly UserManager<AppUser> _userManager;

        public NailStoreController(INailStoreService nailStoreService,
            UserManager<AppUser> userManager)
        {
            _nailStoreService = nailStoreService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {

            if (id > 0)
            {
                var model = _nailStoreService.GetById(id);
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Filter(FilterCommonViewModel viewModel)
        {
            var filterModel = _nailStoreService.Filter(viewModel);
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
                _nailStoreService.Delete(id);
                _nailStoreService.Save();
                return new OkObjectResult(id);
            }
        }
        [HttpPost]
        public IActionResult SaveEntity(NailStoreViewModel viewModel)
        {

            if (viewModel.Id > 0)
            {
                _nailStoreService.UpdateStore(viewModel);

            }
            else
            {
                _nailStoreService.Add(viewModel);
            }
            _nailStoreService.Save();
            return new OkObjectResult(viewModel);

        }
    }
}