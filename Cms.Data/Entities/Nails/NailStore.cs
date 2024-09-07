using Cms.Data.Interfaces;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Nails
{
    [Table("NailStores")]
    public class NailStore : DomainEntity<int>, IDateTracking, IActive
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
