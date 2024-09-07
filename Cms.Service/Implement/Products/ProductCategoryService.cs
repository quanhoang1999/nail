using AutoMapper;
using Cms.Data.Entities.Products;
using Cms.Infrastructure.Enums;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Products;
using Cms.Service.ViewModel.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.Service.Implement.Products
{
    public class ProductCategoryService : WebServiceBase<ProductCategory, int, ProductCategoryViewModel>, IProductCategoryService
    {
        private IRepository<ProductCategory, int> _productCategoryRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ProductCategoryService(IRepository<ProductCategory, int> productCategoryRepository,
            IUnitOfWork unitOfWork, IMapper mapper) : base(productCategoryRepository, unitOfWork, mapper)
        {
            _productCategoryRepository = productCategoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public List<ProductCategoryViewModel> GetAllByParentId(int parentId)
        {
            var model = _productCategoryRepository.GetAll(x => x.Status == Status.Active && x.ParentId == parentId).ToList();
            return _mapper.Map<List<ProductCategory>, List<ProductCategoryViewModel>>(model);
        }

        public List<ProductCategoryViewModel> GetHomeCategories(int top)
        {
            var query = _productCategoryRepository
                .GetAllIncluding(x => x.HomeFlag == true, c => c.Products)
                  .OrderBy(x => x.HomeOrder)
                  .Take(top).ToList();

            var categories = _mapper.Map<List<ProductCategory>, List<ProductCategoryViewModel>>(query); ;
            foreach (var category in categories)
            {
                //category.Products = _productRepository
                //    .FindAll(x => x.HotFlag == true && x.CategoryId == category.Id)
                //    .OrderByDescending(x => x.DateCreated)
                //    .Take(5)
                //    .ProjectTo<ProductViewModel>().ToList();
            }
            return categories;
        }

        public void ReOrder(int sourceId, int targetId)
        {
            var source = _productCategoryRepository.GetById(sourceId);
            var target = _productCategoryRepository.GetById(targetId);
            int tempOrder = source.SortOrder;
            source.SortOrder = target.SortOrder;
            target.SortOrder = tempOrder;

            _productCategoryRepository.Update(source);
            _productCategoryRepository.Update(target);
        }

        public void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            var sourceCategory = _productCategoryRepository.GetById(sourceId);
            sourceCategory.ParentId = targetId;
            _productCategoryRepository.Update(sourceCategory);

            //Get all sibling
            var sibling = _productCategoryRepository.GetAll(x => items.ContainsKey(x.Id));
            foreach (var child in sibling)
            {
                child.SortOrder = items[child.Id];
                _productCategoryRepository.Update(child);
            }
        }
    }
}
