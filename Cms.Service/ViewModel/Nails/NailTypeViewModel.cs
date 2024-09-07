using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Nails
{
    public class NailTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
        public Guid UserCreated { get; set; }
        public Guid? UserModified { get; set; }
        public Guid? UserDeleted { get; set; }
    }
}
