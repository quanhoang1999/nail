using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Zaloka
{
    public class CampaignViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int MessageGroupId { get; set; }       
        public virtual MessageGroupViewModel MessageGroup { get; set; }
        public int OAId { get; set; }
        public virtual List<CampaignDetailViewModel> CampaignDetails { get; set; }
        public virtual OAInfomationViewModel OAInfomation { get; set; }
        public int? DayMove { get; set; }
        public int? GroupOtherId { get; set; }
        public bool IsAction { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
