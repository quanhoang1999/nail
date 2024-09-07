using Cms.Data.Entities.Media;
using Cms.Service.ViewModel.Media;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Abstracts.Media
{
    public interface IPictureService : IWebServiceBase<Picture, int, PictureViewModel>
    {
        Picture InsertPicture(IFormFile formFile, string defaultFileName = "");
    }
}
