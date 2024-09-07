using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Data.Entities.Identity;
using Cms.Service.Abstracts.Nails;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Nails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Website.Areas.Admin.Controllers
{
    public class NailCustomerController : BaseController
    {
        private readonly INailCustomerService _nailCustomerService;
        private readonly INailStoreService _nailStoreService;

        private readonly UserManager<AppUser> _userManager;

        public NailCustomerController(INailCustomerService nailCustomerService,
            INailStoreService nailStoreService,
            UserManager<AppUser> userManager)
        {
            _nailCustomerService = nailCustomerService;
            _nailStoreService = nailStoreService;
            _userManager = userManager;
        }
        public IActionResult Edit(int id)
        {
            ViewBag.NailStore = _nailStoreService.GetAll().Where(x => !x.IsDeleted);
            if (id > 0)
            {
                var model = _nailCustomerService.GetById(id);
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Filter(FilterCommonViewModel viewModel)
        {
            var filterModel = _nailCustomerService.Filter(viewModel);
            return new OkObjectResult(filterModel);
        }

        [HttpPost]
        public IActionResult SaveEntity(NailCustomerViewModel viewModel)
        {

            if (viewModel.Id > 0)
            {
                _nailCustomerService.UpdateNailCustomer(viewModel);

            }
            else
            {
                _nailCustomerService.Add(viewModel);
            }
            _nailCustomerService.Save();
            return new OkObjectResult(viewModel);

        }


        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _nailCustomerService.GetById(id);
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
                _nailCustomerService.Delete(id);

                return new OkObjectResult(id);
            }
        }
        public IActionResult Index()
        {
            ViewBag.NailStore = _nailStoreService.GetAll().Where(x => !x.IsDeleted);
            return View();
        }
    }
}