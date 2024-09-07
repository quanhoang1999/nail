using Cms.Data.Interfaces;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Nails
{
    [Table("NailEGifts")]
    public class NailEGift : DomainEntity<int>, IDateTracking, IActive
    {
        [MaxLength(256)]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public string RecipientEmail { get; set; }
        public string RecipientNote { get; set; }
        public int EGiftID { get; set; }
        public string Image { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TaxPercent { get; set; }
        public decimal? DisountPrice { get; set; }
        public decimal? DisountPercent { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpriredDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsCompevbs { get; set; }
        public bool IsPayPal { get; set; }
        public string PayPayError { get; set; }
        public string PayPalID { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid UserCreated { get; set; }
        public Guid? UserModified { get; set; }
        public Guid? UserDeleted { get; set; }
    }
}
