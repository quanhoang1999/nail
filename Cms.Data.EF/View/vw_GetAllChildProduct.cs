
using System;
namespace Cms.Data.View
{
    public partial class vw_GetAllChildProduct
    {
        public System.Guid Id { get; set; }
        public System.Guid ProductParentId { get; set; }
        public string ParentProductName { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public decimal PriceSale { get; set; }
        public decimal PriceBuy { get; set; }
        public int Amount { get; set; }
        public string Thumbnail { get; set; }
        public bool IsAvaible { get; set; }
        public string Detail { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<System.DateTime> DateDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
