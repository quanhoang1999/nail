using Cms.Data.Entities.Media;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Data.Entities.Nails
{
    public class NailServicePicture : DomainEntity<int>
    {
        public int NailServiceId { get; set; }
        [ForeignKey("NailServiceId")]
        public NailService NailService { get; set; }
        public virtual int PictureId { get; set; }
        [ForeignKey("PictureId")]
        public virtual Picture Picture { get; set; }
        public int DisplayOrder { get; set; }
    }
}
