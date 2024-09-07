using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Zaloka
{
    public class CampaignDetailViewModel
    {
        public int Id { get; set; }
        public int Day { get; set; }
        public int CampaignId { get; set; }
       
        public virtual CampaignViewModel Campaign { get; set; }
        public int? MessageId { get; set; }
      
        public virtual MessageViewModel Message { get; set; }
      
        public DateTime DateCreated { get; set; }
    }
}
