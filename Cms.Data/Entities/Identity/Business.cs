using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Cms.Data.Interfaces;
using Cms.Infrastructure.SharedKernel;

namespace Cms.Data.Entities.Identity
{
    [Table("Business")]
    public class Business : DomainEntity<Guid>, IDateTracking, IActive
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public string Description { get; set; }
        public string AppId { get; set; }
        public string AccessToken { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public Guid BusinessTypeId { get; set; }

        [ForeignKey("BusinessTypeId")]
        public virtual BusinessType BusinessType { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

    }
}
