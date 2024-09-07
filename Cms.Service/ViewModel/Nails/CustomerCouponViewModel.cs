using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cms.Service.ViewModel.Nails
{
    public class CustomerCouponViewModel
    {
        public int Id { get; set; }
        public string CouponCode { get; set; }
        public int? NailCustomerId { get; set; }
        public string Description { get; set; }
        public DateTime? DateUsed { get; set; }
        public NailCustomerViewModel NailCustomer { get; set; }
        public string NailCustomerFullName { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateExpired { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateStarted { get; set; }
        public int DiscountType { get; set; }
        public decimal Discount { get; set; }
        public decimal MinDiscount { get; set; }
        public decimal MaxDiscount { get; set; }
        public DateTime DateCreated { set; get; }
        public DateTime? DateModified { set; get; }
        public DateTime? DateDeleted { set; get; }
        public virtual NailStoreViewModel NailStore { get; set; }
        public int NailStoreId { get; set; }
        public string NailStoreName { get; set; }
    }
}
