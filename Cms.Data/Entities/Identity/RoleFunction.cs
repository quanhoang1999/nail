using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Identity
{
    [Table("RoleFunctions")]
    public class RoleFunction: DomainEntity<Guid>
    {
        [Required]
        public Guid RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual AppRole AppRole { get; set; }

        [Required]
        public Guid FunctionId { get; set; }

        [ForeignKey("FunctionId")]
        public virtual Function Function { get; set; }
    }
}
