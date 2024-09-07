using Cms.Data.Interfaces;
using Cms.Infrastructure.Interfaces;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Nails
{
    [Table("Employees")]
    public class NailEmployee : DomainEntity<Guid>, IDateTracking, IActive, IHasSoftDelete
    {
        [MaxLength(256)]
        public string Name { get; set; }
        [Column(TypeName = "varchar(3)")]
        public string ShortName { get; set; }
        public string Avatar { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeteted { get; set; } = false;
        public Guid UserCreated { get; set; }
        public Guid? UserModified { get; set; }
        public Guid? UserDeleted { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int? NailStoreId { get; set; }
        public virtual NailStore NailStore { get; set; }
    }
}
