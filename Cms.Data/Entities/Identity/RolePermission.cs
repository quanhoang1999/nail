using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Identity
{
    [Table("RolePermissions")]
    public class RolePermission : DomainEntity<Guid>
    {
        public Guid RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual AppRole AppRole { get; set; }

        public Guid PermissionId { get; set; }

        [ForeignKey("PermissionId")]
        public virtual Permission Permission { get; set; }
    }
}
