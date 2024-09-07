using Cms.Data.Interfaces;
using Cms.Infrastructure.Interfaces;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Content
{
    [Table("Reviews")]
    public class Review : DomainEntity<Guid>, IDateTracking, IActive, IHasSoftDelete
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int Vote { get; set; }
        public string Content { get; set; }
        public int Social { get; set; }
        public string Avartar { get; set; }
        public string Icon { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeteted { get; set; } = false;
        public Guid UserCreated { get; set; }
        public Guid? UserModified { get; set; }
        public Guid? UserDeleted { get; set; }
        public bool IsDeleted { get; set; }
    }
}
