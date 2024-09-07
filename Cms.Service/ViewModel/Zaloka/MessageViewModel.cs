using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Zaloka
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int OAId { get; set; }
        
        public OAInfomationViewModel OAInfomation { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
    }
    public  class SendMessageToUsersViewModel
    {
        public int TemplateMessageId { get; set; } 
        public List<string> UserIds { get; set; }
    }

    public class SendMessageToGroupViewModel
    {
        public int TemplateMessageId { get; set; }
        public  int MessageGroupId { get; set; }
    }
   
}
