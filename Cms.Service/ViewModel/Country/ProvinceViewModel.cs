using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cms.Service.ViewModel.Country
{
    public class ProvinceViewModel
    {
        public int Id { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(20)]
        public string Type { get; set; }
        public int TelephoneCode { get; set; }
        [StringLength(20)]
        public string ZipCode { get; set; }
        public int CountryId { get; set; }
        [StringLength(2)]
        public string CountryCode { get; set; }
        public int SortOrder { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
    }
}
