using Cms.Data.Entities.Products;
using Cms.Service.ViewModel.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Abstracts.Products
{
    public interface IProductCategoryService:IWebServiceBase<ProductCategory, int, ProductCategoryViewModel>
    {
        void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items);
        void ReOrder(int sourceId, int targetId);

        List<ProductCategoryViewModel> GetHomeCategories(int top);
        List<ProductCategoryViewModel> GetAllByParentId(int parentId);
    }
}
