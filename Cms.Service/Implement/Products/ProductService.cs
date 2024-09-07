using AutoMapper;
using Cms.Data.EF;
using Cms.Data.Entities.Content;
using Cms.Data.Entities.Products;
using Cms.Data.Enums;
using Cms.Infrastructure.Dtos;
using Cms.Infrastructure.Enums;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Common;
using Cms.Service.Abstracts.Products;
using Cms.Service.Mapping;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Common;
using Cms.Service.ViewModel.Content;
using Cms.Service.ViewModel.Products;
using Cms.Utilities.Contants;
using Cms.Utilities.Helpers;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cms.Service.Implement.Products
{
    public class ProductService : WebServiceBase<Product, int, ProductViewModel>, IProductService
    {
        private IRepository<Product, int> _productRepository;
        private IRepository<Tag, string> _tagRepository;
        private IRepository<ProductTag, int> _productTagRepository;
        private IRepository<ProductQuantity, int> _productQuantityRepository;
        private IRepository<ProductImage, int> _productImageRepository;
        private IRepository<WholePrice, int> _wholePriceRepository;
        private IUploadService _uploadService;
        protected readonly AppDbContextDefault _dbContext;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public ProductService(IRepository<Product, int> productRepository,
            IRepository<Tag, string> tagRepository,
            IRepository<ProductQuantity, int> productQuantityRepository,
            IRepository<ProductImage, int> productImageRepository,
            IRepository<WholePrice, int> wholePriceRepository,
        IUnitOfWork unitOfWork, IMapper mapper,
        AppDbContextDefault dbContext,
        IUploadService uploadService,
        IRepository<ProductTag, int> productTagRepository) : base(productRepository, unitOfWork, mapper)
        {
            _productRepository = productRepository;
            _tagRepository = tagRepository;
            _productQuantityRepository = productQuantityRepository;
            _productTagRepository = productTagRepository;
            _wholePriceRepository = wholePriceRepository;
            _productImageRepository = productImageRepository;
            _uploadService = uploadService;
            _mapper = mapper;
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        public PagedResult<ProductViewModel> GetAllPaging(int? categoryId, string keyword, int page, int pageSize)
        {
            var query = _productRepository.GetAll(x => x.Status == Status.Active);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));
            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId.Value);

            int totalRow = query.Count();

            query = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize).Take(pageSize);

            var data = _mapper.Map<List<Product>, List<ProductViewModel>>(query.ToList());

            var paginationSet = new PagedResult<ProductViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }

        public PagedResult<ProductViewModel> Filter(FilterCommonViewModel viewModel)
        {
            var filterModel = _productRepository.GetAllIncluding(x => x.ProductCategory);
            if (!string.IsNullOrEmpty(viewModel.KeyWord))
                filterModel = filterModel.Where(c => c.Name.Contains(viewModel.KeyWord));
            if (viewModel.CategoryId > 0)
                filterModel = filterModel.Where(c => c.CategoryId == viewModel.CategoryId);
            //if (viewModel.CustomerTypeId > -1)
            //    filterModel = filterModel.Where(c => c.CustomerTypeId == viewModel.CustomerTypeId);
            var messagesVm = filterModel.OrderByDescending(x => x.DateCreated).Skip((viewModel.PageIndex - 1) * viewModel.PageSize).Take(viewModel.PageSize).ToList();
            var totalRow = filterModel.Count();
            var messages = _mapper.Map<List<Product>, List<ProductViewModel>>(messagesVm);
            var paginationSet = new PagedResult<ProductViewModel>()
            {
                Results = messages,
                CurrentPage = viewModel.PageIndex,
                RowCount = totalRow,
                PageSize = viewModel.PageSize
            };
            return paginationSet;
        }

        public ProductViewModel CreateUpdate(ProductViewModel productVm)
        {
            List<ProductTag> productTags = new List<ProductTag>();
            if (!string.IsNullOrEmpty(productVm.Tags))
            {
                string[] tags = productVm.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (_tagRepository.GetById(tagId) == null)
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = Infrastructure.Enums.TagType.Product
                        };
                        _tagRepository.Insert(tag);
                    }

                    ProductTag productTag = new ProductTag
                    {
                        TagId = tagId
                    };
                    productTags.Add(productTag);
                }
                var product = _mapper.Map<ProductViewModel, Product>(productVm);
                foreach (var productTag in productTags)
                {
                    product.ProductTags.Add(productTag);
                }
                _productRepository.Insert(product);
            }
            return productVm;
        }

        public void UpdateProduct(ProductViewModel productVm)
        {
            List<ProductTag> productTags = new List<ProductTag>();

            if (!string.IsNullOrEmpty(productVm.Tags))
            {
                string[] tags = productVm.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (_tagRepository.FirstOrDefault(x => x.Id == tagId) == null)
                    {
                        Tag tag = new Tag();
                        tag.Id = tagId;
                        tag.Name = t;
                        tag.Type = Infrastructure.Enums.TagType.Product;
                        _tagRepository.Insert(tag);
                    }
                    _productTagRepository.RemoveMultiple(_productTagRepository.GetAll(x => x.ProductId == productVm.Id).ToList());
                    ProductTag productTag = new ProductTag
                    {
                        TagId = tagId
                    };
                    productTags.Add(productTag);
                }
            }
            var product = _productRepository.GetById(productVm.Id);
            var dateCreated = product.DateCreated;
            product = productVm.ToEntity(product);
            product.DateCreated = dateCreated;
            foreach (var productTag in productTags)
            {
                product.ProductTags.Add(productTag);
            }
            _productRepository.Update(product);
        }

        public void ImportExcel(string filePath, int categoryId)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                Product product;
                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    product = new Product();
                    product.CategoryId = categoryId;

                    product.Name = workSheet.Cells[i, 1].Value.ToString();

                    product.Description = workSheet.Cells[i, 2].Value.ToString();

                    decimal.TryParse(workSheet.Cells[i, 3].Value.ToString(), out var originalPrice);
                    product.OriginalPrice = originalPrice;

                    decimal.TryParse(workSheet.Cells[i, 4].Value.ToString(), out var price);
                    product.Price = price;
                    decimal.TryParse(workSheet.Cells[i, 5].Value.ToString(), out var promotionPrice);

                    product.PromotionPrice = promotionPrice;
                    product.Content = workSheet.Cells[i, 6].Value.ToString();
                    product.SeoKeywords = workSheet.Cells[i, 7].Value.ToString();

                    product.SeoDescription = workSheet.Cells[i, 8].Value.ToString();
                    bool.TryParse(workSheet.Cells[i, 9].Value.ToString(), out var hotFlag);

                    product.HotFlag = hotFlag;
                    bool.TryParse(workSheet.Cells[i, 10].Value.ToString(), out var homeFlag);
                    product.HomeFlag = homeFlag;

                    product.Status = Status.Active;

                    _productRepository.Insert(product);
                }
            }
        }

        public void AddQuantity(int productId, List<ProductQuantityViewModel> quantities)
        {
            _productQuantityRepository.RemoveMultiple(_productQuantityRepository.GetAll(x => x.ProductId == productId).ToList());
            foreach (var quantity in quantities)
            {
                _productQuantityRepository.Insert(new ProductQuantity()
                {
                    ProductId = productId,
                    ColorId = quantity.ColorId,
                    SizeId = quantity.SizeId,
                    Quantity = quantity.Quantity
                });
            }
        }

        public List<ProductQuantityViewModel> GetQuantities(int productId)
        {
            var model = _productQuantityRepository.GetAll(x => x.ProductId == productId).ToList();
            return _mapper.Map<List<ProductQuantity>, List<ProductQuantityViewModel>>(model);
        }

        public void AddImages(int productId, string[] images)
        {
            _productImageRepository.RemoveMultiple(_productImageRepository.GetAll(x => x.ProductId == productId).ToList());
            foreach (var image in images)
            {
                _productImageRepository.Insert(new ProductImage()
                {
                    Path = image,
                    ProductId = productId,
                    Caption = string.Empty
                });
            }
        }

        public List<ProductImageViewModel> GetImages(int productId)
        {
            var model = _productImageRepository.GetAll(x => x.ProductId == productId).ToList();
            return _mapper.Map<List<ProductImage>, List<ProductImageViewModel>>(model);
        }

        public void AddWholePrice(int productId, List<WholePriceViewModel> wholePrices)
        {
            _wholePriceRepository.RemoveMultiple(_wholePriceRepository.GetAll(x => x.ProductId == productId).ToList());
            foreach (var wholePrice in wholePrices)
            {
                _wholePriceRepository.Insert(new WholePrice()
                {
                    ProductId = productId,
                    FromQuantity = wholePrice.FromQuantity,
                    ToQuantity = wholePrice.ToQuantity,
                    Price = wholePrice.Price
                });
            }
        }

        public List<WholePriceViewModel> GetWholePrices(int productId)
        {
            var model = _wholePriceRepository.GetAll(x => x.ProductId == productId).ToList();
            return _mapper.Map<List<WholePrice>, List<WholePriceViewModel>>(model);
        }

        public List<ProductViewModel> GetLastest(int top)
        {
            var model = _productRepository.GetAll(x => x.Status == Status.Active).OrderByDescending(x => x.DateCreated)
               .Take(top).ToList();
            return _mapper.Map<List<Product>, List<ProductViewModel>>(model);
        }

        public List<ProductViewModel> GetHotProduct(int top)
        {
            var model = _productRepository.GetAll(x => x.Status == Status.Active && x.HotFlag == true)
                  .OrderByDescending(x => x.DateCreated)
                  .Take(top)
                  .ToList();
            return _mapper.Map<List<Product>, List<ProductViewModel>>(model);
        }

        public List<ProductViewModel> GetRelatedProducts(int id, int top)
        {
            var product = _productRepository.GetById(id);
            var model = _productRepository.GetAll(x => x.Status == Status.Active
                && x.Id != id && x.CategoryId == product.CategoryId)
            .OrderByDescending(x => x.DateCreated)
            .Take(top)
            .ToList();
            return _mapper.Map<List<Product>, List<ProductViewModel>>(model);
        }

        public List<ProductViewModel> GetUpsellProducts(int top)
        {
            var model = _productRepository.GetAll(x => x.PromotionPrice != null)
              .OrderByDescending(x => x.DateModified)
              .Take(top).ToList();
            return _mapper.Map<List<Product>, List<ProductViewModel>>(model);
        }

        public List<TagViewModel> GetProductTags(int productId)
        {
            var tags = _tagRepository.GetAll();
            var productTags = _productTagRepository.GetAll();

            var query = from t in tags
                        join pt in productTags
                        on t.Id equals pt.TagId
                        where pt.ProductId == productId
                        select new TagViewModel()
                        {
                            Id = t.Id,
                            Name = t.Name
                        };
            return query.ToList();
        }
        public List<TagViewModel> GetAllProductTags()
        {
            var tags = _tagRepository.GetAll();
            var productTags = _productTagRepository.GetAll();

            var query = from t in tags
                        join pt in productTags
                        on t.Id equals pt.TagId
                        select new TagViewModel()
                        {
                            Id = t.Id,
                            Name = t.Name
                        };
            return query.ToList();
        }
        public bool CheckAvailability(int productId, int size, int color)
        {
            var quantity = _productQuantityRepository.FirstOrDefault(x => x.ColorId == color && x.SizeId == size && x.ProductId == productId);
            if (quantity == null)
                return false;
            return quantity.Quantity > 0;
        }

        public void Copy(CopyViewModel viewModel)
        {
            var entity = _productRepository.GetById(viewModel.Id);
            entity.Id = 0;
            entity.Name = viewModel.Name;
            if (!viewModel.IsCopy)
                entity.Image = null;
            else
                entity.Image = _uploadService.CopyFile(entity.Image);

            //Tags
            List<ProductTag> productTags = new List<ProductTag>();
            if (!string.IsNullOrEmpty(entity.Tags))
            {
                string[] tags = entity.Tags.Split(',');

                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (_tagRepository.GetById(tagId) == null)
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = Infrastructure.Enums.TagType.Product
                        };
                        _tagRepository.Insert(tag);
                    }

                    ProductTag productTag = new ProductTag
                    {
                        TagId = tagId
                    };
                    productTags.Add(productTag);
                }
                //  var product = _mapper.Map<ProductViewModel, Product>(productVm);
                foreach (var productTag in productTags)
                {
                    entity.ProductTags.Add(productTag);
                }
            }
            _productRepository.Insert(entity);
            Save();
        }
        #region Util
        [Obsolete]
        private Dictionary<int, int> GetProductCount(int storeId, bool showHidden)
        {            //invoke stored procedure
            return _dbContext.QueryFromSql<ProductTagWithCount>("ProductTagCountLoadAll", null)
                .ToDictionary(item => item.ProductTagId, item => item.ProductCount);
        }
        #endregion
    }
}
