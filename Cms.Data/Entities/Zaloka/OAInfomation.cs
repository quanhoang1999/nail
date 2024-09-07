using Cms.Data.Interfaces;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Zaloka
{
    [Table("OAInfomations")]
    public class OAInfomation : DomainEntity<int>, IDateTracking, IActive
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AppId { get; set; }
        public string AccessToken { get; set; }
        public int OATypeId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get ; set; }
        public DateTime? DateDeleted { get; set ; }
        public bool IsActive { get; set ; }
    }
}
