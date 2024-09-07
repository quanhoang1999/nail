using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Cms.Infrastructure.Dtos;
using Cms.Service.Abstracts.Products;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Common;
using Cms.Service.ViewModel.Products;
using Cms.Utilities.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace Cms.Website.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private IProductService _productService;
        private IProductCategoryService _productCategoryService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductController(IProductService productService,
            IProductCategoryService productCategoryService,
            IWebHostEnvironment hostingEnvironment)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProductTags()
        {
            return View();
        }

        public IActionResult ProductEdit(int id)
        {
            if (id > 0)
            {
                var model = _productService.GetById(id);
                return View(model);
            }
            return View();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _productService.GetAll();
            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var model = _productCategoryService.GetAll();
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult GetAllPaging(FilterCommonViewModel viewModel)
        {
            var model = _productService.Filter(viewModel);
            return new OkObjectResult(model);
        }
        [HttpGet]
        public IActionResult GetAllTagPaging(FilterCommonViewModel viewModel)
        {
            //     var model = _productService.GetProductTags(viewModel);
            return new OkObjectResult(viewModel);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _productService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(ProductViewModel productVm)
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
                    _productService.CreateUpdate(productVm);
                }
                else
                {
                    _productService.UpdateProduct(productVm);
                }
                _productService.Save();
                return new OkObjectResult(productVm);
            }
        }

        [HttpPost]
        public IActionResult Copy(CopyViewModel viewModel)
        {
            _productService.Copy(viewModel);
            return new OkObjectResult(true);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                _productService.Delete(id);
                _productService.Save();

                return new OkObjectResult(id);
            }
        }
        [HttpPost]
        public IActionResult SaveQuantities(int productId, List<ProductQuantityViewModel> quantities)
        {
            _productService.AddQuantity(productId, quantities);
            _productService.Save();
            return new OkObjectResult(quantities);
        }

        [HttpGet]
        public IActionResult GetQuantities(int productId)
        {
            var quantities = _productService.GetQuantities(productId);
            return new OkObjectResult(quantities);
        }
        [HttpPost]
        public IActionResult SaveImages(int productId, string[] images)
        {
            _productService.AddImages(productId, images);
            _productService.Save();
            return new OkObjectResult(images);
        }

        [HttpGet]
        public IActionResult GetImages(int productId)
        {
            var images = _productService.GetImages(productId);
            return new OkObjectResult(images);
        }

        [HttpPost]
        public IActionResult SaveWholePrice(int productId, List<WholePriceViewModel> wholePrices)
        {
            _productService.AddWholePrice(productId, wholePrices);
            _productService.Save();
            return new OkObjectResult(wholePrices);
        }

        [HttpGet]
        public IActionResult GetWholePrices(int productId)
        {
            var wholePrices = _productService.GetWholePrices(productId);
            return new OkObjectResult(wholePrices);
        }
        [HttpPost]
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
                _productService.ImportExcel(filePath, categoryId);
                _productService.Save();
                return new OkObjectResult(filePath);
            }
            return new NoContentResult();
        }
        [HttpPost]
        public IActionResult ExportExcel()
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string directory = Path.Combine(sWebRootFolder, "export-files");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string sFileName = $"Product_{DateTime.Now:yyyyMMddhhmmss}.xlsx";
            string fileUrl = $"{Request.Scheme}://{Request.Host}/export-files/{sFileName}";
            FileInfo file = new FileInfo(Path.Combine(directory, sFileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            }
            var products = _productService.GetAll();
            using (ExcelPackage package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Products");
                worksheet.Cells["A1"].LoadFromCollection(products, true, TableStyles.Light1);
                worksheet.Cells.AutoFitColumns();
                package.Save(); //Save the workbook.
            }
            return new OkObjectResult(fileUrl);
        }

        [HttpGet]
        public IActionResult GetAllProductTags()
        {
            var productTags = _productService.GetAllProductTags();
            return new OkObjectResult(new GenericResult(true, productTags));
        }
    }
}