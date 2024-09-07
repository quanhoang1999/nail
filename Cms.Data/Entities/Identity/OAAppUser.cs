using Cms.Data.Entities.Zaloka;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Identity
{
    [Table("OAAppUsers")]
    public class OAAppUser : DomainEntity<int>
    {
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; }
        public int OAId { get; set; }
        //[ForeignKey("OAId")]
        //public OAInfomation OAInfomation { get; set; }
    }
}
