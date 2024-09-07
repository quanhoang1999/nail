using Cms.Data.Entities.Media;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Nails
{
    [Table("NailCategoryPictures")]
    public class NailCategoryPicture : DomainEntity<int>
    {
        public int NailCategoryId { get; set; }
        [ForeignKey("NailCategoryId")]
        public virtual NailCategory NailCategory { get; set; }
        public virtual int PictureId { get; set; }
        [ForeignKey("PictureId")]
        public virtual Picture Picture { get; set; }
        public int DisplayOrder { get; set; }
    }
}
