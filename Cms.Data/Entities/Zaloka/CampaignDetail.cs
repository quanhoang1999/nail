using Cms.Data.Interfaces;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Zaloka
{
    [Table("CampaignDetails")]
    public class CampaignDetail : DomainEntity<int>, IDateTracking
    {
        public int Day { get; set; }
        public bool IsSent { get; set; }
        public int CampaignId { get; set; }
        [ForeignKey("CampaignId")]
        public virtual Campaign Campaign { get; set; }
        public int? MessageId { get; set; }
        [ForeignKey("MessageId")]
        public virtual Message Message { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
