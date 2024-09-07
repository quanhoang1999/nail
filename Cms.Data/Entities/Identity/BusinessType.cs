using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Cms.Data.Interfaces;
using Cms.Infrastructure.SharedKernel;

namespace Cms.Data.Entities.Identity
{
    [Table("BusinessType")]
    public class BusinessType : DomainEntity<Guid>, IDateTracking, IActive
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        public DateTime DateCreated
        {
            get; set;
        }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual List<Business> Businesses { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
