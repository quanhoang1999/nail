using Cms.Data.Entities.Content;
using Cms.Infrastructure.Dtos;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Content;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Abstracts.Content
{
    public interface IGalleryService : IWebServiceBase<Gallery, int, GalleryViewModel>
    {
        PagedResult<GalleryViewModel> Filter(FilterCommonViewModel viewModel);
        PagedResult<GalleryViewModel> GalleryGetType(FilterCommonViewModel viewModel);
        List<GalleryViewModel> GetGalleryByNailStoreId(int nailStoreId);
        bool UpdateGallery(GalleryViewModel viewModel);       
    }
}
