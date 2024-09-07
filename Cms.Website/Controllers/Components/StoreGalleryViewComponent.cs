using AutoMapper.Configuration;
using Cms.Infrastructure.Dtos;
using Cms.Service.Abstracts.Content;
using Cms.Service.Abstracts.Nails;
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
    public class StoreGalleryViewComponent : ViewComponent
    {
        private readonly IGalleryService _galleryService;

        private readonly INailStoreService _nailStoreService;

        public StoreGalleryViewComponent(IGalleryService galleryService, INailStoreService nailStoreService)
        {
            _galleryService = galleryService;
            _nailStoreService = nailStoreService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int nailStoreId, bool isGetDescription = false)
        {
            if (isGetDescription)
            {
                string description = _nailStoreService.GetDescription(nailStoreId);
                return View("Description", description);
            }
            else
            {
                var data = _galleryService.GetGalleryByNailStoreId(nailStoreId);
                return View("Default", data);
            }
          
        }

    }
}
