using Cms.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Content
{
    public class TagViewModel
    {
        public string Id { set; get; }

        public string Name { set; get; }
        public TagType Type { set; get; }
    }
}
