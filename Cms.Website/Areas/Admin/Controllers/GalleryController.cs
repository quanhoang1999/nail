using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Infrastructure.Dtos;
using Cms.Service.Abstracts.Common;
using Cms.Service.Abstracts.Content;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Common;
using Cms.Service.ViewModel.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static Cms.Utilities.Contants.CommonProperties;

namespace Cms.Website.Areas.Admin.Controllers
{
    public class GalleryController : BaseController
    {
        public IGalleryService _galleryService;
        public IUploadService _uploadService;
        public GalleryController(IGalleryService galleryService, IUploadService uploadService)
        {
            _galleryService = galleryService;
            _uploadService = uploadService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            List<ValueName> socialList = new List<ValueName>();
            socialList.Add(new ValueName()
            {
                Id = 0,
                Name = "--Select--"
            });
             socialList.Add(new ValueName()
            {
                Id = 1,
                Name = "Youtube"
            });
            socialList.Add(new ValueName()
            {
                Id = 2,
                Name = "File"
            });
            ViewBag.ListFileType = socialList;
            ViewBag.GalleryId = 0;
            if (id > 0)
            {
                var model = _galleryService.GetById(id);
                ViewBag.GalleryId = id;
                return View(model);
            }
            return View();


        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _galleryService.GetById(id);
            return new OkObjectResult(model);
        }
        //[HttpPost]
        //public IActionResult Copy(CopyViewModel viewModel)
        //{
        //    _galleryService.Copy(viewModel);
        //    return new OkObjectResult(true);
        //}

        [HttpPost]
        public IActionResult GetAllPaging(FilterCommonViewModel viewModel)
        {
            var model = _galleryService.Filter(viewModel);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(GalleryViewModel slideVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            if (slideVm.Id == 0)
            {
                _galleryService.Add(slideVm);
            }
            else
            {
                _galleryService.UpdateV2(slideVm, slideVm.Id);
            }
            _galleryService.Save();
            return new OkObjectResult(slideVm);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            var slide = _galleryService.GetById(id);
            _uploadService.DeleteFile(slide.FileUrl);
            _galleryService.Delete(id);
            _galleryService.Save();
            return new OkObjectResult(id);
        }
    }
}