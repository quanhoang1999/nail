using Cms.Service.Abstracts.Nails;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Nails;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Website.Areas.Admin.Controllers
{
    public class NailEGiftController : BaseController
    {

        private readonly INailEgiftService _nailEgiftService;

        public NailEGiftController(INailEgiftService nailEgiftService)
        {
            _nailEgiftService = nailEgiftService;
        }

        public IActionResult Index()
        {
            return View();
        }
        

        public IActionResult Edit(int id)
        {
            ViewBag.NailStore = _nailEgiftService.GetAll().Where(x => !x.IsDeleted);
            if (id>0)
            {
                var model = _nailEgiftService.GetById(id);
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Filter(FilterCommonViewModel viewModel)
        {
            var filterModel = _nailEgiftService.Filter(viewModel);
            return new OkObjectResult(filterModel);
        }

        [HttpPost]
        public IActionResult SaveEntity(NailEgiftViewModel viewModel)
        {

            if (viewModel.Id > 0)
                _nailEgiftService.Update(viewModel);
            else
                _nailEgiftService.Add(viewModel);
            _nailEgiftService.Save();
            return new OkObjectResult(viewModel);

        }
    }
}
