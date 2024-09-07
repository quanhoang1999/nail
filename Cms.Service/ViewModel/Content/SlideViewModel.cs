using Cms.Data.Enums;
using Cms.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Content
{
    public class SlideViewModel
    {
        public int Id
        {
            get; set;
        }
        public string Name { set; get; }


        public string Description { set; get; }


        public string Image { set; get; }


        public string Url { set; get; }

        public int? DisplayOrder { set; get; }

        public string Content { set; get; }


        public SlideGroup GroupAlias { get; set; }

        public int SortOrder { set; get; }
        public Status Status { set; get; }
        public string ImageFullUrl { get; set; }
    }
}
