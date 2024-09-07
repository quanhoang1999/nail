using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Nails
{
    public class NailStoreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string ImageFullUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
