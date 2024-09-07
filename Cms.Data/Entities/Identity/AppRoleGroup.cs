using Cms.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cms.Data.Entities.Identity
{
    [Table("AppRoleGroup")]
    public class AppRoleGroup : DomainEntity<Guid>
    {
        [Required]
        public Guid RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual AppRole AppRole { get; set; }

        [Required]
        public Guid GroupId { get; set; }

        [ForeignKey("GroupId")]
        public virtual AppGroup AppGroup { get; set; }
    }
}
