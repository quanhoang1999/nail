using Cms.Data.Entities.Products;
using Cms.Infrastructure.Dtos;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Common;
using Cms.Service.ViewModel.Content;
using Cms.Service.ViewModel.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Abstracts.Products
{
    public interface IProductService:IWebServiceBase<Product, int, ProductViewModel>
    {
        PagedResult<ProductViewModel> GetAllPaging(int? categoryId, string keyword, int page, int pageSize);
        PagedResult<ProductViewModel> Filter(FilterCommonViewModel viewModel);
        ProductViewModel CreateUpdate(ProductViewModel product);

        void UpdateProduct(ProductViewModel product);
        void ImportExcel(string filePath, int categoryId);
        void AddQuantity(int productId, List<ProductQuantityViewModel> quantities);

        List<ProductQuantityViewModel> GetQuantities(int productId);

        void AddImages(int productId, string[] images);

        List<ProductImageViewModel> GetImages(int productId);

        void AddWholePrice(int productId, List<WholePriceViewModel> wholePrices);

        List<WholePriceViewModel> GetWholePrices(int productId);

        List<ProductViewModel> GetLastest(int top);

        List<ProductViewModel> GetHotProduct(int top);
        List<ProductViewModel> GetRelatedProducts(int id, int top);

        List<ProductViewModel> GetUpsellProducts(int top);

        List<TagViewModel> GetProductTags(int productId);

        bool CheckAvailability(int productId, int size, int color);
        List<TagViewModel> GetAllProductTags();
        void Copy(CopyViewModel viewModel);
    }
}
