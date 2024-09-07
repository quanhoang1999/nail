using Cms.Data.Enums;
using Cms.Infrastructure.Enums;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Content
{
    [Table("Slides")]
    public class Slide: DomainEntity<int>
    {
        [StringLength(250)]
        [Required]
        public string Name { set; get; }

        [StringLength(250)]
        public string Description { set; get; }

        [StringLength(250)]
        [Required]
        public string Image { set; get; }

        [StringLength(250)]
        public string Url { set; get; }

        public int? DisplayOrder { set; get; }

        public string Content { set; get; }

        [Required]
        public SlideGroup GroupAlias { get; set; }

        public int SortOrder { set; get; }
        public Status Status { set; get; }
    }
}
