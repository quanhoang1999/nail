using Cms.Data.Interfaces;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Zaloka
{
    [Table("Messages")]
    public class Message : DomainEntity<int>, IDateTracking, IActive
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public int OAId { get; set; }
        [ForeignKey("OAId")]
        public virtual OAInfomation OAInfomation { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
