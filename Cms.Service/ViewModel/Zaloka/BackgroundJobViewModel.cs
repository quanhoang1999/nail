using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Zaloka
{
    public class BackgroundJobViewModel
    {
        public List<string> UserOaIds { get; }
        public string MessageContent { get; set; }
        public string AccessToken { get; set; }
        public int CampaignDetailId { get; set; }
        public int CampaignId { get; set; }
        public int? GroupOtherId { get; set; }
        public int OaId { get; set; }
        public int MessageGroupId { get; set; }
    }
}
