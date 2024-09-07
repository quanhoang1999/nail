using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Data.Entities.Identity;
using Cms.Service.Abstracts.Nails;
using Cms.Service.Implement.Nails;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Nails;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Website.Areas.Admin.Controllers
{
    public class NailServiceController : BaseController
    {
        private readonly INailServiceService _nailService;
        private readonly INailCategoryService _nailCategoryService;
        private readonly INailEmployeeService _nailEmployeeService;
        private readonly INailStoreService _nailStoreService;
        private readonly UserManager<AppUser> _userManager;

        public NailServiceController(INailServiceService nailService,
            INailCategoryService nailCategoryService,
            INailEmployeeService nailEmployeeService,
            INailStoreService nailStoreService,
            UserManager<AppUser> userManager)
        {
            _nailService = nailService;
            _nailCategoryService = nailCategoryService;
            _nailEmployeeService = nailEmployeeService;
            _nailStoreService = nailStoreService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            ViewBag.NailCategory = _nailCategoryService.GetAll().Where(x => !x.IsDeleted);
            ViewBag.NailStore = _nailStoreService.GetAll().Where(x => !x.IsDeleted);
            return View();
        }
        public IActionResult Edit(int id)
        {
           
            ViewBag.Employee = _nailEmployeeService.GetAll();
            ViewBag.NailStore = _nailStoreService.GetAll().Where(x => !x.IsDeleted);
            ViewBag.NailCategory = _nailCategoryService.GetAll().Where(x => !x.IsDeleted);

            if (id > 0)
            {
                string[] includes = new string[] { "NailEmployeeServices" };
                var model = _nailService.GetByIdInclude(id, includes);
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Filter(FilterCommonViewModel viewModel)
        {

            var filterModel = _nailService.Filter(viewModel);
            return new OkObjectResult(filterModel);
        }

        [HttpPost]
        public IActionResult SaveEntity(NailServiceViewModel viewModel)
        {

            if (viewModel.Id > 0)
            {
                _nailService.UpdateService(viewModel);

            }
            else
            {
                _nailService.InsertService(viewModel);
            }
            _nailService.Save();
            return new OkObjectResult(viewModel);
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
                _nailService.IsDelete(id);
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
                _nailService.DeleteFile(id, images);
                return new OkObjectResult(id);
            }
        }

    }
}