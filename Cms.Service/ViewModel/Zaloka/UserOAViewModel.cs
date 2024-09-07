using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Zaloka
{
    public class UserOAViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int OAId { get; set; }
        public virtual OAInfomationViewModel OAInfomation { get; set; }
        public int? MessageGroupId { get; set; }
        public virtual MessageGroupViewModel MessageGroup { get; set; }
        public string DisplayName { get; set; }
        public string FullAvatar { get; set; }
        public int Gender { get; set; }
        public string UserIdByApp { get; set; }
        public DateTime? DateAddedGroup { get; set; }

        public int NumberDateAdded
        {
            get;set;
        }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
