using Cms.Data.Entities.System;
using Cms.Service.ViewModel.Content;
using Cms.Service.ViewModel.Media;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Abstracts.Media
{
    public interface IImageService : IWebServiceBase<Image, int, ImageViewModel>
    {
        Image InsertPicture(IFormFile formFile, string defaultFileName = "");
    }
}
