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
    public class NailCategoryController : BaseController
    {
        private readonly INailCategoryService _nailCategoryService;
        private readonly INailStoreService _nailStoreService;
        private readonly UserManager<AppUser> _userManager;

        public NailCategoryController(INailCategoryService nailCategoryService,
            INailStoreService nailStoreService,
            UserManager<AppUser> userManager)
        {
            _nailCategoryService = nailCategoryService;
            _nailStoreService = nailStoreService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            ViewBag.NailStore = _nailStoreService.GetAll();
            return View();
        }
        public IActionResult Edit(int id)
        {
            ViewBag.NailStore = _nailStoreService.GetAll();
            if (id > 0)
            {
                var model = _nailCategoryService.GetById(id);
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Filter(FilterCommonViewModel viewModel)
        {
            var filterModel = _nailCategoryService.Filter(viewModel);
            return new OkObjectResult(filterModel);
        }

        [HttpPost]
        public IActionResult SaveEntity(NailCategoryViewModel viewModel)
        {

            if (viewModel.Id > 0)
            {
                _nailCategoryService.UpdateCategory(viewModel);

            }
            else
            {
                _nailCategoryService.InsertCategory(viewModel);
            }
            //_nailCategoryService.Save();
            return new OkObjectResult(viewModel);
        }


        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _nailCategoryService.GetById(id);
            return new OkObjectResult(model);
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
                _nailCategoryService.IsDelete(id);
                return new OkObjectResult(id);
            }
        }
        [HttpPost]
        public IActionResult DeleteFile(int id, string images)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                _nailCategoryService.DeleteFile(id, images);
                return new OkObjectResult(id);
            }
        }
    }
}