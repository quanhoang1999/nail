using Cms.Data.Interfaces;
using Cms.Infrastructure.Interfaces;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Nails
{
    [Table("NailServices")]
    public class NailService : DomainEntity<int>, IHasSoftDelete, IDateTracking, IActive
    {
        public NailService()
        {
            NailServicePictures = new List<NailServicePicture>();
            NailEmployeeServices = new List<NailEmployeeService>();
        }
        public string Name { get; set; }
        public int? Time { get; set; } = 0;
        public DateTime? StartDate { get; set; }
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
        public Guid? UserCreated { get; set; }
        public Guid? UserModified { get; set; }
        public Guid? UserDeleted { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
     //   public string Employees { get; set; }
        public int? NailCategoryId { get; set; }
        [ForeignKey("NailCategoryId")]
        public virtual NailCategory NailCategory { get; set; }
        public virtual ICollection<NailServicePicture> NailServicePictures { get; set; }
        public virtual ICollection<NailEmployeeService> NailEmployeeServices { get; set; }
        public int? NailStoreId { get; set; }
        public virtual NailStore NailStore { get; set; }
    }
}
