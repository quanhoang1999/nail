using Cms.Data.Interfaces;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Cms.Data.Entities.CRM
{
    [Table("Customers")]
    public class Customer: DomainEntity<Guid>, IDateTracking, IActive
    {
        [Required]
        public int CustomerTypeId { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerName { get; set; }
        public bool Gender { get; set; }
        [StringLength(250)]
        public string CustomerFax { get; set; }
        [StringLength(250)]
        public string CustomerWebsite { get; set; }
        [StringLength(250)]
        public string CustomerBirthDay { get; set; }
        [StringLength(250)]
        public string CustomerEmail { get; set; }
        [StringLength(50)]
        public string CustomerPhone { get; set; }

        public int ProvinceID { get; set; }
        public int DistrictID { get; set; }
        public int WardID { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(250)]
        public string FullAddress { get; set; }

        [StringLength(50)]
        public string Taxcode { get; set; }
        public int BrandID { get; set; }
        [StringLength(250)]
        public string Facebook { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public DateTime? DateDeleted { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public Guid UserCreated { get; set; }
        public Guid UserDeleted { get; set; }

    }
}
