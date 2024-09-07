using Cms.Infrastructure.Enums;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.System
{
    [Table("Setting")]
    public class Setting : DomainEntity<Guid>
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public string TextValue { get; set; }

        public int? IntegerValue { get; set; }

        public bool? BooleanValue { get; set; }

        public DateTime? DateValue { get; set; }

        public decimal? DecimalValue { get; set; }

        public Status Status { get; set; }

        public string UniqueCode { get; set; }
    }
}
