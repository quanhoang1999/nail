using Cms.Infrastructure.Enums;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Content
{
    [Table("Footer")]
    public class Footer : DomainEntity<string>
    {
        [Required]
        public string Content { set; get; }

        public Status Status { set; get; }
    }
}
