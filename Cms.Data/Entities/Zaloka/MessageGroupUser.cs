using Cms.Data.Interfaces;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Zaloka
{
    [Table("MessageGroupUsers")]
    public class MessageGroupUser : DomainEntity<int>
    {
        public int? UserOAId { get; set; }
        public int MessageGroupId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserOAId")]
        public virtual UserOA UserOA { get; set; }
        [ForeignKey("MessageGroupId")]
        public virtual MessageGroup MessageGroup { get; set; }
        public DateTime? DateAddedGroup { get; set; }
      
    }
}
