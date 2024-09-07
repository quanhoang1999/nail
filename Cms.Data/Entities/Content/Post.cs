using Cms.Data.Interfaces;
using Cms.Infrastructure.Enums;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Content
{
    [Table("Post")]
    public class Post : DomainEntity<Guid>, IDateTracking
    {
        public Post()
        {
            PostTags = new List<PostTag>();
            PostCates = new List<PostCate>();
        }
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [MaxLength(256)]
        public string Image { set; get; }

        [MaxLength(500)]
        public string Description { set; get; }

        public string Content { set; get; }

        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }

        public string Tags { get; set; }

        public DateTime DateCreated { set; get; }
        public DateTime? DateModified { set; get; }
        public Status Status { set; get; }

        [MaxLength(256)]
        public string SeoPageTitle { set; get; }

        [MaxLength(256)]
        public string SeoAlias { set; get; }

        [MaxLength(256)]
        public string SeoKeywords { set; get; }

        [MaxLength(256)]
        public string SeoDescription { set; get; }
        public DateTime? DateDeleted { set; get; }
        public string Categories { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
        public virtual ICollection<PostCate> PostCates { get; set; }
    }
}
