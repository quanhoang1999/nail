using Cms.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Identity
{
    [Table("AppRoles")]
    public class AppRole : IdentityRole<Guid>, IActive, IDateTracking
    {

        [StringLength(250)]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public Guid? BussinessId { get; set; }

        [ForeignKey("BussinessId")]
        public virtual Business Business { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public DateTime? DateDeleted { get; set; }
    }
}
