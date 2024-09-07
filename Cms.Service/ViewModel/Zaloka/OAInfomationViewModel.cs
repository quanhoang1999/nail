using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Zaloka
{
    public class OAInfomationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AppId { get; set; }
        public string AccessToken { get; set; }
        public int OATypeId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
    }
    public class AddUserToOAViewModel
    {
        public List<Guid> UserIds { get; set; }
        public int OaId { get; set; }
    }
}
