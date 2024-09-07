using Cms.Service.ViewModel.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Nails
{
    public class NailServicePictureViewModel
    {
        public int Id { get; set; }
        public int NailServiceId { get; set; }
      
        public NailServiceViewModel NailService { get; set; }
        public virtual int PictureId { get; set; }
      
        public virtual PictureViewModel Picture { get; set; }
        public int DisplayOrder { get; set; }
    }
}
