using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Zaloka
{
    public class MessageGroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OAId { get; set; }
      
        public OAInfomationViewModel OAInfomation { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
        public virtual List<UserOAViewModel> UserOAs { get; set; }
    }
    public class MessageGroupUserViewModel
    {
        public MessageGroupViewModel MessageGroupViewModel { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OAId { get; set; }
        public string DisplayName { get; set; }
        public string AvatarFull { get; set; }
        public int DateNumberAdded { get; set; }
        public UserOAViewModel UserOAViewModel { get; set; }
        public List<string> UserIds { get; set; }
    }
   
}
