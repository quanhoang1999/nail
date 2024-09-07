using Cms.Data.Interfaces;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Nails
{
    [Table("NailPromotions")]
    public class NailPromotion : DomainEntity<int>, IDateTracking, IActive
    {
        [MaxLength(256)]
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal? SalePrice { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpriredDate { get; set; }
        public bool IsShowHomePage { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid UserCreated { get; set; }
        public Guid? UserModified { get; set; }
        public Guid? UserDeleted { get; set; }
        public int? NailStoreId { get; set; }
        public virtual NailStore NailStore { get; set; }

    }
}
