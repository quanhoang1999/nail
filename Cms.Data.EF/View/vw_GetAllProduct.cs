
using System;

namespace Cms.Data.View
{
    public partial class vw_GetAllProduct
    {
        public System.Guid Id { get; set; }
        public string ProductCode { get; set; }
        public string Sku { get; set; }
        public string ShortName { get; set; }
        public System.Guid ParentGroupedProductId { get; set; }
        public Nullable<System.Guid> BaseUnitId { get; set; }
        public System.Guid CategoryId { get; set; }
        public System.Guid BrandId { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public Nullable<System.Guid> OriginId { get; set; }
        public bool IsGroup { get; set; }
        public string ImageName { get; set; }
        public string BrandName { get; set; }
        public string OriginName { get; set; }
        public string UserCreated { get; set; }
        public System.DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
    }
}
