using Cms.Data.Enums;
using Cms.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cms.Service.ViewModel.Products
{
    public class BillViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerName { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustomerAddress { set; get; }

        [Required]
        [MaxLength(50)]
        public string CustomerMobile { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustomerMessage { set; get; }

        public PaymentMethod PaymentMethod { set; get; }

        public BillStatus BillStatus { set; get; }

        public DateTime DateCreated { set; get; }

        public DateTime DateModified { set; get; }

        public Status Status { set; get; }

        public Guid? CustomerId { set; get; }

#pragma warning disable CA2227 // Collection properties should be read only
        public List<BillDetailViewModel> BillDetails { set; get; }
#pragma warning restore CA2227 // Collection properties should be read only
    }
}
