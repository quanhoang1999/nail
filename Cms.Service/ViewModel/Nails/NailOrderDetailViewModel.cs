using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Nails
{
    public class NailOrderDetailViewModel
    {
        public int Id { get; set; }
        public int NailServiceId { get; set; }
       
        public NailServiceViewModel NailService { get; set; }
        public int NailOrderId { get; set; }
       
        public NailOrderViewModel NailOrder { get; set; }
        public decimal Quantity { set; get; }
        public decimal Price { get; set; }
        public string NailServiceName { get; set; }
        public Guid? NailEmployeeId { get; set; }
     
        public NailEmployeeViewModel NailEmployee { get; set; }
        public string NailEmployeeName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid UserCreated { get; set; }
        public Guid? UserModified { get; set; }
        public Guid? UserDeleted { get; set; }
    }
}
