using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Infrastructure.Dtos
{
    public class TreeViewItem
    {
        public string text { get; set; }

        public Guid value { get; set; }

        public bool collapsed { get; set; }

        public bool @checked { get; set; }

        public List<TreeViewItem> children { get; set; }
    }
}
