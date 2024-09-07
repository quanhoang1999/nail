using Cms.Data.Interfaces;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Zaloka
{
    [Table("Campaigns")]
    public class Campaign : DomainEntity<int>, IDateTracking, IActive
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int MessageGroupId { get; set; }

        [ForeignKey("MessageGroupId")]
        public virtual MessageGroup MessageGroup { get; set; }
        public int OAId { get; set; }
        [ForeignKey("OAId")]
        public virtual OAInfomation OAInfomation { get; set; }

        public virtual ICollection<CampaignDetail> CampaignDetails { get; set; }
        public int? GroupOtherId { get; set; }
        public int? DayMove { get; set; }
        public bool IsAction { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
