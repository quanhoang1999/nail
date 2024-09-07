using Cms.Data.Entities.Media;
using Cms.Data.Interfaces;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Nails
{
    [Table("NailCategories")]
    public class NailCategory : DomainEntity<int>, IDateTracking, IActive
    {
        public NailCategory()
        {
            NailCategoryPictures = new List<NailCategoryPicture>();

        }
        public string Name { get; set; }
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
        public string Images { get; set; }
        public int? NailStoreId { get; set; }
        public virtual NailStore NailStore { get; set; }
        public virtual ICollection<NailCategoryPicture> NailCategoryPictures { get; set; }
        public virtual List<NailService> NailServices { get; set; }
    }
}
