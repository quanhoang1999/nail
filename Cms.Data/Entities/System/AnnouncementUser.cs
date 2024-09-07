using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.System
{
    [Table("AnnouncementUser")]
    public class AnnouncementUser : DomainEntity<Guid>
    {
        [Required]
        public Guid AnnouncementId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public bool? HasRead { get; set; }
    }
}
