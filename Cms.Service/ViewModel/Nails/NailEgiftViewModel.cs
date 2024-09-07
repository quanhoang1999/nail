using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cms.Service.ViewModel.Nails
{
    public class NailEgiftViewModel
    {
        public int Id { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TaxPercent { get; set; }
        public decimal? DisountPrice { get; set; }
        public decimal? DisountPercent { get; set; }
        public string Description { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ExpriredDate { get; set; }

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
