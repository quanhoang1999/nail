using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Identity
{
    [Table("UserBussiness")]
    public class UserBussiness: DomainEntity<Guid>
    {
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; }

        public Guid BussinessId { get; set; }

        [ForeignKey("BussinessId")]
        public virtual Business Business { get; set; }
    }
}
