using AutoMapper;
using Cms.Data.Entities.Products;
using Cms.Data.Entities.System;
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
using static Cms.Infrastructure.Enums.EnumManage;

namespace Cms.Service.Implement.Media
{
    public class ImageService : WebServiceBase<Image, int, ImageViewModel>, IImageService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private IRepository<Image, int> _imageRepository;
        private IRepository<ProductImage, int> _imageProductRepository;
        private IUnitOfWork _unitOfWork;
        private UploadService _uploadService;
        private readonly IMapper _mapper;
        public ImageService(IRepository<Image, int> imageRepository, IMapper mapper,
            IRepository<ProductImage, int> imageProductRepository,
            UploadService uploadService,
            IWebHostEnvironment hostingEnvironment,
            IUnitOfWork unitOfWork) : base(imageRepository, unitOfWork, mapper)
        {
            this._imageRepository = imageRepository;
            _uploadService = uploadService;
            _imageProductRepository = imageProductRepository;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            this._unitOfWork = unitOfWork;
        }
        public Image InsertPicture(IFormFile formFile, string defaultFileName = "")
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
        public virtual Image InsertPicture(IFormFile formFile, string mimeType, string seoFilename,
           string altAttribute = null, string titleAttribute = null,
           bool isNew = true, bool validateBinary = true)
        {
            mimeType = UtilHepler.EnsureNotNull(mimeType);
            mimeType = UtilHepler.EnsureMaximumLength(mimeType, 20);

            seoFilename = UtilHepler.EnsureMaximumLength(seoFilename, 100);


            var virtualPath = _uploadService.UploadFormFile(formFile);
            var picture = new Image
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

            //event notification
            // _eventPublisher.EntityInserted(picture);

            return picture;
        }

        //public virtual string GetPictureUrl(Image picture,
        //  int targetSize = 0,
        //  bool showDefaultPicture = true,
        //  string storeLocation = null,
        //  PictureType defaultPictureType = PictureType.Entity)
        //{
        //    if (picture == null)
        //        return showDefaultPicture ? GetDefaultPictureUrl(targetSize, defaultPictureType, storeLocation) : string.Empty;

        //    byte[] pictureBinary = null;
        //    if (picture.IsNew)
        //    {
        //    //    DeletePictureThumbs(picture);
        //    //    pictureBinary = LoadPictureBinary(picture);

        //        if ((pictureBinary?.Length ?? 0) == 0)
        //            return showDefaultPicture ? GetDefaultPictureUrl(targetSize, defaultPictureType, storeLocation) : string.Empty;

        //        //we do not validate picture binary here to ensure that no exception ("Parameter is not valid") will be thrown
        //        picture = UpdatePicture(picture.Id,
        //            pictureBinary,
        //            picture.MimeType,
        //            picture.SeoFilename,
        //            picture.AltAttribute,
        //            picture.TitleAttribute,
        //            false,
        //            false);
        //    }

        //    var seoFileName = picture.SeoFilename; // = GetPictureSeName(picture.SeoFilename); //just for sure

        //    var lastPart = GetFileExtensionFromMimeType(picture.MimeType);
        //    string thumbFileName;
        //    if (targetSize == 0)
        //    {
        //        thumbFileName = !string.IsNullOrEmpty(seoFileName)
        //            ? $"{picture.Id:0000000}_{seoFileName}.{lastPart}"
        //            : $"{picture.Id:0000000}.{lastPart}";
        //    }
        //    else
        //    {
        //        thumbFileName = !string.IsNullOrEmpty(seoFileName)
        //            ? $"{picture.Id:0000000}_{seoFileName}_{targetSize}.{lastPart}"
        //            : $"{picture.Id:0000000}_{targetSize}.{lastPart}";
        //    }

        //    var thumbFilePath = GetThumbLocalPath(thumbFileName);

        //    //the named mutex helps to avoid creating the same files in different threads,
        //    //and does not decrease performance significantly, because the code is blocked only for the specific file.
        //    using (var mutex = new Mutex(false, thumbFileName))
        //    {
        //        if (GeneratedThumbExists(thumbFilePath, thumbFileName))
        //            return GetThumbUrl(thumbFileName, storeLocation);

        //        mutex.WaitOne();

        //        //check, if the file was created, while we were waiting for the release of the mutex.
        //        if (!GeneratedThumbExists(thumbFilePath, thumbFileName))
        //        {
        //            pictureBinary = pictureBinary ?? LoadPictureBinary(picture);

        //            if ((pictureBinary?.Length ?? 0) == 0)
        //                return showDefaultPicture ? GetDefaultPictureUrl(targetSize, defaultPictureType, storeLocation) : string.Empty;

        //            byte[] pictureBinaryResized;
        //            if (targetSize != 0)
        //            {
        //                //resizing required
        //                using (var image = Image.Load(pictureBinary, out var imageFormat))
        //                {
        //                    image.Mutate(imageProcess => imageProcess.Resize(new ResizeOptions
        //                    {
        //                        Mode = ResizeMode.Max,
        //                        Size = CalculateDimensions(image.Size(), targetSize)
        //                    }));

        //                    pictureBinaryResized = EncodeImage(image, imageFormat);
        //                }
        //            }
        //            else
        //            {
        //                //create a copy of pictureBinary
        //                pictureBinaryResized = pictureBinary.ToArray();
        //            }

        //            SaveThumb(thumbFilePath, thumbFileName, picture.MimeType, pictureBinaryResized);
        //        }

        //        mutex.ReleaseMutex();
        //    }

        //    return GetThumbUrl(thumbFileName, storeLocation);
        //}


        //public virtual string GetDefaultPictureUrl(int targetSize = 0,
        //    PictureType defaultPictureType = PictureType.Entity,
        //    string storeLocation = null)
        //{
        //    string defaultImageFileName;
        //    switch (defaultPictureType)
        //    {
        //        case PictureType.Avatar:
        //            defaultImageFileName = "default-avatar.jpg";
        //            break;
        //        case PictureType.Entity:
        //        default:
        //            defaultImageFileName = "default-image.jpg";
        //            break;
        //    }
        //    string imageFolder;
        //    imageFolder = $@"\uploaded\images\";

        //    string folder = _hostingEnvironment.WebRootPath + imageFolder;

        //    string filePath = Path.Combine(folder, defaultImageFileName);
        //    if (!File.Exists(filePath))
        //    {
        //        return string.Empty;
        //    }

        //    if (targetSize == 0)
        //    {
        //        var url = filePath;

        //        return url;
        //    }
        //    else
        //    {
        //        var fileExtension = Path.GetExtension(filePath);
        //        var thumbFileName = $"{_fileProvider.GetFileNameWithoutExtension(filePath)}_{targetSize}{fileExtension}";
        //        var thumbFilePath = GetThumbLocalPath(thumbFileName);
        //        if (!GeneratedThumbExists(thumbFilePath, thumbFileName))
        //        {
        //            using (var image = Image.Load(filePath, out var imageFormat))
        //            {
        //                image.Mutate(imageProcess => imageProcess.Resize(new ResizeOptions
        //                {
        //                    Mode = ResizeMode.Max,
        //                    Size = CalculateDimensions(image.Size(), targetSize)
        //                }));
        //                var pictureBinary = EncodeImage(image, imageFormat);
        //                SaveThumb(thumbFilePath, thumbFileName, imageFormat.DefaultMimeType, pictureBinary);
        //            }
        //        }

        //        var url = GetThumbUrl(thumbFileName, storeLocation);
        //        return url;
        //    }
        //}
    }
}
