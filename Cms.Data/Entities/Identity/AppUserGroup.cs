using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Cms.Infrastructure.SharedKernel;

namespace Cms.Data.Entities.Identity
{
    [Table("AppUserGroup")]
    public class AppUserGroup: DomainEntity<Guid>
    {
        [Required]
        public Guid UserId { get; set; }     


        [Required]
        public Guid GroupId { get; set; }

      
    }
}
