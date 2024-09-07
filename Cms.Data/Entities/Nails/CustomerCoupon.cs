using Cms.Data.Interfaces;
using Cms.Infrastructure.Interfaces;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Nails
{
    [Table("CustomerCoupons")]
    public class CustomerCoupon : DomainEntity<int>, IDateTracking, IHasSoftDelete
    {
        public string CouponCode { get; set; }
        public string Description { get; set; }
        public DateTime? DateUsed { get; set; }
        public int? NailCustomerId { get; set; }
        [ForeignKey("NailCustomerId")]
        public NailCustomer NailCustomer { get; set; }
        public int? NailStoreId { get; set; }
        public virtual NailStore NailStore { get; set; }
        public DateTime DateExpired { get; set; }
        public DateTime DateStarted { get; set; }
        public int DiscountType { get; set; }
        public decimal Discount { get; set; }
        public decimal MinDiscount { get; set; }
        public decimal MaxDiscount { get; set; }
        public DateTime DateCreated { set; get; }
        public DateTime? DateModified { set; get; }
        public DateTime? DateDeleted { set; get; }
        public bool IsDeleted { get; set; }
    }
}
