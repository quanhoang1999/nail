using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Identity
{
    [Table("RoleUIElement")]
    public class RoleUIElement : DomainEntity<Guid>
    {
        [Required]
        public Guid RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual AppRole AppRole { get; set; }

        [Required]
        public Guid ElementId { get; set; }

        [ForeignKey("ElementId")]
        public virtual UIElement UIElement { get; set; }
    }
}
