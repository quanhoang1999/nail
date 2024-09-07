using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Nails
{
    public class NailViewModel
    {
        public int Id { get; set; }
        public int? NailTypeId { get; set; }        
        public NailTypeViewModel NailType { get; set; }

       
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public string Desription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpriredDate { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TaxPercent { get; set; }
        public bool IsUsingTaxPercent { get; set; }
        public decimal? DisountPrice { get; set; }
        public bool IsUsingDisountPercent { get; set; }
        public decimal? DisountPercent { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeteted { get; set; } = false;
        public Guid UserCreated { get; set; }
        public Guid? UserModified { get; set; }
        public Guid? UserDeleted { get; set; }
        public int? Time { get; set; }
        public string NotePrice { get; set; }
        public int? OrderIndex { get; set; }
        public bool IsShowMenu { get; set; }
        public bool IsShowHomePage { get; set; }
    }
}
