using AutoMapper.Configuration;
using Cms.Infrastructure.Dtos;
using Cms.Service.Abstracts.Content;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Content;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Website.Controllers.Components
{
    public class GalleryViewComponent : ViewComponent
    {
        private readonly IGalleryService _galleryService;
 

        public GalleryViewComponent( IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string page = "index")
        {
            FilterCommonViewModel filter = new FilterCommonViewModel();
            filter.PageSize = 3;
            filter.PageIndex = 1;
            var data = _galleryService.GalleryGetType(filter);
            return View("Default", data);
        }

    }
}
