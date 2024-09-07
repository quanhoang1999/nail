using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Nails
{
    public class NailEmployeeServiceViewModel
    {
        public int Id { get; set; }
        public int? NailServiceId { get; set; }
       
        public Guid? NailEmployeeId { get; set; }
       
        public NailEmployeeViewModel NailEmployee { get; set; }
    }
}
