using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cms.Service.ViewModel.Country
{
    public class WardViewModel
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Type { get; set; }
        public string LatiLongTude { get; set; }
        public int DistrictID { get; set; }
        public int SortOrder { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
    }
}
