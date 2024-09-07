using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Cms.Service.Abstracts.Common;
using Cms.Service.Abstracts.Content;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Content;
using Cms.Utilities.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cms.Website.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        private readonly IPostService _postService;
        private readonly IPostCategoryService _postCategoryService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public IUploadService _uploadService;
        public PostController(IPostService postService, IUploadService uploadService, IPostCategoryService postCategoryService, IWebHostEnvironment hostingEnvironment)
        {
            _postService = postService;
            _postCategoryService = postCategoryService;
            _hostingEnvironment = hostingEnvironment;
            _uploadService = uploadService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Filter(FilterCommonViewModel viewModel)
        {
            var filterModel = _postService.Filter(viewModel);
            return new OkObjectResult(filterModel);
        }
        [HttpPost]
        public IActionResult SaveEntity(PostViewModel postView)
        {

            if (postView.Id == null || postView.Id == Guid.Empty)
            {
                _postService.Create(postView);
            }
            else
            {
                _postService.UpdatePost(postView);
            }
            return new OkObjectResult(postView);

        }

        public IActionResult Edit(Guid id)
        {
            ViewBag.PostID = "";
            if (id != null || id != Guid.Empty)
            {
                var model = _postService.GetById(id);
                ViewBag.PostID = id;
                return View(model);
            }
            return View();


        }

        [HttpGet]
        public IActionResult GetById(Guid id)
        {
            var model = _postService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                _postService.Delete(id);

                return new OkObjectResult(id);
            }
        }

        public IActionResult Category()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetCategoryById(int id)
        {
            var model = _postCategoryService.GetById(id);

            return new ObjectResult(model);
        }
        [HttpPost]
        public IActionResult SaveEntityCategory(PostCategoryViewModel productVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                productVm.SeoAlias = TextHelper.ToUnsignString(productVm.Name);
                if (productVm.Id == 0)
                {
                    _postCategoryService.Add(productVm);
                }
                else
                {
                    _postCategoryService.Update(productVm);
                }
                _postCategoryService.Save();
                return new OkObjectResult(productVm);

            }
        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            if (id == 0)
            {
                return new BadRequestResult();
            }
            else
            {
                _postCategoryService.Delete(id);
                _postCategoryService.Save();
                return new OkObjectResult(id);
            }
        }
        [HttpGet]
        public IActionResult GetAllCategory()
        {
            var model = _postCategoryService.GetAll();
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                if (sourceId == targetId)
                {
                    return new BadRequestResult();
                }
                else
                {
                    _postCategoryService.UpdateParentId(sourceId, targetId, items);
                    _postCategoryService.Save();
                    return new OkResult();
                }
            }
        }

        [HttpPost]
        public IActionResult ReOrder(int sourceId, int targetId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                if (sourceId == targetId)
                {
                    return new BadRequestResult();
                }
                else
                {
                    _postCategoryService.ReOrder(sourceId, targetId);
                    _postCategoryService.Save();
                    return new OkResult();
                }
            }
        }

        public IActionResult ImportExcel(IList<IFormFile> files, int categoryId)
        {
            if (files != null && files.Count > 0)
            {
                var file = files[0];
                var filename = ContentDispositionHeaderValue
                                   .Parse(file.ContentDisposition)
                                   .FileName
                                   .Trim('"');

                string folder = _hostingEnvironment.WebRootPath + $@"\uploaded\excels";
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
                _postService.ImportExcel(filePath, categoryId);
                _postService.Save();
                return new OkObjectResult(filePath);
            }
            return new NoContentResult();
        }
    }
}