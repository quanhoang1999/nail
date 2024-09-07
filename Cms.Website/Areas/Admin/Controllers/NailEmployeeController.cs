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
    public class NailEmployeeController : BaseController
    {
        private readonly INailEmployeeService _nailEmployeeService;
        private readonly INailStoreService _nailStoreService;
        private readonly UserManager<AppUser> _userManager;

        public NailEmployeeController(INailEmployeeService nailEmployeeService,
            INailStoreService nailStoreService,
            UserManager<AppUser> userManager)
        {
            _nailEmployeeService = nailEmployeeService;
            _nailStoreService = nailStoreService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            ViewBag.NailStore = _nailStoreService.GetAll().Where(x => !x.IsDeleted);
            return View();
        }

        public IActionResult Edit(Guid id)
        {
            ViewBag.NailStore = _nailStoreService.GetAll().Where(x => !x.IsDeleted);
            if (id != Guid.Empty)
            {
                var model = _nailEmployeeService.GetById(id);
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Filter(FilterCommonViewModel viewModel)
        {
            var filterModel = _nailEmployeeService.Filter(viewModel);
            return new OkObjectResult(filterModel);
        }

        [HttpPost]
        public IActionResult SaveEntity(NailEmployeeViewModel viewModel)
        {

            if (viewModel.Id != Guid.Empty)
                _nailEmployeeService.CreateUpdate(viewModel);
            else
                _nailEmployeeService.Add(viewModel);
            _nailEmployeeService.Save();
            return new OkObjectResult(viewModel);

        }


        [HttpGet]
        public IActionResult GetById(Guid id)
        {
            var model = _nailEmployeeService.GetById(id);
            return new OkObjectResult(model);
        }
        [HttpPost]
        public IActionResult IsActive(NailEmployeeViewModel viewModel)
        {
            var result = _nailEmployeeService.UpdateStatus(viewModel);
            return new OkObjectResult(result);
        }
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                _nailEmployeeService.Delete(id);

                return new OkObjectResult(id);
            }
        }
    }
}