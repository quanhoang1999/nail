using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Cms.Data.Interfaces;
using Cms.Infrastructure.SharedKernel;

namespace Cms.Data.Entities.Identity
{
    [Table("AppGroups")]
    public class AppGroup : DomainEntity<Guid>, IActive, IDateTracking
    {
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public Guid? BussinessId { get; set; }

        [ForeignKey("BussinessId")]
        public virtual Business Business { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public DateTime? DateDeleted { get; set; }
    }
}
