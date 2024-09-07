using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Content
{
    [Table("PostCates")]
    public class PostCate : DomainEntity<int>
    {
        public Guid PostId { set; get; }

        public int PostCategoryId { set; get; }
        [ForeignKey("PostId")]
        public virtual Post Post { set; get; }

        [ForeignKey("PostCategoryId")]
        public virtual PostCategory PostCategory { set; get; }

    }
}
