using Cms.Data.Interfaces;
using Cms.Infrastructure.Interfaces;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Nails
{
    [Table("NailOrderDetails")]
    public class NailOrderDetail : DomainEntity<int>, IDateTracking, IActive, IHasSoftDelete
    {
        public int NailServiceId { get; set; }
        [ForeignKey("NailServiceId")]
        public NailService NailService { get; set; }
        public int NailOrderId { get; set; }
        [ForeignKey("NailOrderId")]
        public NailOrder NailOrder { get; set; }
        public decimal Quantity { set; get; }
        public decimal Price { get; set; }
        public string NailServiceName { get; set; }
        public Guid? NailEmployeeId { get; set; }
        [ForeignKey("NailEmployeeId")]
        public NailEmployee NailEmployee { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid UserCreated { get; set; }
        public Guid? UserModified { get; set; }
        public Guid? UserDeleted { get; set; }
    }
}
