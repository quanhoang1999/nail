using Cms.Infrastructure.Enums;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.System
{
    [Table("Announcement")]
    public class Announcement: DomainEntity<Guid>
    {
        [Required]
        [StringLength(250)]
        public string Title { set; get; }

        [StringLength(250)]
        public string Content { set; get; }

        public DateTime DateCreated { set; get; }
        public DateTime? DateModified { set; get; }

        public DateTime? DateDeleted { set; get; }
        public Status Status { set; get; }
        public Guid OwnerId { set; get; }
    }
}
