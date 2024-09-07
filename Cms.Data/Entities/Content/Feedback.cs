using Cms.Data.Interfaces;
using Cms.Infrastructure.Enums;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Content
{
    [Table("Feedback")]
    public class Feedback : DomainEntity<Guid>, IDateTracking
    {
        [StringLength(250)]
        [Required]
        public string Name { set; get; }

        [StringLength(250)]
        public string Email { set; get; }

        [StringLength(500)]
        public string Message { set; get; }

        public string Phone { set; get; }

        public Status Status { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime? DateModified { set; get; }
        public DateTime? DateDeleted { set; get; }
    }
}
