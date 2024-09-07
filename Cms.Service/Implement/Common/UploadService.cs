using AutoMapper;
using Cms.Data.Entities.Media;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Common;
using Cms.Service.ViewModel.Media;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Cms.Service.Implement.Common
{
    public class UploadService : IUploadService
    {
        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".jpg", ".jpeg", ".png", ".doc", ".docx", ".xlsx", ".xls", ".pdf" };
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IRepository<Picture, int> _pictureRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UploadService(IWebHostEnvironment hostingEnvironment, IRepository<Picture, int> pictureRepository,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _hostingEnvironment = hostingEnvironment;
            _pictureRepository = pictureRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public string UploadFile(IFormCollection formCollection, string type = null)
        {
            var files = formCollection.Files;
            try
            {
                if (files.Count == 0)
                {
                    return string.Empty;
                }
                else
                {
                    var file = files[0];
                    Guid fileNameEncode = Guid.NewGuid();
                    string extension = Path.GetExtension(file.FileName);
                    string imageFolder;
                    imageFolder = $@"\uploaded\images\";

                    string folder = _hostingEnvironment.WebRootPath + imageFolder;

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    string filePath = Path.Combine(folder, fileNameEncode + extension);
                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    return fileNameEncode + extension;
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }

        public string UploadFormFile(IFormFile formFile, string type = null)
        {
            var file = formFile;
            Guid fileNameEncode = Guid.NewGuid();
            string extension = Path.GetExtension(file.FileName);
            string imageFolder;
            imageFolder = $@"\uploaded\images\";

            string folder = _hostingEnvironment.WebRootPath + imageFolder;

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string filePath = Path.Combine(folder, fileNameEncode + extension);
            using (FileStream fs = System.IO.File.Create(filePath))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            return fileNameEncode + extension;


        }

        public void DeleteFile(string fileName)
        {
            string imageFolder;
            imageFolder = $@"\uploaded\images\";
            string folder = _hostingEnvironment.WebRootPath + imageFolder;
            string filePath = Path.Combine(folder, fileName);
            System.IO.FileInfo fi = new System.IO.FileInfo(filePath);
            try
            {
                fi.Delete();
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public string CopyFile(string fileName)
        {
            string imageFolder;
            imageFolder = $@"\uploaded\images\";
            string folder = _hostingEnvironment.WebRootPath + imageFolder;
            Guid fileNameEncode = Guid.NewGuid();
            string extension = Path.GetExtension(fileName);
            string filePath = Path.Combine(folder, fileName);
            string destFile = System.IO.Path.Combine(folder, fileNameEncode + extension);
            File.Copy(filePath, destFile, true);
            return fileNameEncode + extension;
        }

        public virtual byte[] GetDownloadBits(IFormFile file)
        {
            using (var fileStream = file.OpenReadStream())
            {
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    return fileBytes;
                }
            }
        }

        public List<string> UploadMultilImage(IFormFile[] files)
        {
            List<string> lstImages = new List<string>();
            if (files == null)
                return null;
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue
                                                      .Parse(file.ContentDisposition)
                                                      .FileName
                                                      .Trim('"');
                Guid fileNameEncode = Guid.NewGuid();
                string extension = Path.GetExtension(file.FileName).ToLower();

                if (ACCEPTED_FILE_TYPES.Any(s => s == extension))
                {
                    string imageFolder = $@"\uploaded\product\";
                    string folder = _hostingEnvironment.WebRootPath + imageFolder;
                    if (!Directory.Exists(folder)){
                        Directory.CreateDirectory(folder);
                    }
                    string filePath = Path.Combine(folder, fileNameEncode + extension);
                    using (FileStream fs = System.IO.File.Create(filePath)){
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    if (System.IO.File.Exists(filePath))
                        lstImages.Add(fileNameEncode + extension);
                    var picture = new Picture();
                    picture.MimeType = extension;
                    picture.VirtualPath = filePath;
                    picture.TitleAttribute = filename;
                    picture.SeoFilename = filename;
                    _pictureRepository.Insert(picture);
                    _unitOfWork.Commit();

                }

            }
            return lstImages;
        }
        public List<PictureViewModel> UploadMutiple(IFormFile[] files)
        {
            List<Picture> lstImages = new List<Picture>();
            if (files == null)
                return null;
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue
                                                      .Parse(file.ContentDisposition)
                                                      .FileName
                                                      .Trim('"');
                Guid fileNameEncode = Guid.NewGuid();
                string extension = Path.GetExtension(file.FileName).ToLower();

                if (ACCEPTED_FILE_TYPES.Any(s => s == extension))
                {
                    string imageFolder = $@"\uploaded\product\";
                    string folder = _hostingEnvironment.WebRootPath + imageFolder;
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    string filePath = Path.Combine(folder, fileNameEncode + extension);
                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                  //  if (System.IO.File.Exists(filePath))
                   //     lstImages.Add(fileNameEncode + extension);
                    var picture = new Picture();
                    picture.FileName = fileNameEncode.ToString();
                    picture.MimeType = extension;
                    picture.AltAttribute = filename;
                    picture.VirtualPath = fileNameEncode + extension;
                    picture.TitleAttribute = filename;
                    picture.SeoFilename = filename;
                    _pictureRepository.Insert(picture);
                    _unitOfWork.Commit();

                    lstImages.Add(picture);
                }

            }
            var pictures = _mapper.Map<List<Picture>, List<PictureViewModel>>(lstImages);
            return pictures;
        }

    }
}
