using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Cms.Service.Abstracts.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Cms.Website.Areas.Admin.Controllers
{
    public class UploadController : BaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUploadService _uploadService;
        public UploadController(IWebHostEnvironment hostingEnvironment, IUploadService uploadService)
        {
            _hostingEnvironment = hostingEnvironment;
            _uploadService = uploadService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task UploadImageForCKEditor(IList<IFormFile> upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            DateTime now = DateTime.Now;
            if (upload.Count == 0)
            {
                await HttpContext.Response.WriteAsync("Yêu cầu nhập ảnh");
            }
            else
            {
                var file = upload[0];
                var filename = ContentDispositionHeaderValue
                                    .Parse(file.ContentDisposition)
                                    .FileName
                                    .Trim('"');

                var imageFolder = $@"\uploaded\images\{now.ToString("yyyyMMdd")}";

                string folder = _hostingEnvironment.WebRootPath + imageFolder;

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string filePath = Path.Combine(folder, filename);
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                await HttpContext.Response.WriteAsync("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", '" + Path.Combine(imageFolder, filename).Replace(@"\", @"/") + "');</script>");
            }
        }
        /// <summary>
        /// Upload image for form
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadImage()
        {
            var files = Request.Form.Files;
            if (files.Count == 0)
            {
                return new OkObjectResult("File không tồn tài");
            }
            else
            {
                var url = _uploadService.UploadFile(Request.Form, null);
                return new OkObjectResult(url);
            }
        }
        [HttpPost]
        public IActionResult UploadMultipleImage(IFormFile[] files)
        {
          
            if (files.Length == 0)
            {
                return new OkObjectResult("File không tồn tài");
            }
            else
            {
                var model = _uploadService.UploadMutiple(files);
                return new OkObjectResult(model);
            }
        }
    }
}