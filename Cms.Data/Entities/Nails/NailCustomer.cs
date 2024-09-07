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
    [Table("NailCustomers")]
    public class NailCustomer : DomainEntity<int>, IDateTracking, IHasSoftDelete, IActive
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        [StringLength(250)]
        public string Email { set; get; }
        public string Phone { set; get; }
        public DateTime? Birthdate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
        public Guid UserCreated { get; set; }
        public Guid? UserModified { get; set; }
        public Guid? UserDeleted { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int? NailStoreId { get; set; }
        public virtual NailStore NailStore { get; set; }
    }
}
