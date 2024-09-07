using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Service.Abstracts.Common;
using Cms.Service.Abstracts.Content;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Common;
using Cms.Service.ViewModel.Content;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cms.Website.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        public ISlideService _slideService;
        public IUploadService _uploadService;
        public SlideController(ISlideService slideService, IUploadService uploadService)
        {
            _slideService = slideService;
            _uploadService = uploadService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SlideEdit(int id)
        {
            ViewBag.SlideId = 0;
            if (id > 0)
            {
                var model = _slideService.GetById(id);
                ViewBag.SlideId = id;
                return View(model);
            }
            return View();


        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _slideService.GetById(id);
            return new OkObjectResult(model);
        }
        [HttpPost]
        public IActionResult Copy(CopyViewModel viewModel)
        {
            _slideService.Copy(viewModel);
            return new OkObjectResult(true);
        }

        [HttpPost]
        public IActionResult GetAllPaging(FilterCommonViewModel viewModel)
        {
            var model = _slideService.Filter(viewModel);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(SlideViewModel slideVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            if (slideVm.Id == 0)
            {
                _slideService.Add(slideVm);
            }
            else
            {
                _slideService.UpdateV2(slideVm, slideVm.Id);
            }
            _slideService.Save();
            return new OkObjectResult(slideVm);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            var slide = _slideService.GetById(id);
            _uploadService.DeleteFile(slide.Image);
            _slideService.Delete(id);
            _slideService.Save();
            return new OkObjectResult(id);
        }
    }
}