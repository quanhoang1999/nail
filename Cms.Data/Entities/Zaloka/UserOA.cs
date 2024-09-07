using Cms.Data.Interfaces;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Zaloka
{
    [Table("UserOAs")]
    public class UserOA : DomainEntity<int>, IDateTracking, IActive
    {
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string FullAvatar { get; set; }
        public int Gender { get; set; }
        public string UserIdByApp { get; set; }
        public int OAId { get; set; }
        [ForeignKey("OAId")]
        public virtual OAInfomation OAInfomation { get; set; }
        public int? MessageGroupId { get; set; }
        [ForeignKey("MessageGroupId")]
        public virtual MessageGroup MessageGroup { get; set; }
        public DateTime? DateAddedGroup { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
