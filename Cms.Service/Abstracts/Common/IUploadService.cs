using Cms.Data.Entities.Media;
using Cms.Service.ViewModel.Media;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Abstracts.Common
{
    public interface IUploadService
    {
        string UploadFile(IFormCollection formCollection, string type);
        string UploadFormFile(IFormFile formFile, string type);
        void DeleteFile(string fileName);
        string CopyFile(string fileName);
        byte[] GetDownloadBits(IFormFile file);
        List<string> UploadMultilImage(IFormFile[] files);
        List<PictureViewModel> UploadMutiple(IFormFile[] files);
    }
}
