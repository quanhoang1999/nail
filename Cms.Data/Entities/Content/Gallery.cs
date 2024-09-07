using Cms.Data.Entities.Nails;
using Cms.Data.Interfaces;
using Cms.Infrastructure.Interfaces;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Content
{
    [Table("Galleries")]
    public class Gallery : DomainEntity<int>, IDateTracking, IActive, IHasSoftDelete
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileUrl { get; set; }
        public int FileType { get; set; }
        public int? NailStoreId { get; set; }
        [ForeignKey("NailStoreId")]
        public virtual NailStore NailStore { get; set; }
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
