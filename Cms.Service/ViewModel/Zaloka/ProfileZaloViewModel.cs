using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Zaloka
{
    public class ProfileZaloViewModel
    {
        public string DisplayName { get; set; }
        public string FullAvatar { get; set; }
        public string Avatar { get; set; }
        public int Gender { get; set; }
        public string UserId { get; set; }
        public string UserIdByApp { get; set; }

    }
    public class ProfileOAViewModel
    {
        public string OaId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Cover { get; set; }
    }
}
