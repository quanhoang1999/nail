using Cms.Data.Interfaces;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Nails
{
    [Table("NailEmployeeServices")]
    public class NailEmployeeService : DomainEntity<int>, IDateTracking, IActive
    {
        public int? NailServiceId { get; set; }
        [ForeignKey("NailServiceId")]
        public NailService NailService { get; set; }
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
