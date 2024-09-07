using Cms.Infrastructure.Enums;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Content
{
    [Table("Tag")]
    public class Tag : DomainEntity<string>
    {
        [MaxLength(50)]
        [Required]
        public string Name { set; get; }

        [Required]
        public TagType Type { set; get; }

        public int TagCount { get; set; }
    }
}
