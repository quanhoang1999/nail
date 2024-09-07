using AutoMapper;
using Cms.Data.Entities.Media;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Media;
using Cms.Service.Implement.Common;
using Cms.Service.ViewModel.Media;
using Cms.Utilities.Contants;
using Cms.Utilities.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cms.Service.Implement.Media
{
    public class PictureService : WebServiceBase<Picture, int, PictureViewModel>, IPictureService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private IRepository<Picture, int> _imageRepository;
        private IUnitOfWork _unitOfWork;
        private UploadService _uploadService;
        private readonly IMapper _mapper;
        public PictureService(IRepository<Picture, int> imageRepository, IMapper mapper,
            UploadService uploadService,
            IWebHostEnvironment hostingEnvironment,
            IUnitOfWork unitOfWork) : base(imageRepository, unitOfWork, mapper)
        {
            this._imageRepository = imageRepository;
            _uploadService = uploadService;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            this._unitOfWork = unitOfWork;
        }
        public Picture InsertPicture(IFormFile formFile, string defaultFileName = "")
        {
            var imgExt = new List<string>
            {
                ".bmp",
                ".gif",
                ".jpeg",
                ".jpg",
                ".jpe",
                ".jfif",
                ".pjpeg",
                ".pjp",
                ".png",
                ".tiff",
                ".tif"
            } as IReadOnlyCollection<string>;

            var fileName = formFile.FileName;
            if (string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(defaultFileName))
                fileName = defaultFileName;

            //remove path (passed in IE)
            fileName = Path.GetFileName(fileName);

            var contentType = formFile.ContentType;

            var fileExtension = Path.GetExtension(fileName);
            if (!string.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();

            if (imgExt.All(ext => !ext.Equals(fileExtension, StringComparison.CurrentCultureIgnoreCase)))
                return null;

            //contentType is not always available 
            //that's why we manually update it here
            //http://www.sfsu.edu/training/mimetype.htm
            if (string.IsNullOrEmpty(contentType))
            {
                switch (fileExtension)
                {
                    case ".bmp":
                        contentType = MimeTypes.ImageBmp;
                        break;
                    case ".gif":
                        contentType = MimeTypes.ImageGif;
                        break;
                    case ".jpeg":
                    case ".jpg":
                    case ".jpe":
                    case ".jfif":
                    case ".pjpeg":
                    case ".pjp":
                        contentType = MimeTypes.ImageJpeg;
                        break;
                    case ".png":
                        contentType = MimeTypes.ImagePng;
                        break;
                    case ".tiff":
                    case ".tif":
                        contentType = MimeTypes.ImageTiff;
                        break;
                    default:
                        break;
                }
            }
            var picture = InsertPicture(formFile, contentType, Path.GetFileNameWithoutExtension(fileName));
            return picture;
        }
        public virtual Picture InsertPicture(IFormFile formFile, string mimeType, string seoFilename,
           string altAttribute = null, string titleAttribute = null,
           bool isNew = true, bool validateBinary = true)
        {
            mimeType = UtilHepler.EnsureNotNull(mimeType);
            mimeType = UtilHepler.EnsureMaximumLength(mimeType, 20);

            seoFilename = UtilHepler.EnsureMaximumLength(seoFilename, 100);


            var virtualPath = _uploadService.UploadFormFile(formFile);
            var picture = new Picture
            {
                MimeType = mimeType,
                SeoFilename = seoFilename,
                AltAttribute = altAttribute,
                TitleAttribute = titleAttribute,
                VirtualPath = virtualPath,
                IsNew = isNew
            };
            _imageRepository.Insert(picture);
            Save();

            return picture;
        }
    }
}
