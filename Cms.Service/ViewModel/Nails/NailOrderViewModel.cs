using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Nails
{
    public class NailOrderViewModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public DateTime DatePick { get; set; }
        public DateTime TimePick { get; set; }
        public bool IsFinish { get; set; }
        public bool IsSendSMS { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid UserCreated { get; set; }
        public Guid? UserModified { get; set; }
        public Guid? UserDeleted { get; set; }
        public ICollection<NailOrderDetailViewModel> NailOrderDetails { get; set; }
        public virtual NailStoreViewModel NailStore { get; set; }
        public int NailStoreId { get; set; }
        public string NailStoreName { get; set; }
    }
}
