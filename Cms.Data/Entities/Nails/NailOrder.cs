using Cms.Data.Interfaces;
using Cms.Infrastructure.Interfaces;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Nails
{
    [Table("NailOrders")]
    public class NailOrder : DomainEntity<int>, IDateTracking, IActive, IHasSoftDelete
    {
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public DateTime DatePick { get; set; }
        public DateTime TimePick { get; set; }
        public bool IsFinish { get; set; }
        public bool IsSendSMS { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid UserCreated { get; set; }
        public Guid? UserModified { get; set; }
        public Guid? UserDeleted { get; set; }
        public ICollection<NailOrderDetail> NailOrderDetails { get; set; }
        public int? NailStoreId { get; set; }
        public virtual NailStore NailStore { get; set; }
    }
}
