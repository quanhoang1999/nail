using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Service.Abstracts.Content;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Content;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cms.Website.Areas.Admin.Controllers
{
    public class PageController : BaseController
    {
        public IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            var model = _pageService.GetAll();

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _pageService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult GetAllPaging(FilterCommonViewModel viewModel)
        {
            var model = _pageService.Filter(viewModel);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(PageViewModel pageVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            if (pageVm.Id == 0)
            {
                _pageService.Add(pageVm);
            }
            else
            {
                _pageService.UpdateV2(pageVm,pageVm.Id);
            }
            _pageService.Save();
            return new OkObjectResult(pageVm);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            _pageService.Delete(id);
            _pageService.Save();

            return new OkObjectResult(id);
        }
    }
}