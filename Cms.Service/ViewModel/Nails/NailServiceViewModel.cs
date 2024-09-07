using Cms.Service.ViewModel.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cms.Service.ViewModel.Nails
{
    public class NailServiceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Time { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ExpriredDate { get; set; }
        public decimal SalePrice { get; set; }
        public string NotePrice { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
        public int? OrderIndex { get; set; }
        public bool IsShowMenu { get; set; }
        public bool IsShowHomePage { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid UserCreated { get; set; }
        public Guid? UserModified { get; set; }
        public Guid? UserDeleted { get; set; }
        public string Description { get; set; }
        public int NailCategoryId { get; set; }
        public string DefaultImage { get; set; }
        public List<string> Images { get; set; }
        public virtual NailCategoryViewModel NailCategory { get; set; }
        public virtual NailStoreViewModel NailStore { get; set; }
        public string NailStoreName { get; set; }
        public int NailStoreId { get; set; }
        public List<PictureViewModel> Pictures { get; set; }
        public List<Guid> Employees { get; set; }
        public List<NailEmployeeViewModel> NailEmployees { get; set; }
        public virtual ICollection<NailEmployeeServiceViewModel> NailEmployeeServices { get; set; }
    }
}
